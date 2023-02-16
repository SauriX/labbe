using EventBus.Messages.Common;
using MassTransit;
using Service.MedicalRecord.Application.IApplication;
using Service.MedicalRecord.Client.IClient;
using Service.MedicalRecord.Dictionary;
using Service.MedicalRecord.Dtos.Request;
using Service.MedicalRecord.Mapper;
using Service.MedicalRecord.Repository.IRepository;
using Service.MedicalRecord.Utils;
using Shared.Error;
using Shared.Extensions;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using SharedResponses = Shared.Dictionary.Responses;
using RecordResponses = Service.MedicalRecord.Dictionary.Response;
using Service.MedicalRecord.Settings.ISettings;
using Service.MedicalRecord.Transactions;
using RequestTemplates = Service.MedicalRecord.Dictionary.EmailTemplates.Request;
using Service.MedicalRecord.Domain.Request;
using AREAS = Shared.Dictionary.Catalogs.Area;
using COMPANIES = Shared.Dictionary.Catalogs.Company;
using MEDICS = Shared.Dictionary.Catalogs.Medic;
using ORIGIN = Shared.Dictionary.Catalogs.Origin;
using Service.MedicalRecord.Dtos.Promotion;
using Service.MedicalRecord.Domain.Catalogs;
using Service.MedicalRecord.Dtos.WeeClinic;
using Integration.WeeClinic.Dtos;
using Service.MedicalRecord.Dtos.Invoice;
using VT = Shared.Dictionary.Catalogs.ValueType;
using Service.MedicalRecord.Dtos.Quotation;
using Shared.Helpers;
using System.Text.Json;
using Service.MedicalRecord.Dtos.Catalogs;

namespace Service.MedicalRecord.Application
{
    public class RequestApplication : IRequestApplication
    {
        private readonly ITransactionProvider _transaction;
        private readonly IRequestRepository _repository;
        private readonly ICatalogClient _catalogClient;
        private readonly IPdfClient _pdfClient;
        private readonly ISendEndpointProvider _sendEndpointProvider;
        private readonly IRabbitMQSettings _rabbitMQSettings;
        private readonly IQueueNames _queueNames;
        private readonly IWeeClinicApplication _weeService;
        private readonly IRepository<Branch> _branchRepository;
        private readonly IMedicalRecordRepository _recordRepository;
        private readonly IBillingClient _billingClient;

        private const byte URGENCIA_CARGO = 3;

        public RequestApplication(
            ITransactionProvider transaction,
            IRequestRepository repository,
            ICatalogClient catalogClient,
            IPdfClient pdfClient,
            ISendEndpointProvider sendEndpoint,
            IRabbitMQSettings rabbitMQSettings,
            IQueueNames queueNames,
            IWeeClinicApplication weeService,
            IRepository<Branch> branchRepository,
            IMedicalRecordRepository recordRepository,
            IBillingClient billingClient)
        {
            _transaction = transaction;
            _repository = repository;
            _catalogClient = catalogClient;
            _pdfClient = pdfClient;
            _sendEndpointProvider = sendEndpoint;
            _queueNames = queueNames;
            _rabbitMQSettings = rabbitMQSettings;
            _weeService = weeService;
            _branchRepository = branchRepository;
            _recordRepository = recordRepository;
            _billingClient = billingClient;
        }

        public async Task<IEnumerable<RequestInfoDto>> GetByFilter(RequestFilterDto filter)
        {
            var request = await _repository.GetByFilter(filter);

            return request.ToRequestInfoDto();
        }

        public async Task<RequestDto> GetById(Guid recordId, Guid requestId)
        {
            var request = await _repository.GetById(requestId);

            if (request == null || request.ExpedienteId != recordId)
            {
                throw new CustomException(HttpStatusCode.NotFound, SharedResponses.NotFound);
            }

            return request.ToRequestDto();
        }

        public async Task<RequestGeneralDto> GetGeneral(Guid recordId, Guid requestId)
        {
            var request = await GetExistingRequest(recordId, requestId);

            return request.ToRequestGeneralDto();
        }

        public async Task<RequestStudyUpdateDto> GetStudies(Guid recordId, Guid requestId)
        {
            var request = await GetExistingRequest(recordId, requestId);

            var packs = await _repository.GetPacksByRequest(request.Id);
            var packsDto = packs.ToRequestPackDto();

            var studies = await _repository.GetStudiesByRequest(request.Id);
            var studiesDto = studies.ToRequestStudyDto();

            var ids = studiesDto.Select(x => x.EstudioId).Concat(packsDto.SelectMany(y => y.Estudios).Select(y => y.EstudioId)).Distinct().ToList();
            var studiesParams = await _catalogClient.GetStudies(ids);

            var packsPromoFilter = packs
                .Where(x => !x.Estudios.Any(s => s.EstatusId == Status.RequestStudy.Pendiente))
                .GroupBy(x => x.PaqueteId).Select(x => x.First())
                .Select(x => new PriceListInfoFilterDto(0, x.PaqueteId, request.SucursalId, (Guid)request.MedicoId, (Guid)request.CompañiaId, x.ListaPrecioId))
                .ToList();
            var studiesPromoFilter = studies
                .Where(x => x.EstatusId == Status.RequestStudy.Pendiente)
                .GroupBy(x => x.EstudioId).Select(x => x.First())
                .Select(x => new PriceListInfoFilterDto(x.EstudioId, 0, request.SucursalId, (Guid)request.MedicoId, (Guid)request.CompañiaId, x.ListaPrecioId))
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

                    study.Parametros = st.Parametros.Where(x => !x.TipoValor.In(VT.Observacion, VT.Etiqueta, VT.SinValor, VT.Texto, VT.Parrafo)).ToList();
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
                if (string.IsNullOrEmpty(request.FolioWeeClinic)) study.Asignado = true;

                var st = studiesParams.FirstOrDefault(x => x.Id == study.EstudioId);
                if (st == null) continue;

                var studyParamsDeserialized = JsonSerializer.Deserialize<List<ParameterListDto>>(JsonSerializer.Serialize(st.Parametros));

                study.Parametros = studyParamsDeserialized.Where(x => !x.TipoValor.In(VT.Observacion, VT.Etiqueta, VT.SinValor, VT.Texto, VT.Parrafo)).ToList();
                study.Indicaciones = st.Indicaciones;

                study.Tipo = st.Parametros.Count() > 0 ? "LABORATORIO" : "PATOLOGICO";

                var promos = studiesPromos.Where(x => x.EstudioId == study.EstudioId).ToList();
                study.Promociones = promos;

                if (study.PromocionId != null && !promos.Any(x => x.PromocionId == study.PromocionId))
                {
                    study.Promociones.Add(new PriceListInfoPromoDto(study.EstudioId, 0, study.PromocionId, study.Promocion, study.Descuento, study.DescuentoPorcentaje));
                }
            }

