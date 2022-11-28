using ClosedXML.Excel;
using ClosedXML.Report;
using EventBus.Messages.Common;
using MassTransit;
using Service.MedicalRecord.Application.IApplication;
using Service.MedicalRecord.Client;
using Service.MedicalRecord.Client.IClient;
using Service.MedicalRecord.Dictionary;
using Service.MedicalRecord.Domain.Catalogs;
using Service.MedicalRecord.Dtos.MedicalRecords;
using Service.MedicalRecord.Mapper;
using Service.MedicalRecord.Repository.IRepository;
using Service.MedicalRecord.Settings.ISettings;
using Service.MedicalRecord.Transactions;
using Service.MedicalRecord.Utils;
using Shared.Dictionary;
using Shared.Error;
using Shared.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using SharedResponses = Shared.Dictionary.Responses;
using RecordResponses = Service.MedicalRecord.Dictionary.Response;
using Service.MedicalRecord.Dtos.Quotation;
using Service.MedicalRecord.Domain.Quotation;
using QuotationTemplates = Service.MedicalRecord.Dictionary.EmailTemplates.Quotation;
using Service.MedicalRecord.Dtos.Promotion;
using Service.MedicalRecord.Dtos.Request;

namespace Service.MedicalRecord.Application
{
    public class QuotationApplication : IQuotationApplication
    {
        private readonly ITransactionProvider _transaction;
        private readonly IQuotationRepository _repository;
        private readonly ICatalogClient _catalogClient;
        private readonly IPdfClient _pdfClient;
        private readonly ISendEndpointProvider _sendEndpointProvider;
        private readonly IRabbitMQSettings _rabbitMQSettings;
        private readonly IQueueNames _queueNames;
        private readonly IRepository<Branch> _branchRepository;
        private readonly IRequestApplication _requestApplication;

        private const byte PORCENTAJE = 1;
        private const byte CANTIDAD = 2;

        public QuotationApplication(
            ITransactionProvider transaction,
            IQuotationRepository repository,
            ICatalogClient catalogClient,
            IPdfClient pdfClient,
            ISendEndpointProvider sendEndpointProvider,
            IRabbitMQSettings rabbitMQSettings,
            IQueueNames queueNames,
            IRepository<Branch> branchRepository,
            IRequestApplication requestApplication)
        {
            _transaction = transaction;
            _repository = repository;
            _catalogClient = catalogClient;
            _pdfClient = pdfClient;
            _sendEndpointProvider = sendEndpointProvider;
            _rabbitMQSettings = rabbitMQSettings;
            _queueNames = queueNames;
            _branchRepository = branchRepository;
            _requestApplication = requestApplication;
        }

        public async Task<IEnumerable<QuotationInfoDto>> GetByFilter(QuotationFilterDto filter)
        {
            var quotations = await _repository.GetByFilter(filter);

            return quotations.ToQuotationInfoDto();
        }

        public async Task<IEnumerable<QuotationInfoDto>> GetActive()
        {
            var quotations = await _repository.GetActive();

            return quotations.ToQuotationInfoDto();
        }

        public async Task<QuotationDto> GetById(Guid quotationId)
        {
            var quotation = await _repository.GetById(quotationId);

            return quotation.ToQuotationDto();
        }

        public async Task<QuotationGeneralDto> GetGeneral(Guid quotationId)
        {
            var quotation = await GetExistingQuotation(quotationId);

            return quotation.ToQuotationGeneralDto();
        }

