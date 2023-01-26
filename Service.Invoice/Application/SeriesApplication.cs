using ClosedXML.Excel;
using ClosedXML.Report;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Service.Billing.Application.IApplication;
using Service.Billing.Client.IClient;
using Service.Billing.Dictionary;
using Service.Billing.Dto.Series;
using Service.Billing.Dtos.Series;
using Service.Billing.Mapper;
using Service.Billing.Repository.IRepository;
using Shared.Dictionary;
using Shared.Error;
using Shared.Extensions;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Threading.Tasks;

namespace Service.Billing.Application
{
    public class SeriesApplication : ISeriesApplication
    {
        private readonly ISeriesRepository _repository;
        private readonly IIdentityClient _identityClient;
        private readonly ICatalogClient _catalogClient;
        private readonly string BillingPath;
        private const int TIPO_FACTURA = 1;

        public SeriesApplication(ISeriesRepository repository, IIdentityClient identityClient, ICatalogClient catalogClient, IConfiguration configuration)
        {
            _repository = repository;
            _identityClient = identityClient;
            _catalogClient = catalogClient;
            BillingPath = configuration.GetValue<string>("ClientUrls:Billing") + configuration.GetValue<string>("ClientRoutes:Billing");

        }

        public async Task<IEnumerable<SeriesListDto>> GetByBranch(Guid branchId, byte type)
        {
            var series = await _repository.GetByBranch(branchId, type);

            return series.ToSeriesListDto();
        }

        public async Task<IEnumerable<SeriesListDto>> GetByFilter(SeriesFilterDto filter)
        {
            var series = await _repository.GetByFilter(filter);

            return series.ToSeriesListDto();
        }

        public async Task<SeriesDto> GetByNewForm(SeriesNewDto newSerie)
        {
            _ = new SeriesDto();
            SeriesDto data;

            if (newSerie.TipoSerie == TIPO_FACTURA)
            {
                var user = await _identityClient.GetUserById(newSerie.UsuarioId.ToString());

                var userBranch = await _catalogClient.GetBranchByName(user.SucursalId.ToString());
                var userConfiguration = await _catalogClient.GetFiscalConfig();
                var defaultBranch = await _catalogClient.GetBranchByName(newSerie.SucursalId.ToString());

                var userData = userBranch.ToOwnerInfoDto();
                var expeditionData = defaultBranch.ToExpeditionPlaceDto();

                userData.WebSite = "www.laboratorioramos.com.mx";
                userData.RFC = userConfiguration.RFC ?? "";
                userData.RazonSocial = userConfiguration.RazonSocial ?? "";

                data = new SeriesDto
                {
                    Factura = new InvoiceSerieDto(),
                    Emisor = userData,
                    Expedicion = expeditionData
                };
            }
            else
            {
                data = new SeriesDto
                {
                    Factura = new InvoiceSerieDto(),
                    Emisor = new OwnerInfoDto(),
                    Expedicion = new ExpeditionPlaceDto()
                };
            }

            return data;
        }

        public async Task<SeriesDto> GetById(int id)
        {
            var serie = await _repository.GetById(id);

            var userBranch = await _catalogClient.GetBranchByName(serie.EmisorId.ToString());
            var defaultBranch = await _catalogClient.GetBranchByName(serie.SucursalId.ToString());

            var userData = userBranch.ToOwnerInfoDto();
            var expeditionData = defaultBranch.ToExpeditionPlaceDto();
            var invoiceData = serie.ToInvoiceSerieDto();

            invoiceData.ClaveCer = Path.Combine(BillingPath, serie.ArchivoCer.Replace("wwwroot/file/series", "")).Replace("\\", "/");
            invoiceData.ClaveKey = Path.Combine(BillingPath, serie.ArchivoKey.Replace("wwwroot/file/series", "")).Replace("\\", "/");

            var data = new SeriesDto
            {
                Factura = invoiceData,
                Emisor = userData,
                Expedicion = expeditionData,
                UsuarioId = serie.UsuarioCreoId
            };

            return data;
        }