            var totals = request.ToRequestTotalDto();

            var data = new RequestStudyUpdateDto()
            {
                Paquetes = packsDto,
                Estudios = studiesDto,
                Total = totals,
            };

            return data;
        }

        public async Task<IEnumerable<RequestPaymentDto>> GetPayments(Guid recordId, Guid requestId)
        {
            await GetExistingRequest(recordId, requestId);

            var payments = await _repository.GetPayments(requestId);

            var paymentsDto = payments.ToRequestPaymentDto();

            return paymentsDto;
        }

        public async Task<IEnumerable<string>> GetImages(Guid recordId, Guid requestId)
        {
            var request = await GetExistingRequest(recordId, requestId);

            var images = await _repository.GetImages(requestId);

            var imagesNames = images.Select(x => x.Clave);

            return imagesNames;
        }

        public async Task<string> GetNextPaymentNumber(string serie)
        {
            var date = DateTime.Now.ToString("yy");

            var lastCode = await _repository.GetLastPaymentCode(serie, date);
            var consecutive = lastCode == null ? 1 : Convert.ToInt32(lastCode.Replace(date, "")) + 1;

            return $"{date}{consecutive:D5}";
        }

        public async Task<string> Create(RequestDto requestDto)
        {
            var record = await _recordRepository.Find(requestDto.ExpedienteId);

            if (record == null)
            {
                throw new CustomException(HttpStatusCode.NotFound, "El expediente no es válido");
            }

            var code = await GetNewCode(requestDto);

            requestDto.Clave = code;
            var newRequest = requestDto.ToModel();
            newRequest.MedicoId = MEDICS.A_QUIEN_CORRESPONDA;
            newRequest.CompañiaId = COMPANIES.PARTICULARES;
            newRequest.EnvioCorreo = record.Correo;
            newRequest.EnvioWhatsApp = record.Celular;
            newRequest.UsuarioCreoId = requestDto.UsuarioId;
            newRequest.UsuarioCreo = requestDto.Usuario;

            var series = await _catalogClient.GetBranchSeries(requestDto.SucursalId, 2);

            if (series != null && series.Count > 0)
            {
                var serie = series.OrderBy(x => x.Id).Last();
                var next = await GetNextPaymentNumber(serie.Clave);

                newRequest.Serie = serie.Clave;
                newRequest.SerieNumero = next;
            }

            await _repository.Create(newRequest);

            return newRequest.Id.ToString();
        }

        public async Task<string> CreateWeeClinic(RequestDto requestDto)
        {
            if (string.IsNullOrWhiteSpace(requestDto.FolioWeeClinic))
            {
                throw new CustomException(HttpStatusCode.BadRequest, "El Folio de WeeClinic es requerido");
            }

            var weeStudies = await _weeService.GetServicesByFolio(requestDto.FolioWeeClinic);
            weeStudies = weeStudies.Where(x => requestDto.Servicios.Contains(x.IdServicio)).ToList();

            var filter = new PriceListInfoFilterDto(0, 0, requestDto.SucursalId, MEDICS.A_QUIEN_CORRESPONDA, COMPANIES.PARTICULARES, Guid.Empty)
            {
                Estudios = weeStudies.Select(x => x.ClaveInterna).ToList()
            };

            var branch = await _branchRepository.GetOne(x => x.Id == requestDto.SucursalId);

            var labStudies = await _catalogClient.GetStudiesInfo(filter);

            var newRequest = requestDto.ToModel();
            var studies = labStudies.ToModel(newRequest.Id, new List<RequestStudy>(), requestDto.UsuarioId);

            var weePrices = new List<WeeServicePricesDto>();

            foreach (var study in studies)
            {
                var ws = weeStudies.FirstOrDefault(x => x.ClaveInterna.Trim() == study.Clave.Trim());

                var weePrice = await _weeService.GetServicePrice(ws.IdServicio, branch.Clave);
                weePrices.Add(weePrice);

                study.EstatusId = Status.RequestStudy.Pendiente;
                study.Precio = weePrice.Paciente.Total + weePrice.Aseguradora.Total;
                study.PrecioFinal = weePrice.Paciente.Total + weePrice.Aseguradora.Total;
                study.EstudioWeeClinic = new RequestStudyWee(ws.IdNodo, ws.IdServicio, ws.Cubierto, weePrice.Paciente.Total, weePrice.Aseguradora.Total, ws.IsAvaliable, ws.RestanteDays, ws.Vigencia, ws.IsCancel);
            }

            string code = await GetNewCode(requestDto);
            newRequest.Clave = code;

            newRequest.MedicoId = MEDICS.A_QUIEN_CORRESPONDA;
            newRequest.CompañiaId = COMPANIES.PARTICULARES;
            newRequest.UsuarioCreo = requestDto.Usuario;

            var weeData = weeStudies.First();

            newRequest.FolioWeeClinic = requestDto.FolioWeeClinic;
            newRequest.IdPersona = weeData.IdPersona;
            newRequest.IdOrden = weeData.IdOrden;
            newRequest.Estudios = studies;

            newRequest.TotalEstudios = weePrices.Sum(x => x.Paciente.Total + x.Aseguradora.Total);
            newRequest.Descuento = weePrices.Sum(x => x.Paciente.Descuento + x.Aseguradora.Descuento);
            newRequest.Cargo = 0;
            newRequest.Copago = weePrices.Sum(x => x.Paciente.Total);
            newRequest.Total = newRequest.TotalEstudios - (newRequest.TotalEstudios - newRequest.Copago);
            newRequest.Saldo = newRequest.Copago;
            newRequest.UsuarioModificoId = requestDto.UsuarioId;
            newRequest.FechaModifico = DateTime.Now;

            await _repository.Create(newRequest);

            return newRequest.Id.ToString();
        }