        public async Task<QuotationStudyUpdateDto> GetStudies(Guid quotationId)
        {
            var quotation = await GetExistingQuotation(quotationId);

            var packs = await _repository.GetPacksByQuotation(quotation.Id);
            var packsDto = packs.ToQuotationPackDto();

            var studies = await _repository.GetStudiesByQuotation(quotation.Id);
            var studiesDto = studies.ToQuotationStudyDto();

            var ids = studiesDto.Select(x => x.EstudioId).Concat(packsDto.SelectMany(y => y.Estudios).Select(y => y.EstudioId)).Distinct().ToList();
            var studiesParams = await _catalogClient.GetStudies(ids);

            var packsPromoFilter = packs
                .GroupBy(x => x.PaqueteId).Select(x => x.First())
                .Select(x => new PriceListInfoFilterDto(0, x.PaqueteId, quotation.SucursalId, (Guid)quotation.MedicoId, (Guid)quotation.CompañiaId, x.ListaPrecioId))
                .ToList();
            var studiesPromoFilter = studies
                .GroupBy(x => x.EstudioId).Select(x => x.First())
                .Select(x => new PriceListInfoFilterDto(x.EstudioId, 0, quotation.SucursalId, (Guid)quotation.MedicoId, (Guid)quotation.CompañiaId, x.ListaPrecioId))
                .ToList();

            var packsPromos = new List<PriceListInfoPromoDto>();
            var studiesPromos = new List<PriceListInfoPromoDto>();

            if (packsPromoFilter.Any())
            {
                packsPromos = await _catalogClient.GetPacksPromos(packsPromoFilter);
            }

            if (studiesPromoFilter.Any())
            {
                studiesPromos = await _catalogClient.GetStudiesPromos(studiesPromoFilter);
            }

            foreach (var pack in packsDto)
            {
                foreach (var study in pack.Estudios)
                {
                    var st = studiesParams.FirstOrDefault(x => x.Id == study.EstudioId);
                    if (st == null) continue;

                    study.Parametros = st.Parametros;
                    study.Indicaciones = st.Indicaciones;
                }

                var promos = packsPromos.Where(x => x.PaqueteId == pack.PaqueteId).ToList();
                pack.Promociones = promos;

                if (pack.PromocionId != null && !promos.Any(x => x.PromocionId == pack.PromocionId))
                {
                    pack.Promociones.Add(new PriceListInfoPromoDto(0, pack.PaqueteId, pack.PromocionId, pack.Promocion, pack.Descuento, pack.DescuentoPorcentaje));
                }
            }

            foreach (var study in studiesDto)
            {
                var st = studiesParams.FirstOrDefault(x => x.Id == study.EstudioId);
                if (st == null) continue;

                study.Parametros = st.Parametros;
                study.Indicaciones = st.Indicaciones;

                var promos = studiesPromos.Where(x => x.EstudioId == study.EstudioId).ToList();
                study.Promociones = promos;

                if (study.PromocionId != null && !promos.Any(x => x.PromocionId == study.PromocionId))
                {
                    study.Promociones.Add(new PriceListInfoPromoDto(study.EstudioId, 0, study.PromocionId, study.Promocion, study.Descuento, study.DescuentoPorcentaje));
                }
            }

            var totals = quotation.ToQuotationTotalDto();

            var data = new QuotationStudyUpdateDto()
            {
                Paquetes = packsDto,
                Estudios = studiesDto,
                Total = totals,
            };

            return data;
        }

        public async Task<string> Create(QuotationDto quotationDto)
        {
            var code = await GetNewCode(quotationDto);

            quotationDto.Clave = code;
            var newQuotation = quotationDto.ToModel();
            newQuotation.MedicoId = null; //MEDICS.A_QUIEN_CORRESPONDA;
            newQuotation.CompañiaId = null; //COMPANIES.PARTICULARES;
            newQuotation.CargoTipo = CANTIDAD;
            newQuotation.UsuarioCreoId = quotationDto.UsuarioId;
            newQuotation.UsuarioCreo = quotationDto.Usuario;
            newQuotation.ExpedienteId = quotationDto.ExpedienteId;

            await _repository.Create(newQuotation);

            return newQuotation.Id.ToString();
        }

        public async Task<string> ConvertToRequest(Guid quotationId, Guid userId, string userName)
        {
            var quotation = await _repository.GetById(quotationId);

            if (quotation == null)
            {
                throw new CustomException(HttpStatusCode.NotFound, SharedResponses.NotFound);
            }

            if (quotation.ExpedienteId == null)
            {
                throw new CustomException(HttpStatusCode.BadRequest, "La cotización debe tener un expediente asignado");
            }

            var requestInfo = quotation.ToRequestConvertDto(userId, userName);

            var requestId = await _requestApplication.ConvertToRequest(requestInfo);

            return requestId;
        }

        public async Task UpdateGeneral(QuotationGeneralDto quotationDto)
        {
            var quotation = await GetExistingQuotation(quotationDto.CotizacionId);

            quotation.Procedencia = quotationDto.Procedencia;
            quotation.CompañiaId = quotationDto.CompañiaId;
            quotation.MedicoId = quotationDto.MedicoId;
            quotation.EnvioCorreo = quotationDto.Correo;
            quotation.EnvioWhatsApp = quotationDto.WhatsApp;
            quotation.Observaciones = quotationDto.Observaciones;
            quotation.UsuarioModificoId = quotationDto.UsuarioId;
            quotation.FechaModifico = DateTime.Now;

            await _repository.Update(quotation);
        }

