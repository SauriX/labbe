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
        private readonly string key;
        private readonly ISeriesRepository _repository;
        private readonly IIdentityClient _identityClient;
        private readonly IBranchRepository _branchRepository;
        private readonly IConfigurationApplication _configurationApplication;
        private readonly string CatalogPath;
        private const int TIPO_FACTURA = 1;

        public SeriesApplication(ISeriesRepository repository, IIdentityClient identityClient, IBranchRepository branchRepository, IConfigurationApplication configurationApplication, IConfiguration configuration)
        {
            key = configuration.GetValue<string>("KeySettings:AvailableKey");
            _repository = repository;
            _identityClient = identityClient;
            _branchRepository = branchRepository;
            _configurationApplication = configurationApplication;
            CatalogPath = configuration.GetValue<string>("ClientUrls:Catalog") + configuration.GetValue<string>("ClientRoutes:Catalog");

        }

        public async Task<IEnumerable<SeriesListDto>> GetByBranch(Guid branchId, byte type)
        {
            var series = await _repository.GetByBranchType(branchId, type);

            return series.ToSeriesListDto();
        }

        public async Task<IEnumerable<SeriesListDto>> GetAll()
        {
            var series = await _repository.GetAll();

            return series.ToSeriesListDto();
        }

        public async Task<IEnumerable<SeriesListDto>> GetByFilter(SeriesFilterDto filter)
        {
            var series = await _repository.GetByFilter(filter);

            return series.ToSeriesListDto();
        }

        public async Task<SeriesDto> GetByNewForm(SeriesNewDto newSerie)
        {
            var data = new SeriesDto
            {
                Factura = new InvoiceSerieDto(),
                Expedicion = new ExpeditionPlaceDto()
            };

            return data;
        }

        public async Task<SeriesDto> GetById(int id, byte tipo)
        {
            var serie = await _repository.GetById(id, tipo);

            _ = new SeriesDto();
            SeriesDto data;

            ExpeditionPlaceDto expeditionData = new();

            var invoiceData = serie.ToInvoiceSerieDto();

            if (serie.SucursalId != Guid.Empty)
            {
                var defaultBranch = await _branchRepository.GetById(serie.SucursalId.ToString());
                expeditionData = defaultBranch.ToExpeditionPlaceDto(key);
            }

            if (tipo == TIPO_FACTURA)
            {

                invoiceData.Id = id;

                if (serie.ArchivoKey != null)
                {
                    invoiceData.ClaveKey = Path.Combine(CatalogPath, serie.ArchivoKey.Replace("wwwroot/file/series", "")).Replace("\\", "/");
                }

                if (serie.ArchivoCer != null)
                {
                    invoiceData.ClaveCer = Path.Combine(CatalogPath, serie.ArchivoCer.Replace("wwwroot/file/series", "")).Replace("\\", "/");
                }

                data = new SeriesDto
                {
                    Id = id,
                    Factura = invoiceData,
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
                    Expedicion = expeditionData,
                    UsuarioId = serie.UsuarioCreoId
                };
            }

            return data;
        }

        public async Task<ExpeditionPlaceDto> GetBranch(string branchId)
        {
            var branch = await _branchRepository.GetById(branchId);

            return branch.ToExpeditionPlaceDto(key);
        }

        public async Task<IEnumerable<SeriesListDto>> GetSeries(Guid branchId)
        {
            var series = await _repository.GetAll(branchId);

            return series.ToSeriesListDto();
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
            string branchName;

            if (branch != null)
            {
                data.Ciudad = branch.Ciudad;
                branchName = branch.Nombre;
            }
            else
            {
                throw new CustomException(HttpStatusCode.BadRequest, "Debe asignar una sucursal");
            }

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

            var branch = await _branchRepository.GetById(ticket.Expedicion.SucursalId);

            if (branch != null)
            {
                data.Ciudad = branch.Ciudad;
            }
            else
            {
                throw new CustomException(HttpStatusCode.BadRequest, "Debe asignar una sucursal");
            }

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

            var branch = await _branchRepository.GetById(ticket.Expedicion.SucursalId);

            if (branch == null)
            {
                throw new CustomException(HttpStatusCode.BadRequest, "Debe asignar una sucursal");
            }

            data.Ciudad = branch.Ciudad;

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
            var sameBranch = data.SucursalId.ToString() == serie.Expedicion.SucursalId;

            if (branch == null)
            {
                throw new CustomException(HttpStatusCode.BadRequest, "Debe asignar una sucursal");
            }

            data.Ciudad = branch.Ciudad;
            var branchName = branch.Nombre;

            if (sameBranch)
            {
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
            }
            else
            {
                File.Delete(data.ArchivoKey);
                File.Delete(data.ArchivoCer);

                var cerFile = await SaveFilePath(serie.Factura.ArchivoCer, branchName);
                var keyFile = await SaveFilePath(serie.Factura.ArchivoKey, branchName);

                if (!string.IsNullOrEmpty(cerFile) && !string.IsNullOrEmpty(keyFile))
                {
                    data.ArchivoCer = cerFile;
                    data.ArchivoKey = keyFile;
                }
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