        public async Task<string> ConvertToRequest(RequestConvertDto requestDto)
        {
            try
            {
                _transaction.BeginTransaction();

                var request = new RequestDto(requestDto.ExpedienteId,
                    requestDto.SucursalId,
                    null,
                    null,
                    requestDto.UsuarioId);
                var id = await Create(request);

                var general = requestDto.General;
                general.SolicitudId = Guid.Parse(id);
                general.UsuarioId = requestDto.UsuarioId;
                general.Procedencia = 1;
                general.ExpedienteId = requestDto.ExpedienteId;
                general.Urgencia = 1;
                await UpdateGeneral(general);

                _transaction.CommitTransaction();

                var studies = new RequestStudyUpdateDto
                {
                    SolicitudId = Guid.Parse(id),
                    ExpedienteId = requestDto.ExpedienteId,
                    Paquetes = requestDto.Paquetes,
                    Estudios = requestDto.Estudios,
                    Total = new RequestTotalDto()
                    {
                        SolicitudId = Guid.Parse(id),
                        ExpedienteId = requestDto.ExpedienteId,
                    },
                    UsuarioId = requestDto.UsuarioId,
                };

                await UpdateStudies(studies);

                return id;
            }
            catch (Exception)
            {
                _transaction.RollbackTransaction();
                throw;
            }
        }

        public async Task<RequestPaymentDto> CreatePayment(RequestPaymentDto requestDto)
        {
            var request = await GetExistingRequest(requestDto.ExpedienteId, requestDto.SolicitudId);

            if (requestDto.Cantidad > request.Saldo)
            {
                throw new CustomException(HttpStatusCode.BadRequest, "La cantidad del pago no puede sobrepasar el saldo");
            }

            var newPayment = requestDto.ToModel();

            await _repository.CreatePayment(newPayment);

            await UpdateTotals(request.ExpedienteId, request.Id, requestDto.UsuarioId);

            return newPayment.ToRequestPaymentDto();
        }

        public async Task<IEnumerable<RequestPaymentDto>> CheckInPayment(RequestCheckInDto checkInDto)
        {
            if (checkInDto.Pagos == null || checkInDto.Pagos.Count == 0)
            {
                throw new CustomException(HttpStatusCode.BadRequest, RecordResponses.Request.NoPaymentSelected);
            }

            if (new bool[] { checkInDto.Desglozado, checkInDto.Simple, checkInDto.PorConcepto }.Count(x => x) != 1)
            {
                throw new CustomException(HttpStatusCode.BadRequest, "Debe seleccionar solo una opción entre Desglozado por estudio, Simple o Por Concepto");
            }

            var request = await GetExistingRequest(checkInDto.ExpedienteId, checkInDto.SolicitudId);

            var paymentsToCheckIn = await _repository.GetPayments(checkInDto.SolicitudId);
            paymentsToCheckIn = paymentsToCheckIn.Where(x => checkInDto.Pagos.Select(p => p.Id).Contains(x.Id) && x.EstatusId == Status.RequestPayment.Pagado).ToList();

            if (!paymentsToCheckIn.Any())
            {
                throw new CustomException(HttpStatusCode.BadRequest, RecordResponses.Request.NoPaymentSelected);
            }

            var maxPay = paymentsToCheckIn.OrderByDescending(x => x.Cantidad).FirstOrDefault();

            var totalQty = paymentsToCheckIn.Sum(x => x.Cantidad);

            if (checkInDto.Desglozado && totalQty != request.Total)
            {
                throw new CustomException(HttpStatusCode.BadRequest, "Para hacer una factura desglozada la suma de los pagos debe ser la totalidad de la solicitud");
            }

            var record = await _recordRepository.GetById(checkInDto.ExpedienteId);
            var taxData = record?.TaxData?.FirstOrDefault(x => x.FacturaID == checkInDto.DatoFiscalId)?.Factura;

            if (taxData == null)
            {
                throw new CustomException(HttpStatusCode.BadRequest, "Datos fiscales inválidos");
            }

            var invoiceDto = new InvoiceDto
            {
                Expediente = record.Expediente,
                ExpedienteId = checkInDto.ExpedienteId,
                Solicitud = request.Clave,
                SolicitudId = checkInDto.SolicitudId,
                Serie = checkInDto.Serie,
                UsoCFDI = checkInDto.UsoCFDI,
                FormaPago = maxPay.FormaPago,
                RegimenFiscal = taxData.RegimenFiscal,
                RFC = taxData.RFC,
                Paciente = record.NombreCompleto,
                ConNombre = checkInDto.Simple,
                Desglozado = checkInDto.Desglozado,
                EnvioCorreo = checkInDto.EnvioCorreo ? request.EnvioCorreo : null,
                EnvioWhatsapp = checkInDto.EnvioWhatsapp ? request.EnvioWhatsApp : null,
                Cliente = new ClientDto
                {
                    RFC = taxData.RFC,
                    RazonSocial = taxData.RazonSocial,
                    RegimenFiscal = taxData.RegimenFiscal,
                    Correo = request.EnvioCorreo,
                    Telefono = request.EnvioWhatsApp,
                    CodigoPostal = taxData.CodigoPostal,
                    Pais = "MEX",
                    Estado = taxData.Estado,
                    Ciudad = taxData.Ciudad,
                    Municipio = taxData.Ciudad,
                    Colonia = taxData.ColoniaId.ToString(),
                    Calle = taxData.Calle,
                },
            };

            if (checkInDto.Simple || checkInDto.PorConcepto)
            {
                if (checkInDto.Detalle.Count != 1)
                {
                    throw new CustomException(HttpStatusCode.BadRequest, "Las facturas simples o por concepto solo deben tener un detalle");
                }

                invoiceDto.Productos = new List<ProductDto>
                {
                    new ProductDto
                    {
                        Clave = checkInDto.Detalle[0].Clave,
                        Descripcion = checkInDto.Detalle[0].Descripcion,
                        Precio = totalQty,
                        Cantidad = 1,
                        Descuento = 0,
                    }
                };
            }
            else
            {
                var studies = await _repository.GetStudiesByRequest(checkInDto.SolicitudId);
                var packs = await _repository.GetPacksByRequest(checkInDto.SolicitudId);

                invoiceDto.Productos = new List<ProductDto>();
                invoiceDto.Productos.AddRange(studies.GroupBy(x => new { x.Clave, x.PrecioFinal }).Select(x => new ProductDto
                {
                    Clave = x.Key.Clave,
                    Descripcion = x.First().Nombre,
                    Precio = x.First().Precio,
                    Cantidad = x.Count(),
                    Descuento = x.First().Descuento * x.Count(),
                }));
                invoiceDto.Productos.AddRange(packs.GroupBy(x => new { x.Clave, x.PrecioFinal }).Select(x => new ProductDto
                {
                    Clave = x.Key.Clave,
                    Descripcion = x.First().Nombre,
                    Precio = x.First().Precio,
                    Cantidad = x.Count(),
                    Descuento = x.First().Descuento * x.Count(),
                }));
            }

            var invoiceResponse = await _billingClient.CheckInPayment(invoiceDto);

            foreach (var payment in paymentsToCheckIn)
            {
                payment.EstatusId = Status.RequestPayment.Facturado;
                payment.FacturaId = invoiceResponse.Id;
                payment.SerieFactura = invoiceResponse.Serie + " " + invoiceResponse.SerieNumero;
                payment.FacturapiId = invoiceResponse.FacturapiId;
                payment.UsuarioModificoId = checkInDto.UsuarioId;
                payment.FechaModifico = DateTime.Now;
            }

            await _repository.BulkUpdatePayments(checkInDto.SolicitudId, paymentsToCheckIn);

            var checkedIn = paymentsToCheckIn.ToRequestPaymentDto();

            return checkedIn;
        }