        public async Task AssignRecord(Guid quotationId, Guid? recordId, Guid userId)
        {
            var quotation = await GetExistingQuotation(quotationId);

            quotation.ExpedienteId = recordId;
            quotation.UsuarioModificoId = userId;
            quotation.FechaModifico = DateTime.Now;

            await _repository.Update(quotation);
        }

        public async Task SendTestEmail(QuotationSendDto quotationDto)
        {
            var quotation = await GetExistingQuotation(quotationDto.CotizacionId);

            var subject = QuotationTemplates.Subjects.TestMessage;
            var title = QuotationTemplates.Titles.QuotationCode(quotation.Clave);
            var message = QuotationTemplates.Messages.TestMessage;

            var emailToSend = new EmailContract(quotationDto.Correo, null, subject, title, message)
            {
                Notificar = true,
                RemitenteId = quotationDto.UsuarioId.ToString()
            };

            var endpoint = await _sendEndpointProvider.GetSendEndpoint(new Uri(string.Concat(_rabbitMQSettings.Host, "/", _queueNames.Email)));

            await endpoint.Send(emailToSend);
        }

        public async Task SendTestWhatsapp(QuotationSendDto quotationDto)
        {
            _ = await GetExistingQuotation(quotationDto.CotizacionId);

            var message = QuotationTemplates.Messages.TestMessage;

            var phone = quotationDto.Telefono.Replace("-", "");
            phone = phone.Length == 10 ? "52" + phone : phone;
            var emailToSend = new WhatsappContract(phone, message)
            {
                Notificar = true,
                RemitenteId = quotationDto.UsuarioId.ToString()
            };

            var endpoint = await _sendEndpointProvider.GetSendEndpoint(new Uri(string.Concat(_rabbitMQSettings.Host, "/", _queueNames.Whatsapp)));

            await endpoint.Send(emailToSend);
        }

        public async Task UpdateTotals(QuotationTotalDto quotationDto)
        {
            var quotation = await GetExistingQuotation(quotationDto.CotizacionId);

            await _repository.Update(quotation);
        }

        public async Task UpdateStudies(QuotationStudyUpdateDto quotationDto)
        {
            try
            {
                _transaction.BeginTransaction();

                var quotation = await GetExistingQuotation(quotationDto.CotizacionId);

                var studiesDto = quotationDto.Estudios ?? new List<QuotationStudyDto>();
                var packStudiesDto = new List<QuotationStudyDto>();

                if (quotationDto.Paquetes != null)
                {
                    if (quotationDto.Paquetes.Any(x => x.Estudios == null || x.Estudios.Count == 0))
                    {
                        throw new CustomException(HttpStatusCode.BadRequest, RecordResponses.Quotation.PackWithoutStudies);
                    }

                    packStudiesDto = quotationDto.Paquetes.SelectMany(x =>
                    {
                        foreach (var item in x.Estudios)
                        {
                            item.PaqueteId = x.PaqueteId;
                        }

                        return x.Estudios;
                    }).ToList();
                }

                studiesDto.AddRange(packStudiesDto);

                var currentPacks = await _repository.GetPacksByQuotation(quotationDto.CotizacionId);

                var currentSudies = await _repository.GetStudiesByQuotation(quotationDto.CotizacionId);

                var packs = quotationDto.Paquetes.ToModel(quotationDto.CotizacionId, currentPacks, quotationDto.UsuarioId);

                await _repository.BulkInsertUpdatePacks(quotationDto.CotizacionId, packs);

                var studies = studiesDto.ToModel(quotationDto.CotizacionId, currentSudies, quotationDto.UsuarioId);

                await _repository.BulkInsertUpdateStudies(quotationDto.CotizacionId, studies);

                quotation.TotalEstudios = quotationDto.Total.TotalEstudios;
                quotation.Cargo = quotationDto.Total.Cargo;
                quotation.CargoTipo = quotationDto.Total.CargoTipo;
                quotation.Total = quotationDto.Total.Total;
                quotation.UsuarioModificoId = quotationDto.UsuarioId;
                quotation.FechaModifico = DateTime.Now;

                await _repository.Update(quotation);

                _transaction.CommitTransaction();
            }
            catch (Exception)
            {
                _transaction.RollbackTransaction();
                throw;
            }
        }

