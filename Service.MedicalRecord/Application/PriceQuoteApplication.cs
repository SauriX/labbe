using ClosedXML.Excel;
using ClosedXML.Report;
using EventBus.Messages.Common;
using MassTransit;
using Service.MedicalRecord.Application.IApplication;
using Service.MedicalRecord.Client;
using Service.MedicalRecord.Client.IClient;
using Service.MedicalRecord.Dictionary;
using Service.MedicalRecord.Domain.Catalogs;
using Service.MedicalRecord.Domain.PriceQuote;
using Service.MedicalRecord.Dtos.MedicalRecords;
using Service.MedicalRecord.Dtos.PriceQuote;
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

namespace Service.MedicalRecord.Application
{
    public class PriceQuoteApplication : IPriceQuoteApplication
    {
        private readonly ITransactionProvider _transaction;
        private readonly IPriceQuoteRepository _repository;
        private readonly ICatalogClient _catalogClient;
        private readonly IPdfClient _pdfClient;
        private readonly ISendEndpointProvider _sendEndpointProvider;
        private readonly IRabbitMQSettings _rabbitMQSettings;
        private readonly IQueueNames _queueNames;
        private readonly IRepository<Branch> _branchRepository;

        private const byte PORCENTAJE = 1;
        private const byte CANTIDAD = 2;

        public PriceQuoteApplication(
            ITransactionProvider transaction,
            IPriceQuoteRepository repository,
            ICatalogClient catalogClient,
            IPdfClient pdfClient,
            ISendEndpointProvider sendEndpointProvider,
            IRabbitMQSettings rabbitMQSettings,
            IQueueNames queueNames,
            IRepository<Branch> branchRepository)
        {
            _transaction = transaction;
            _repository = repository;
            _catalogClient = catalogClient;
            _pdfClient = pdfClient;
            _sendEndpointProvider = sendEndpointProvider;
            _rabbitMQSettings = rabbitMQSettings;
            _queueNames = queueNames;
            _branchRepository = branchRepository;
        }

        public async Task<IEnumerable<PriceQuoteInfoDto>> GetByFilter(PriceQuoteFilterDto filter)
        {
            var expedientes = await _repository.GetByFilter(filter);

            return expedientes.ToPriceQuoteInfoDto();
        }

        public async Task<IEnumerable<PriceQuoteInfoDto>> GetActive()
        {
            var expedientes = await _repository.GetActive();

            return expedientes.ToPriceQuoteInfoDto();
        }

        public async Task<PriceQuoteDto> GetById(Guid id)
        {
            var expediente = await _repository.GetById(id);

            return expediente.ToPriceQuoteDto();
        }

        public async Task<PriceQuoteGeneralDto> GetGeneral(Guid id)
        {
            var priceQuote = await GetExistingPriceQuote(id);

            return priceQuote.ToPriceQuoteGeneralDto();
        }

        public async Task<PriceQuoteStudyUpdateDto> GetStudies(Guid priceQuoteId)
        {
            var priceQuote = await GetExistingPriceQuote(priceQuoteId);

            //var packs = await _repository.GetPacksByPriceQuote(priceQuote.Id);
            //var packsDto = packs.ToPriceQuotePackDto();

            //var studies = await _repository.GetStudiesByPriceQuote(priceQuote.Id);
            //var studiesDto = studies.ToPriceQuoteStudyDto();

            //var ids = studiesDto.Select(x => x.EstudioId).Concat(packsDto.SelectMany(y => y.Estudios).Select(y => y.EstudioId)).Distinct().ToList();
            //var studiesParams = await _catalogClient.GetStudies(ids);

            //foreach (var pack in packsDto)
            //{
            //    foreach (var study in pack.Estudios)
            //    {
            //        var st = studiesParams.FirstOrDefault(x => x.Id == study.EstudioId);
            //        if (st == null) continue;

            //        study.Parametros = st.Parametros;
            //        study.Indicaciones = st.Indicaciones;
            //    }
            //}

            //foreach (var study in studiesDto)
            //{
            //    var st = studiesParams.FirstOrDefault(x => x.Id == study.EstudioId);
            //    if (st == null) continue;

            //    study.Parametros = st.Parametros;
            //    study.Indicaciones = st.Indicaciones;
            //}

            //var totals = priceQuote.ToPriceQuoteTotalDto();

            var data = new PriceQuoteStudyUpdateDto()
            {
                //Paquetes = packsDto,
                //Estudios = studiesDto,
                //  Total = totals,
            };

            return data;
        }