        public async Task<string> UpdateSeries(RequestDto requestDto)
        {
            var request = await GetExistingRequest(requestDto.ExpedienteId, (Guid)requestDto.SolicitudId);

            if (string.IsNullOrWhiteSpace(requestDto.Serie))
            {
                throw new CustomException(HttpStatusCode.BadRequest, "Debe seleccionar una serie");
            }

            var series = await _catalogClient.GetBranchSeries(request.SucursalId, 2);

            if (!series.Select(x => x.Clave).Contains(requestDto.Serie))
            {
                throw new CustomException(HttpStatusCode.BadRequest, "La serie no es valida");
            }

            var next = await GetNextPaymentNumber(requestDto.Serie);

            request.Serie = requestDto.Serie;
            request.SerieNumero = next;
            request.UsuarioModificoId = requestDto.UsuarioId;
            request.FechaModifico = DateTime.Now;

            await _repository.Update(request);

            return next;
        }

        public async Task UpdateGeneral(RequestGeneralDto requestDto)
        {
            var request = await GetExistingRequest(requestDto.ExpedienteId, requestDto.SolicitudId);

            var prevOrigin = request.Procedencia;
            var prevUrgency = request.Urgencia;
            var prevCompany = request.CompañiaId;

            if (!string.IsNullOrEmpty(requestDto.Whatsapp))
            {
                requestDto.Whatsapp = string.Join(",", requestDto.Whatsapp.Split(",").Select(x => Helpers.AddNumberSeparator(x)));
            }

            request.Procedencia = requestDto.Procedencia;
            request.CompañiaId = requestDto.CompañiaId;
            request.MedicoId = requestDto.MedicoId;
            request.Afiliacion = requestDto.Afiliacion;
            request.Urgencia = requestDto.Urgencia;
            request.EnvioCorreo = requestDto.Correo;
            request.EnvioWhatsApp = requestDto.Whatsapp;
            request.EnvioMedico = requestDto.EnvioMedico;
            request.Observaciones = requestDto.Observaciones;
            request.UsuarioModificoId = requestDto.UsuarioId;
            request.FechaModifico = DateTime.Now;

            await _repository.Update(request);

            if (requestDto.Procedencia != prevOrigin || requestDto.Urgencia != prevUrgency)
            {
                await UpdateTotals(requestDto.ExpedienteId, requestDto.SolicitudId, requestDto.UsuarioId);
            }

            if (requestDto.CompañiaId != prevCompany)
            {
                await UpdateStudies(new RequestStudyUpdateDto(requestDto.ExpedienteId, requestDto.SolicitudId, requestDto.UsuarioId), isDeleting: true);
            }
        }

        public async Task SendTestEmail(RequestSendDto requestDto)
        {
            var request = await GetExistingRequest(requestDto.ExpedienteId, requestDto.SolicitudId);

            var subject = RequestTemplates.Subjects.TestMessage;
            var title = RequestTemplates.Titles.RequestCode(request.Clave);
            var message = RequestTemplates.Messages.TestMessage;

            var emailToSend = new EmailContract(requestDto.Correos, subject, title, message)
            {
                Notificar = true,
                RemitenteId = requestDto.UsuarioId.ToString(),
                CorreoIndividual = true
            };

            var endpoint = await _sendEndpointProvider.GetSendEndpoint(new Uri(string.Concat(_rabbitMQSettings.Host, "/", _queueNames.Email)));

            await endpoint.Send(emailToSend);
        }

        public async Task SendTestWhatsapp(RequestSendDto requestDto)
        {
            _ = await GetExistingRequest(requestDto.ExpedienteId, requestDto.SolicitudId);

            var message = RequestTemplates.Messages.TestMessage;

            var phones = requestDto.Telefonos.Select(x =>
            {
                x = ("52" + x.Replace("-", ""))[^12..];
                return x;
            }).ToList();

            var emailToSend = new WhatsappContract(phones, message)
            {
                Notificar = true,
                RemitenteId = requestDto.UsuarioId.ToString()
            };

            var endpoint = await _sendEndpointProvider.GetSendEndpoint(new Uri(string.Concat(_rabbitMQSettings.Host, "/", _queueNames.Whatsapp)));

            await endpoint.Send(emailToSend);
        }

        public async Task UpdateTotals(RequestTotalDto requestDto)
        {
            var request = await GetExistingRequest(requestDto.ExpedienteId, requestDto.SolicitudId);

            await _repository.Update(request);
        }