        public async Task CancelQuotation(Guid quotationId, Guid userId)
        {
            var quotation = await GetExistingQuotation(quotationId);

            if (quotation.EstatusId == Status.Quotation.Cancelado)
            {
                throw new CustomException(HttpStatusCode.BadGateway, RecordResponses.Quotation.AlreadyCancelled);
            }

            quotation.EstatusId = Status.Quotation.Cancelado;
            quotation.UsuarioModificoId = userId;
            quotation.FechaModifico = DateTime.Now;

            await _repository.Update(quotation);
        }

        public async Task DeleteStudies(QuotationStudyUpdateDto quotationDto)
        {
            var quotation = await GetExistingQuotation(quotationDto.CotizacionId);

            var studiesIds = quotationDto.Estudios.Select(x => x.Id);

            var studies = await _repository.GetStudyById(quotationDto.CotizacionId, studiesIds);

            if (studies == null || studies.Count == 0)
            {
                throw new CustomException(HttpStatusCode.BadRequest, RecordResponses.Quotation.NoStudySelected);
            }

            await _repository.BulkDeleteStudies(Guid.NewGuid(), studies);
        }

        public async Task<byte[]> PrintQuotation(Guid id)
        {
            var quotation = await _repository.GetById(id);

            if (quotation == null)
            {
                throw new CustomException(HttpStatusCode.NotFound, SharedResponses.NotFound);
            }

            //var order = quotation.ToQuotationOrderDto();

            return new byte[0x00];
        }

        private async Task<Quotation> GetExistingQuotation(Guid id)
        {
            var quotation = await _repository.FindAsync(id);

            if (quotation == null)
            {
                throw new CustomException(HttpStatusCode.NotFound, SharedResponses.NotFound);
            }

            return quotation;
        }

        private async Task<string> GetNewCode(QuotationDto quotationDto)
        {
            var date = DateTime.Now.ToString("ddMMyy");

            var branch = await _branchRepository.GetOne(x => x.Id == quotationDto.SucursalId);
            var lastCode = await _repository.GetLastCode(quotationDto.SucursalId, date);

            var consecutive = Codes.GetCode(branch.Clinicos, lastCode);
            var code = $"{consecutive}{date}";
            return code;
        }

        public async Task DeactivateQuotation(Guid quotationId)
        {
            var quotation = await _repository.GetById(quotationId);

            quotation.Activo = false;

            await _repository.Update(quotation);
        }

        


        //public async Task<(byte[] file, string fileName)> ExportList(QuotationFilterDto search)
        //{
        //    var studys = await GetByFilter(search);

        //    var path = Assets.CotizacionList;

        //    var template = new XLTemplate(path);

        //    template.AddVariable("Direccion", "Avenida Humberto Lobo #555");
        //    template.AddVariable("Sucursal", "San Pedro Garza García, Nuevo León");
        //    template.AddVariable("Titulo", "Cotizaciones");
        //    template.AddVariable("Fecha", DateTime.Now.ToString("dd/MM/yyyy"));
        //    template.AddVariable("Cotizaciones", studys);

        //    template.Generate();

        //    var range = template.Workbook.Worksheet("Cotizaciones").Range("Cotizaciones");
        //    var table = template.Workbook.Worksheet("Cotizaciones").Range("$A$3:" + range.RangeAddress.LastAddress).CreateTable();
        //    table.Theme = XLTableTheme.TableStyleMedium2;

        //    template.Format();

        //    return (template.ToByteArray(), $"Catálogo de Cotizaciones.xlsx");
        //}

        //public async Task<(byte[] file, string fileName)> ExportForm(Guid id)
        //{
        //    var study = await GetById(id);

        //    var path = Assets.CotizacionForm;

        //    var template = new XLTemplate(path);
        //    template.AddVariable("Direccion", "Avenida Humberto Lobo #555");
        //    template.AddVariable("Sucursal", "San Pedro Garza García, Nuevo León");
        //    template.AddVariable("Titulo", "Cotizacion");
        //    template.AddVariable("Fecha", DateTime.Now.ToString("dd/MM/yyyy"));
        //    template.AddVariable("Cotizacion", study);

        //    template.Generate();

        //    template.Format();

        //    return (template.ToByteArray(), $"Catálogo de Cotizacion ({study.nomprePaciente}).xlsx");
        //}
    }
}