        public async Task<string> Create(PriceQuoteDto priceQuoteDto)
        {
            var code = await GetNewCode(priceQuoteDto);

            //priceQuoteDto.Clave = code;
            var newPriceQuote = priceQuoteDto.ToModel();
            //newPriceQuote.MedicoId = null; //MEDICS.A_QUIEN_CORRESPONDA;
            //newPriceQuote.CompañiaId = null; //COMPANIES.PARTICULARES;
            //newPriceQuote.CargoTipo = CANTIDAD;
            //newPriceQuote.UsuarioCreoId = priceQuoteDto.UsuarioId;
            //newPriceQuote.UsuarioCreo = priceQuoteDto.Usuario;

            await _repository.Create(newPriceQuote);

            return newPriceQuote.Id.ToString();
        }

        public async Task UpdateGeneral(PriceQuoteGeneralDto priceQuoteDto)
        {
            var priceQuote = await GetExistingPriceQuote(priceQuoteDto.CotizacionId);

            //priceQuote.Procedencia = priceQuoteDto.Procedencia;
            //priceQuote.CompañiaId = priceQuoteDto.CompañiaId;
            //priceQuote.MedicoId = priceQuoteDto.MedicoId;
            //priceQuote.Afiliacion = priceQuoteDto.Afiliacion;
            //priceQuote.Urgencia = priceQuoteDto.Urgencia;
            //priceQuote.EnvioCorreo = priceQuoteDto.Correo;
            //priceQuote.EnvioWhatsApp = priceQuoteDto.Whatsapp;
            //priceQuote.Observaciones = priceQuoteDto.Observaciones;
            //priceQuote.UsuarioModificoId = priceQuoteDto.UsuarioId;
            //priceQuote.FechaModifico = DateTime.Now;

            await _repository.Update(priceQuote);
        }

        public async Task SendTestEmail(PriceQuoteSendDto priceQuoteDto)
        {
            var priceQuote = await GetExistingPriceQuote(priceQuoteDto.CotizacionId);

            //var subject = PriceQuoteTemplates.Subjects.TestMessage;
            //var title = PriceQuoteTemplates.Titles.PriceQuoteCode(priceQuote.Clave);
            //var message = PriceQuoteTemplates.Messages.TestMessage;

            //var emailToSend = new EmailContract(priceQuoteDto.Correo, null, subject, title, message)
            //{
            //    Notificar = true,
            //    RemitenteId = priceQuoteDto.UsuarioId.ToString()
            //};

            //var endpoint = await _sendEndpointProvider.GetSendEndpoint(new Uri(string.Concat(_rabbitMQSettings.Host, "/", _queueNames.Email)));

            //await endpoint.Send(emailToSend);
        }

        public async Task SendTestWhatsapp(PriceQuoteSendDto priceQuoteDto)
        {
            _ = await GetExistingPriceQuote(priceQuoteDto.CotizacionId);

            //var message = PriceQuoteTemplates.Messages.TestMessage;

            //var phone = priceQuoteDto.Telefono.Replace("-", "");
            //phone = phone.Length == 10 ? "52" + phone : phone;
            //var emailToSend = new WhatsappContract(phone, message)
            //{
            //    Notificar = true,
            //    RemitenteId = priceQuoteDto.UsuarioId.ToString()
            //};

            //var endpoint = await _sendEndpointProvider.GetSendEndpoint(new Uri(string.Concat(_rabbitMQSettings.Host, "/", _queueNames.Whatsapp)));

            //await endpoint.Send(emailToSend);
        }

        public async Task UpdateTotals(PriceQuoteTotalDto priceQuoteDto)
        {
            var priceQuote = await GetExistingPriceQuote(priceQuoteDto.CotizacionId);

            await _repository.Update(priceQuote);
        }