        public async Task<RequestStudyUpdateDto> UpdateStudies(RequestStudyUpdateDto requestDto, bool isDeleting = false)
        {
            try
            {
                _transaction.BeginTransaction();

                var request = await GetExistingRequest(requestDto.ExpedienteId, requestDto.SolicitudId);

                if ((requestDto.Estudios == null || requestDto.Estudios.Count == 0)
                    && (requestDto.Paquetes == null || requestDto.Paquetes.Count == 0)
                    && !isDeleting)
                {
                    throw new CustomException(HttpStatusCode.BadRequest, "Debe agregar por lo menos un estudio o paquete");
                }

                var studiesDto = requestDto.Estudios ?? new List<RequestStudyDto>();
                var packStudiesDto = new List<RequestStudyDto>();

                if (requestDto.Paquetes != null && requestDto.Paquetes.Any())
                {
                    if (requestDto.Paquetes.Any(x => x.Estudios == null || x.Estudios.Count == 0))
                    {
                        throw new CustomException(HttpStatusCode.BadRequest, RecordResponses.Request.PackWithoutStudies);
                    }

                    packStudiesDto = requestDto.Paquetes.SelectMany(x =>
                    {
                        foreach (var item in x.Estudios)
                        {
                            item.PaqueteId = x.PaqueteId;
                        }

                        return x.Estudios;
                    }).ToList();
                }

                studiesDto.AddRange(packStudiesDto);

                var currentPacks = await _repository.GetPacksByRequest(requestDto.SolicitudId);
                var currentSudies = await _repository.GetStudiesByRequest(requestDto.SolicitudId);

                var packs = requestDto.Paquetes.ToModel(requestDto.SolicitudId, currentPacks, requestDto.UsuarioId);
                await _repository.BulkUpdateDeletePacks(requestDto.SolicitudId, packs);

                var studies = studiesDto.ToModel(requestDto.SolicitudId, currentSudies, requestDto.UsuarioId);
                await _repository.BulkUpdateDeleteStudies(requestDto.SolicitudId, studies);

                var pathologicalCode = await GeneratePathologicalCode(request);
                request.ClavePatologica = pathologicalCode;

                await _repository.Update(request);

                await UpdateTotals(request.ExpedienteId, request.Id, requestDto.UsuarioId);

                _transaction.CommitTransaction();

                AssignStudiesIds(requestDto, packs, studies);

                return requestDto;
            }
            catch (Exception)
            {
                _transaction.RollbackTransaction();
                throw;
            }
        }

        private static void AssignStudiesIds(RequestStudyUpdateDto requestDto, List<RequestPack> packs, List<RequestStudy> studies)
        {
            foreach (var pack in requestDto.Paquetes.Where(x => x.Id == 0))
            {
                var newPack = packs.FirstOrDefault(x => x.PaqueteId == pack.PaqueteId && !requestDto.Paquetes.Select(p => p.Id).Contains(x.Id));
                pack.Id = newPack.Id;
                packs.Remove(newPack);

                foreach (var study in pack.Estudios)
                {
                    var newStudy = studies.FirstOrDefault(x => x.EstudioId == study.EstudioId && x.PaqueteId == pack.Id);
                    study.Id = newStudy.Id;
                    studies.Remove(newStudy);
                }
            }

            foreach (var study in requestDto.Estudios.Where(x => x.Id == 0))
            {
                var newStudy = studies.FirstOrDefault(x => x.EstudioId == study.EstudioId && !requestDto.Estudios.Select(p => p.Id).Contains(x.Id));
                study.Id = newStudy.Id;
                studies.Remove(newStudy);
            }
        }

        public async Task CancelRequest(Guid recordId, Guid requestId, Guid userId)
        {
            var request = await GetExistingRequest(recordId, requestId);

            if (request.EstatusId == Status.Request.Cancelado)
            {
                throw new CustomException(HttpStatusCode.BadGateway, RecordResponses.Request.AlreadyCancelled);
            }

            if (request.EstatusId == Status.Request.Completado)
            {
                throw new CustomException(HttpStatusCode.BadGateway, RecordResponses.Request.AlreadyCompleted);
            }

            var studies = await _repository.GetAllStudies(requestId);

            if (studies.Any(x => x.EstatusId != Status.RequestStudy.Pendiente && x.EstatusId != Status.RequestStudy.Cancelado))
            {
                throw new CustomException(HttpStatusCode.BadRequest, RecordResponses.Request.ProcessingStudies);
            }

            request.EstatusId = Status.Request.Cancelado;
            request.UsuarioModificoId = userId;
            request.FechaModifico = DateTime.Now;

            await _repository.Update(request);
        }

        public async Task DeleteRequest(Guid recordId, Guid requestId)
        {
            var request = await _repository.GetById(requestId);

            if (request == null || request.ExpedienteId != recordId)
            {
                throw new CustomException(HttpStatusCode.NotFound, SharedResponses.NotFound);
            }

            if (request.Estudios.Any() || request.Paquetes.Any())
            {
                throw new CustomException(HttpStatusCode.BadRequest, "No es posible eliminar solicitudes con estudios");
            }

            await _repository.Delete(request);
        }

        public async Task CancelStudies(RequestStudyUpdateDto requestDto)
        {
            var request = await GetExistingRequest(requestDto.ExpedienteId, requestDto.SolicitudId);

            var studiesIds = requestDto.Estudios.Select(x => x.Id);

            var allStudies = await _repository.GetAllStudies(requestDto.SolicitudId);
            var studies = await _repository.GetStudyById(requestDto.SolicitudId, studiesIds);

            var groups = allStudies.Where(x => x.PaqueteId != null).GroupBy(x => x.Paquete.Nombre);

            foreach (var g in groups)
            {
                var st = (from gs in g.Where(x => x.EstatusId != Status.RequestStudy.Pendiente)
                          join s in studies on gs.Id equals s.Id
                          select s);

                if (st != null && st.Any())
                {
                    throw new CustomException(HttpStatusCode.BadRequest, $"No es posible cancelar ya que un estudio del paquete {g.Key} ya no esta pendiente");
                }
            }

            studies = studies.Where(x => x.EstatusId == Status.RequestStudy.Pendiente).ToList();

            if (studies == null || studies.Count == 0)
            {
                throw new CustomException(HttpStatusCode.BadRequest, RecordResponses.Request.NoStudySelected);
            }

            var branch = await _branchRepository.GetOne(x => x.Id == request.SucursalId);

            var weeServices = new List<WeeServiceDto>();

            foreach (var study in studies)
            {
                if (!string.IsNullOrEmpty(request.FolioWeeClinic))
                {
                    if (study.EstudioWeeClinic == null)
                    {
                        throw new CustomException(HttpStatusCode.BadRequest, $"El estudio {study.Clave} no contiene información de WeeClinic, favor de contactar a su administrador de sistema");
                    }

                    //if (study.EstudioWeeClinic.IsCancel == 0)
                    //{
                    //    throw new CustomException(HttpStatusCode.BadRequest, $"No es posible cancelar el estudio {study.Clave} a traves de WeeClinic");
                    //}

                    var weeCancelled = await _weeService.CancelService(study.EstudioWeeClinic.IdServicio, study.EstudioWeeClinic.IdNodo, branch.Clave);

                    if (!weeCancelled.Cancelado)
                    {
                        throw new CustomException(HttpStatusCode.BadRequest, $"No fue posible cancelar el estudio {study.Clave} a traves de WeeClinic");
                    }
                }

                study.EstatusId = Status.RequestStudy.Cancelado;
                study.UsuarioModificoId = requestDto.UsuarioId;
                study.FechaModifico = DateTime.Now;
            }

            await _repository.BulkInsertUpdateStudies(requestDto.SolicitudId, studies);

            await UpdateTotals(request.ExpedienteId, request.Id, requestDto.UsuarioId);
        }

