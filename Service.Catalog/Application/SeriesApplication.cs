using ClosedXML.Excel;
using ClosedXML.Report;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Service.Catalog.Application.IApplication;
using Service.Catalog.Dto.Series;
using Service.Catalog.Dtos.Series;
using Service.Catalog.Mapper;
using Service.Catalog.Repository.IRepository;
using Service.Catalog.Client.IClient;
using Shared.Dictionary;
using Shared.Error;
using Shared.Extensions;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Threading.Tasks;
using Service.Catalog.Dictionary;
using Service.Catalog.Domain.Series;
using Service.Catalog.Domain.Branch;

namespace Service.Catalog.Application
{
    public class SeriesApplication : ISeriesApplication
    {
        private readonly ISeriesRepository _repository;
        private readonly IIdentityClient _identityClient;
        private readonly IBranchRepository _branchRepository;
        private readonly IConfigurationApplication _configurationApplication;
        private readonly string CatalogPath;
        private const int TIPO_FACTURA = 1;

        public SeriesApplication(ISeriesRepository repository, IIdentityClient identityClient, IBranchRepository branchRepository, IConfigurationApplication configurationApplication, IConfiguration configuration)
        {
            _repository = repository;
            _identityClient = identityClient;
            _branchRepository = branchRepository;
            _configurationApplication = configurationApplication;
            CatalogPath = configuration.GetValue<string>("ClientUrls:Catalog") + configuration.GetValue<string>("ClientRoutes:Catalog");

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
                var userBranch = await _branchRepository.GetById(newSerie.EmisorId.ToString());
                var userConfiguration = await _configurationApplication.GetFiscal();

                var userData = new OwnerInfoDto();

                if (userConfiguration != null)
                {
                    userData = userBranch.ToOwnerInfoDto();
                }

                userData.WebSite = userConfiguration.WebSite ?? "www.laboratorioramos.com.mx";
                userData.RFC = userConfiguration.RFC ?? "";
                userData.RazonSocial = userConfiguration.RazonSocial ?? "";
                userData.Correo = userConfiguration.Correo ?? "";

                data = new SeriesDto
                {
                    Factura = new InvoiceSerieDto(),
                    Emisor = userData,
                    Expedicion = new ExpeditionPlaceDto()
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

        public async Task<SeriesDto> GetById(int id, byte tipo)
        {
            var serie = await _repository.GetById(id, tipo);

            _ = new SeriesDto();
            SeriesDto data;

            var invoiceData = serie.ToInvoiceSerieDto();

            if (tipo == TIPO_FACTURA)
            {
                var userBranch = await _branchRepository.GetById(serie.EmisorId.ToString());
                var userConfiguration = await _configurationApplication.GetFiscal();
                var defaultBranch = await _branchRepository.GetById(serie.SucursalId.ToString());

                var userData = userBranch.ToOwnerInfoDto();
                var expeditionData = defaultBranch.ToExpeditionPlaceDto();

                userData.WebSite = userConfiguration.WebSite ?? "www.laboratorioramos.com.mx";
                userData.RFC = userConfiguration.RFC ?? "";
                userData.RazonSocial = userConfiguration.RazonSocial ?? "";
                userData.Correo = userConfiguration.Correo ?? "";

                invoiceData.Id = id;
                invoiceData.ClaveCer = Path.Combine(CatalogPath, serie.ArchivoCer.Replace("wwwroot/file/series", "")).Replace("\\", "/");
                invoiceData.ClaveKey = Path.Combine(CatalogPath, serie.ArchivoKey.Replace("wwwroot/file/series", "")).Replace("\\", "/");

                data = new SeriesDto
                {
                    Id = id,
                    Factura = invoiceData,
                    Emisor = userData,
                    Expedicion = expeditionData,
                    UsuarioId = serie.UsuarioCreoId
                };
            }
            else
            {
                data = new SeriesDto
                {
                    Id = id,
                    Factura = invoiceData,
                    Emisor = new OwnerInfoDto(),
                    Expedicion = new ExpeditionPlaceDto(),
                    UsuarioId = serie.UsuarioCreoId
                };
            }

            return data;
        }

        public async Task<ExpeditionPlaceDto> GetBranch(string branchId)
        {
            var branch = await _branchRepository.GetById(branchId);

            return branch.ToExpeditionPlaceDto();
        }

        private static async Task<string> SaveFilePath(IFormFile archivo, string nombre)
        {
            var path = Path.Combine("wwwroot/file/series", nombre);
            var name = archivo.FileName;

            var isSaved = await archivo.SaveFileAsync(path, name);

            if (isSaved)
            {
                return Path.Combine(path, name);
            }

            return null;
        }

        public async Task CreateInvoice(SeriesDto serie)
        {
            var data = serie.ToModelCreate();

            await CheckDuplicate(data);

            var branch = await _branchRepository.GetById(serie.Expedicion.SucursalId);

            data.Ciudad = branch.Ciudad;
            var branchName = branch.Nombre;

            var cerFile = await SaveFilePath(serie.Factura.ArchivoCer, branchName);
            var keyFile = await SaveFilePath(serie.Factura.ArchivoKey, branchName);

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

            await CheckDuplicate(data);

            await _repository.Create(data);
        }

        public async Task UpdateTicket(TicketDto ticket)
        {
            var existingSerie = await _repository.GetById((int)ticket.Id, ticket.TipoSerie);

            if (existingSerie == null)
            {
                throw new CustomException(HttpStatusCode.NotFound, Responses.NotFound);
            }

            var data = ticket.ToTicketUpdate(existingSerie);

            await CheckDuplicate(data);

            await _repository.Update(data);
        }

        public async Task UpdateInvoice(SeriesDto serie)
        {
            var existingSerie = await _repository.GetById((int)serie.Id, serie.Factura.TipoSerie);

            if (existingSerie == null)
            {
                throw new CustomException(HttpStatusCode.NotFound, Responses.NotFound);
            }

            var data = serie.ToModelUpdate(existingSerie);

            var branch = await _branchRepository.GetById(serie.Expedicion.SucursalId);

            data.Ciudad = branch.Ciudad;
            var branchName = branch.Nombre;

            if (serie.Factura.ArchivoCer != null)
            {
                File.Delete(data.ArchivoCer);
                var cerFile = await SaveFilePath(serie.Factura.ArchivoCer, branchName);
                data.ArchivoCer = cerFile;
            }

            if (serie.Factura.ArchivoKey != null)
            {
                File.Delete(data.ArchivoKey);
                var keyFile = await SaveFilePath(serie.Factura.ArchivoKey, branchName);
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
            template.AddVariable("Titulo", "Series de facturas y recibos");
            template.AddVariable("Fecha", DateTime.Now.ToString("dd/MM/yyyy"));
            template.AddVariable("Series", invoice);

            template.Generate();

            var range = template.Workbook.Worksheet("Series").Range("Series");
            var table = template.Workbook.Worksheet("Series").Range("$A$3:" + range.RangeAddress.LastAddress).CreateTable();
            table.Theme = XLTableTheme.TableStyleMedium2;

            template.Format();

            return (template.ToByteArray(), "Series de facturas y recibos.xlsx");
        }

        private async Task CheckDuplicate(Serie serie)
        {
            var isDuplicate = await _repository.IsDuplicate(serie);

            if (isDuplicate)
            {
                throw new CustomException(HttpStatusCode.Conflict, Responses.Duplicated("La clave o el nombre"));
            }
        }
    }
}