        public async Task UpdateStudies(PriceQuoteStudyUpdateDto priceQuoteDto)
        {
            try
            {
                _transaction.BeginTransaction();

                var priceQuote = await GetExistingPriceQuote(priceQuoteDto.CotizacionId);

                //var studiesDto = priceQuoteDto.Estudios ?? new List<PriceQuoteStudyDto>();
                //var packStudiesDto = new List<PriceQuoteStudyDto>();

                //if (priceQuoteDto.Paquetes != null)
                //{
                //    if (priceQuoteDto.Paquetes.Any(x => x.Estudios == null || x.Estudios.Count == 0))
                //    {
                //        throw new CustomException(HttpStatusCode.BadPriceQuote, RecordResponses.PriceQuote.PackWithoutStudies);
                //    }

                //    packStudiesDto = priceQuoteDto.Paquetes.SelectMany(x =>
                //    {
                //        foreach (var item in x.Estudios)
                //        {
                //            item.PaqueteId = x.PaqueteId;
                //        }

                //        return x.Estudios;
                //    }).ToList();
                //}

                //studiesDto.AddRange(packStudiesDto);

                //var currentPacks = await _repository.GetPacksByPriceQuote(priceQuoteDto.CotizacionId);

                //var currentSudies = await _repository.GetStudiesByPriceQuote(priceQuoteDto.CotizacionId);

                //var packs = priceQuoteDto.Paquetes.ToModel(priceQuoteDto.CotizacionId, currentPacks, priceQuoteDto.UsuarioId);

                //await _repository.BulkUpdateDeletePacks(priceQuoteDto.CotizacionId, packs);

                //var studies = studiesDto.ToModel(priceQuoteDto.CotizacionId, currentSudies, priceQuoteDto.UsuarioId);

                //await _repository.BulkUpdateDeleteStudies(priceQuoteDto.CotizacionId, studies);

                //priceQuote.TotalEstudios = priceQuoteDto.Total.TotalEstudios;
                //priceQuote.Cargo = priceQuoteDto.Total.Cargo;
                //priceQuote.CargoTipo = priceQuoteDto.Total.CargoTipo;
                //priceQuote.Total = priceQuoteDto.Total.Total;
                //priceQuote.UsuarioModificoId = priceQuoteDto.UsuarioId;
                //priceQuote.FechaModifico = DateTime.Now;

                await _repository.Update(priceQuote);

                _transaction.CommitTransaction();
            }
            catch (Exception)
            {
                _transaction.RollbackTransaction();
                throw;
            }
        }

        public async Task CancelPriceQuote(Guid priceQuoteId, Guid userId)
        {
            var priceQuote = await GetExistingPriceQuote(priceQuoteId);

            //if (priceQuote.EstatusId == Status.PriceQuote.Cancelado)
            //{
            //    throw new CustomException(HttpStatusCode.BadGateway, RecordResponses.PriceQuote.AlreadyCancelled);
            //}

            //priceQuote.EstatusId = Status.PriceQuote.Cancelado;
            //priceQuote.UsuarioModificoId = userId;
            //priceQuote.FechaModifico = DateTime.Now;

            await _repository.Update(priceQuote);
        }

        public async Task DeleteStudies(PriceQuoteStudyUpdateDto priceQuoteDto)
        {
            var priceQuote = await GetExistingPriceQuote(priceQuoteDto.CotizacionId);

            var studiesIds = priceQuoteDto.Estudios.Select(x => x.Id);

            var studies = await _repository.GetStudyById(priceQuoteDto.CotizacionId, studiesIds);

            if (studies == null || studies.Count == 0)
            {
                throw new CustomException(HttpStatusCode.BadRequest, RecordResponses.PriceQuote.NoStudySelected);
            }

            await _repository.BulkUpdateDeleteStudies(Guid.NewGuid(), studies);
        }

        public async Task<byte[]> PrinPriceQuote(Guid id)
        {
            var priceQuote = await _repository.GetById(id);

            if (priceQuote == null)
            {
                throw new CustomException(HttpStatusCode.NotFound, SharedResponses.NotFound);
            }

            //var order = priceQuote.ToPriceQuoteOrderDto();

            return new byte[0x00];
        }

        private async Task<PriceQuote> GetExistingPriceQuote(Guid id)
        {
            var priceQuote = await _repository.FindAsync(id);

            if (priceQuote == null)
            {
                throw new CustomException(HttpStatusCode.NotFound, SharedResponses.NotFound);
            }

            return priceQuote;
        }

        private async Task<string> GetNewCode(PriceQuoteDto priceQuoteDto)
        {
            var date = DateTime.Now.ToString("ddMMyy");

            var branch = await _branchRepository.GetOne(x => x.Id == priceQuoteDto.SucursalId);
            var lastCode = await _repository.GetLastCode(priceQuoteDto.SucursalId, date);

            var consecutive = Codes.GetCode(branch.Clinicos, lastCode);
            var code = $"{consecutive}{date}";
            return code;
        }

        //public async Task<(byte[] file, string fileName)> ExportList(PriceQuoteFilterDto search)
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