        public async Task<List<RequestPaymentDto>> CancelPayment(Guid recordId, Guid requestId, List<RequestPaymentDto> paymentsDto)
        {
            var request = await GetExistingRequest(recordId, requestId);

            var payments = await _repository.GetPayments(requestId);

            var paymentsToCancel = payments.Where(x => paymentsDto.Select(p => p.Id).Contains(x.Id)
            && (x.EstatusId == Status.RequestPayment.Pagado || x.EstatusId == Status.RequestPayment.Facturado))
                .ToList();

            if (!paymentsToCancel.Any())
            {
                throw new CustomException(HttpStatusCode.BadRequest, RecordResponses.Request.NoPaymentSelected);
            }

            var userId = paymentsDto.First().UsuarioId;
            var userName = paymentsDto.First().UsuarioRegistra;

            foreach (var payment in paymentsToCancel)
            {
                payment.EstatusId = Status.RequestPayment.Cancelado;
                payment.UsuarioRegistra = userName;
                payment.UsuarioModificoId = userId;
                payment.FechaModifico = DateTime.Now;
            }

            await _repository.BulkUpdatePayments(requestId, paymentsToCancel);

            await UpdateTotals(request.ExpedienteId, request.Id, userId);

            foreach (var payment in paymentsDto)
            {
                payment.EstatusId = Status.RequestPayment.Cancelado;
            }

            return paymentsDto;
        }

        public async Task<int> SendStudiesToSampling(RequestStudyUpdateDto requestDto)
        {
            var request = await GetExistingRequest(requestDto.ExpedienteId, requestDto.SolicitudId);

            var studiesIds = requestDto.Estudios.Select(x => x.Id);
            var studies = await _repository.GetStudyById(requestDto.SolicitudId, studiesIds);

            studies = studies.Where(x => x.EstatusId == Status.RequestStudy.Pendiente || x.EstatusId == Status.RequestStudy.TomaDeMuestra).ToList();

            if (studies == null || studies.Count == 0)
            {
                throw new CustomException(HttpStatusCode.BadRequest, RecordResponses.Request.NoStudySelected);
            }

            foreach (var study in studies)
            {
                if (study.EstatusId == Status.RequestStudy.Pendiente)
                {
                    study.EstatusId = Status.RequestStudy.TomaDeMuestra;
                    study.FechaTomaMuestra = DateTime.Now;
                    study.UsuarioTomaMuestra = requestDto.Usuario;
                }
                else
                {
                    study.EstatusId = Status.RequestStudy.Pendiente;
                }

                study.UsuarioModificoId = requestDto.UsuarioId;
                study.FechaModifico = DateTime.Now;
            }

            await _repository.BulkInsertUpdateStudies(requestDto.SolicitudId, studies);
            ;
            return studies.Count;
        }

        public async Task<int> SendStudiesToRequest(RequestStudyUpdateDto requestDto)
        {
            var request = await GetExistingRequest(requestDto.ExpedienteId, requestDto.SolicitudId);

            var studiesIds = requestDto.Estudios.Select(x => x.Id);
            var studies = await _repository.GetStudyById(requestDto.SolicitudId, studiesIds);

            studies = studies.Where(x => x.EstatusId == Status.RequestStudy.TomaDeMuestra || x.EstatusId == Status.RequestStudy.Solicitado).ToList();

            if (studies == null || studies.Count == 0)
            {
                throw new CustomException(HttpStatusCode.BadRequest, RecordResponses.Request.NoStudySelected);
            }

            foreach (var study in studies)
            {
                if (study.EstatusId == Status.RequestStudy.TomaDeMuestra)
                {
                    study.EstatusId = Status.RequestStudy.Solicitado;
                    study.FechaSolicitado = DateTime.Now;
                    study.UsuarioSolicitado = requestDto.Usuario;
                }
                else
                {
                    study.EstatusId = Status.RequestStudy.TomaDeMuestra;
                }

                study.UsuarioModificoId = requestDto.UsuarioId;
                study.FechaModifico = DateTime.Now;
            }

            await _repository.BulkInsertUpdateStudies(requestDto.SolicitudId, studies);

            return studies.Count;
        }

        public async Task AddPartiality(RequestPartialityDto requestDto)
        {
            var request = await GetExistingRequest(requestDto.ExpedienteId, requestDto.SolicitudId);

            request.Parcialidad = requestDto.Aplicar;
            request.UsuarioModificoId = requestDto.UsuarioId;
            request.FechaModifico = DateTime.Now;

            await _repository.Update(request);
        }

        public async Task<byte[]> PrintTicket(Guid recordId, Guid requestId, string userName)
        {
            var request = await _repository.GetById(requestId);

            if (request == null || request.ExpedienteId != recordId)
            {
                throw new CustomException(HttpStatusCode.NotFound, SharedResponses.NotFound);
            }

            var payments = await _repository.GetPayments(requestId);

            payments = payments.Where(x => !x.EstatusId.In(Status.RequestPayment.Cancelado, Status.RequestPayment.FacturaCancelada)).ToList();

            var order = request.ToRequestTicketDto(payments, userName);

            return await _pdfClient.GenerateTicket(order);
        }

        public async Task<byte[]> PrintOrder(Guid recordId, Guid requestId, string userName)
        {
            var request = await _repository.GetById(requestId);

            if (request == null || request.ExpedienteId != recordId)
            {
                throw new CustomException(HttpStatusCode.NotFound, SharedResponses.NotFound);
            }

            var order = request.ToRequestOrderDto(userName);

            return await _pdfClient.GenerateOrder(order);
        }

        public async Task<byte[]> PrintTags(Guid recordId, Guid requestId, List<RequestTagDto> tags)
        {
            var request = await _repository.GetById(requestId);

            if (request == null || request.ExpedienteId != recordId)
            {
                throw new CustomException(HttpStatusCode.NotFound, SharedResponses.NotFound);
            }

            var printTags = await HandleTags(request, tags);

            return await _pdfClient.GenerateTags(printTags);
        }