        private static async Task<string> SaveFilePath(IFormFile cer, IFormFile key, string nombre)
        {
            var path = Path.Combine("wwwroot/file/series", nombre);
            var name = cer != null ? string.Concat(cer, ".cer") : string.Concat(key, ".key");

            var isSaved = cer != null ? await cer.SaveFileAsync(path, name) : await key.SaveFileAsync(path, name);

            if (isSaved)
            {
                return Path.Combine(path, name);
            }

            return null;
        }

        public async Task CreateInvoice(SeriesDto serie)
        {
            var data = serie.ToModelCreate();
            
            var expeditionBranch = await _catalogClient.GetBranchByName(serie.Expedicion.Municipio);
            data.SucursalId = Guid.Parse(expeditionBranch.IdSucursal);
            data.Sucursal = expeditionBranch.Nombre;
            data.Ciudad = expeditionBranch.Ciudad;

            var fileName = serie.Factura.Id + "/" + serie.Factura.Nombre;

            var cerFile = await SaveFilePath(serie.Factura.ArchivoCer, null, fileName);
            var keyFile = await SaveFilePath(null, serie.Factura.ArchivoKey, fileName);

            if (!string.IsNullOrEmpty(cerFile) && !string.IsNullOrEmpty(keyFile))
            {
                data.ArchivoCer = cerFile;
                data.ArchivoKey = keyFile;

                await _repository.Create(data);
            }
            else
            {
                throw new CustomException(HttpStatusCode.NotFound, Responses.NotFound);
            }
        }

        public async Task CreateTicket(TicketDto ticket)
        {
            var data = ticket.ToTicketCreate();

            await _repository.Create(data);
        }

        public async Task UpdateTicket(TicketDto ticket)
        {
            var existingSerie = await _repository.GetById((int)ticket.Id);

            if(existingSerie == null)
            {
                throw new CustomException(HttpStatusCode.NotFound, Responses.NotFound);
            }

            var data = ticket.ToTicketUpdate(existingSerie);

            await _repository.Update(data);
        }

        public async Task UpdateInvoice(SeriesDto serie)
        {
            var existingSerie = await _repository.GetById((int)serie.Id);

            if (existingSerie == null)
            {
                throw new CustomException(HttpStatusCode.NotFound, Responses.NotFound);
            }

            var data = serie.ToModelUpdate(existingSerie);

            var fileName = serie.Factura.Id + "/" + serie.Factura.Nombre;

            var cerFile = await SaveFilePath(serie.Factura.ArchivoCer, null, fileName);
            var keyFile = await SaveFilePath(null, serie.Factura.ArchivoKey, fileName);

            if (cerFile == existingSerie.ArchivoCer)
            {
                data.ArchivoCer = cerFile;
            }

            if(keyFile == existingSerie.ArchivoKey)
            {
                data.ArchivoKey = keyFile;
            }

            await _repository.Update(data);
        }

        public async Task<(byte[] file, string fileName)> ExportSeriesList(SeriesFilterDto search)
        {
            var invoice = await GetByFilter(search);

            var path = Assets.SeriesList;

            var template = new XLTemplate(path);

            template.AddVariable("Direccion", "Avenida Humberto Lobo #555");
            template.AddVariable("Sucursal", "San Pedro Garza García, Nuevo León");
            template.AddVariable("Titulo", "Catálogo de facturas y recibos");
            template.AddVariable("Fecha", DateTime.Now.ToString("dd/MM/yyyy"));
            template.AddVariable("Series", invoice);

            template.Generate();

            var range = template.Workbook.Worksheet("Series").Range("Series");
            var table = template.Workbook.Worksheet("Series").Range("$A$3:" + range.RangeAddress.LastAddress).CreateTable();
            table.Theme = XLTableTheme.TableStyleMedium2;

            template.Format();

            return (template.ToByteArray(), "Catálogo de facturas y recibos.xlsx");
        }
    }
}