        private async Task<List<RequestTagDto>> HandleTags(Request request, List<RequestTagDto> tags)
        {
            var requestDate = request.FechaCreo;
            var branch = await _catalogClient.GetBranch(request.SucursalId);
            var lastCode = await _repository.GetLastTagCode(requestDate.ToString("ddMMyy"));

            List<RequestTagDto> printTags = new();
            List<string> nameStudy = new();
            var sumTag = 0m;

            foreach (var tag in tags.OrderBy(x => x.Orden))
            {
                sumTag += tag.Cantidad;
                nameStudy.Add(tag.Estudios);

                if (sumTag > 0.5m && sumTag <= 1)
                {
                    var code = Codes.GetTagCode(request.EstatusId.ToString(), lastCode, requestDate);

                    tag.Clave = code;
                    tag.ClaveEtiqueta = code;
                    tag.Ciudad = branch.clave;
                    tag.Paciente = request.Expediente.NombreCompleto;
                    tag.EdadSexo = request.Expediente.Edad + " " + request.Expediente.Genero;

                    tag.Estudios = string.Join("\r\n", nameStudy);
                    tag.NombreInfo = tag.Estudios;
                    tag.Cantidad = sumTag;

                    sumTag = 0;
                    nameStudy = new();

                    var current = code[8..];
                    var next = Convert.ToInt32(current) + 1;
                    lastCode = code;

                    printTags.Add(tag);
                }
                else
                {
                    continue;
                }
            }

            var saveTags = printTags.ToRequestTag(request.Id);

            await _repository.BulkInsertUpdateTags(request.Id, saveTags);

            return printTags;
        }

        public async Task<string> SaveImage(RequestImageDto requestDto)
        {
            var request = await GetExistingRequest(requestDto.ExpedienteId, requestDto.SolicitudId);

            var typeOk = requestDto.Tipo.In("orden", "ine", "ineReverso", "formato");

            var isImage = requestDto.Imagen.IsImage();

            if (!typeOk || !isImage)
            {
                throw new CustomException(HttpStatusCode.BadRequest, SharedResponses.InvalidImage);
            }

            requestDto.Clave = request.Clave;

            if (requestDto.Tipo == "formato")
            {
                var fileName = requestDto.Imagen.FileName;
                var name = fileName[..fileName.LastIndexOf(".")];

                var existingImage = await _repository.GetImage(requestDto.SolicitudId, name);

                var path = await SaveImageGetPath(requestDto, existingImage?.Clave ?? name);

                var image = new RequestImage(existingImage?.Id ?? 0, requestDto.SolicitudId, existingImage?.Clave ?? name, path, "format")
                {
                    UsuarioCreoId = existingImage?.UsuarioCreoId ?? requestDto.UsuarioId,
                    FechaCreo = existingImage?.FechaCreo ?? DateTime.Now,
                    UsuarioModificoId = existingImage == null ? null : requestDto.UsuarioId,
                    FechaModifico = existingImage == null ? null : DateTime.Now
                };

                await _repository.UpdateImage(image);

                return image.Clave;
            }
            else
            {
                var path = await SaveImageGetPath(requestDto);

                if (requestDto.Tipo == "orden")
                {
                    request.RutaOrden = path;
                }
                else if (requestDto.Tipo == "ine")
                {
                    request.RutaINE = path;
                }
                else if (requestDto.Tipo == "ineReverso")
                {
                    request.RutaINEReverso = path;
                }

                request.UsuarioModificoId = requestDto.UsuarioId;
                request.FechaModifico = DateTime.Now;

                await _repository.Update(request);

                return request.Clave;
            }
        }

        public async Task DeleteImage(Guid recordId, Guid requestId, string code)
        {
            await GetExistingRequest(recordId, requestId);

            await _repository.DeleteImage(requestId, code);
        }

        public async Task<WeeTokenValidationDto> SendCompareToken(RequestTokenDto requestDto, string actionCode)
        {
            var request = await GetExistingRequest(requestDto.ExpedienteId, requestDto.SolicitudId);

            if (request.FolioWeeClinic == null || request.IdPersona == null || request.IdOrden == null)
            {
                throw new CustomException(HttpStatusCode.BadRequest, "La solicitud no pertenece a WeeClinic");
            }

            var validation = await _weeService.OperateToken(request.IdPersona, actionCode, requestDto.Token);

            return validation;
        }

        public async Task<WeeTokenVerificationDto> VerifyWeeToken(RequestTokenDto requestDto)
        {
            var request = await GetExistingRequest(requestDto.ExpedienteId, requestDto.SolicitudId);

            if (request.FolioWeeClinic == null || request.IdPersona == null || request.IdOrden == null)
            {
                throw new CustomException(HttpStatusCode.BadRequest, "La solicitud no pertenece a WeeClinic");
            }

            var branch = await _branchRepository.GetOne(x => x.Id == request.SucursalId);

            var validation = await _weeService.VerifyToken(request.IdPersona, request.IdOrden, requestDto.Token, branch?.Clave);

            if (validation.Verificado)
            {
                request.TokenValidado = true;
                request.UsuarioModificoId = requestDto.UsuarioId;
                request.FechaModifico = DateTime.Now;

                await _repository.Update(request);
            }

            return validation;
        }

        public async Task<List<WeeServiceAssignmentDto>> AssignWeeServices(Guid recordId, Guid requestId, Guid userId)
        {
            try
            {
                _transaction.BeginTransaction();
                var request = await GetExistingRequest(recordId, requestId);

                if (request.FolioWeeClinic == null || request.IdPersona == null || request.IdOrden == null)
                {
                    throw new CustomException(HttpStatusCode.BadRequest, "La solicitud no pertenece a WeeClinic");
                }

                var branch = await _branchRepository.GetOne(x => x.Id == request.SucursalId);
                var studies = await _repository.GetStudiesByRequest(requestId);

                var services = studies.Select(x => new WeeServiceNodeDto(x.EstudioWeeClinic.IdServicio, x.EstudioWeeClinic.IdNodo)).ToList();

                var assignments = await _weeService.AssignServices(services, branch.Clave);

                var results = new List<WeeServiceAssignmentDto>();

                foreach (var assignment in assignments)
                {
                    var servicesIds = assignment.IdServicio.Split("|").Select(x => x.Split(",").FirstOrDefault());

                    foreach (var serviceId in servicesIds)
                    {
                        var study = studies.FirstOrDefault(x => x.EstudioWeeClinic.IdServicio == serviceId);

                        if (study == null) continue;

                        results.Add(new WeeServiceAssignmentDto
                        {
                            IdServicio = serviceId,
                            Estatus = assignment.Estatus,
                            Mensaje = assignment.Mensaje,
                            Clave = study.Clave,
                            Nombre = study.Nombre
                        });
                    }
                }

                foreach (var study in studies)
                {
                    var result = results.FirstOrDefault(x => x.IdServicio == study.EstudioWeeClinic.IdServicio);

                    if (result == null) continue;

                    if (result.Asignado)
                    {
                        study.EstudioWeeClinic.Asignado = true;
                    }
                }

                var assigned = studies.Where(x => x.EstudioWeeClinic.Asignado).ToList();

                request.TotalEstudios = assigned.Sum(x => x.PrecioFinal);
                request.Descuento = assigned.Sum(x => x.Descuento);
                request.Cargo = 0;
                request.Copago = assigned.Sum(x => x.EstudioWeeClinic.TotalPaciente);
                request.Total = request.TotalEstudios - (request.TotalEstudios - request.Copago);
                request.Saldo = request.Copago;
                request.UsuarioModificoId = userId;
                request.FechaModifico = DateTime.Now;

                await _repository.Update(request);

                await _repository.BulkUpdateWeeStudies(requestId, studies.Select(x => x.EstudioWeeClinic).ToList());

                _transaction.CommitTransaction();

                return results;
            }
            catch (Exception)
            {
                _transaction.RollbackTransaction();
                throw;
            }
        }

        private async Task UpdateTotals(Guid recordId, Guid requestId, Guid userId)
        {
            var request = await GetExistingRequest(recordId, requestId);

            var studies = await _repository.GetStudiesByRequest(requestId);
            var packs = await _repository.GetPacksByRequest(requestId);

            var payments = await _repository.GetPayments(requestId);

            studies = studies.Where(x => x.EstatusId != Status.RequestStudy.Cancelado && (x.EstudioWeeClinic == null || x.EstudioWeeClinic.Asignado)).ToList();
            packs = packs.Where(x => !x.Cancelado).ToList();
            payments = payments.Where(x => x.EstatusId.In(Status.RequestPayment.Pagado, Status.RequestPayment.Facturado)).ToList();

            var studyAndPack = studies.Select(x => new { x.Descuento, x.Precio, x.PrecioFinal, Copago = x.EstudioWeeClinic?.TotalPaciente ?? 0 })
                .Concat(packs.Select(x => new { x.Descuento, x.Precio, x.PrecioFinal, Copago = 0m }));

            var totalStudies = studyAndPack.Sum(x => x.PrecioFinal);

            var discount = totalStudies == 0 ? 0 : studyAndPack.Sum(x => x.Descuento);
            var charge = totalStudies == 0 ? 0 : request.Urgencia == URGENCIA_CARGO ? totalStudies * .10m : 0;
            var cup = totalStudies == 0 ? 0 : request.EsWeeClinic ? studyAndPack.Sum(x => x.Copago) :
                request.Procedencia == ORIGIN.COMPAÑIA ? payments.Sum(x => x.Cantidad) : 0;

            var finalTotal = totalStudies - discount + charge;
            var userTotal = cup > 0 ? cup : finalTotal;
            var balance = finalTotal - payments.Sum(x => x.Cantidad);

            request.TotalEstudios = totalStudies;
            request.Descuento = discount;
            request.Cargo = charge;
            request.Copago = cup;
            request.Total = userTotal;
            request.Saldo = balance;
            request.UsuarioModificoId = userId;
            request.FechaModifico = DateTime.Now;

            await _repository.Update(request);
        }

        private static async Task<string> SaveImageGetPath(RequestImageDto requestDto, string fileName = null)
        {
            var path = Path.Combine("wwwroot/images/requests", requestDto.Clave);
            var name = string.Concat(fileName ?? requestDto.Tipo, ".png");

            var isSaved = await requestDto.Imagen.SaveFileAsync(path, name);

            if (isSaved)
            {
                return Path.Combine(path, name);
            }

            return null;
        }

        private async Task<Domain.Request.Request> GetExistingRequest(Guid recordId, Guid requestId)
        {
            var request = await _repository.FindAsync(requestId);

            if (request == null || request.ExpedienteId != recordId)
            {
                throw new CustomException(HttpStatusCode.NotFound, SharedResponses.NotFound);
            }

            return request;
        }

        private async Task<string> GetNewCode(RequestDto requestDto)
        {
            var date = DateTime.Now.ToString("yyMMdd");

            var branch = await _branchRepository.GetOne(x => x.Id == requestDto.SucursalId);
            var lastCode = await _repository.GetLastCode(requestDto.SucursalId, date);

            var code = Codes.GetCode(branch.Codigo, lastCode);
            return code;
        }

        private async Task<string> GeneratePathologicalCode(Request request)
        {
            var allStudies = await _repository.GetAllStudies(request.Id);

            var isCitologic = allStudies.Any(x => x.AreaId == AREAS.CITOLOGIA_NASAL && x.EstatusId != Status.RequestStudy.Cancelado);
            var isPathologic = allStudies.Any(x => x.AreaId == AREAS.HISTOPATOLOGIA && x.EstatusId != Status.RequestStudy.Cancelado);

            string citCode = null;
            string patCode = null;

            if (request.ClavePatologica != null)
            {
                var pathCodes = request.ClavePatologica.Split(",");
                citCode = pathCodes.FirstOrDefault(x => x.Contains("C"))?.Trim();
                patCode = pathCodes.FirstOrDefault(x => x.Contains("LR"))?.Trim();
            }

            var date = DateTime.Now.ToString("yy");

            if (isCitologic && citCode == null)
            {
                var lastCode = await _repository.GetLastPathologicalCode(request.SucursalId, date, "C");
                citCode = Codes.GetPathologicalCode("C", lastCode);
            }
            else if (!isCitologic)
            {
                citCode = null;
            }
            if (isPathologic && patCode == null)
            {
                var lastCode = await _repository.GetLastPathologicalCode(request.SucursalId, date, "LR");
                patCode = Codes.GetPathologicalCode("LR", lastCode);
            }
            else if (!isPathologic)
            {
                patCode = null;
            }

            return patCode == null && citCode == null ? null :
                patCode != null && citCode == null ? patCode :
                patCode == null && citCode != null ? citCode :
                $"{patCode}, {citCode}";
        }
    }
}
