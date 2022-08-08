using EventBus.Messages.Common;
using MassTransit;
using Microsoft.Extensions.Configuration;
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

        public const byte Compañia = 1;

        public RequestApplication(
            ITransactionProvider transaction,
            IRequestRepository repository,
            ICatalogClient catalogClient,
            IPdfClient pdfClient,
            ISendEndpointProvider sendEndpoint,
            IRabbitMQSettings rabbitMQSettings,
            IQueueNames queueNames)
        {
            _transaction = transaction;
            _repository = repository;
            _catalogClient = catalogClient;
            _pdfClient = pdfClient;
            _sendEndpointProvider = sendEndpoint;
            _queueNames = queueNames;
            _rabbitMQSettings = rabbitMQSettings;
        }

        public async Task<RequestDto> GetById(Guid recordId, Guid requestId)
        {
            var request = await GetExistingRequest(recordId, requestId);

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

            return new RequestStudyUpdateDto()
            {
                Paquetes = packsDto,
                Estudios = studiesDto
            };
        }

        public async Task<string> Create(RequestDto requestDto)
        {
            var date = DateTime.Now.ToString("ddMMyy");

            var codeRange = await _catalogClient.GetCodeRange(requestDto.SucursalId);
            var lastCode = await _repository.GetLastCode(requestDto.SucursalId, date);

            var consecutive = Code.GetCode(codeRange, lastCode);
            var code = $"{consecutive}{date}";

            requestDto.Clave = code;
            var newRequest = requestDto.ToModel();

            await _repository.Create(newRequest);

            return newRequest.Id.ToString();
        }

        public async Task UpdateGeneral(RequestGeneralDto requestDto)
        {
            var request = await GetExistingRequest(requestDto.ExpedienteId, requestDto.SolicitudId);

            request.Procedencia = requestDto.Procedencia;
            request.CompañiaId = requestDto.Procedencia == Compañia ? requestDto.CompañiaId : null;
            request.MedicoId = requestDto.MedicoId;
            request.Afiliacion = requestDto.Afiliacion;
            request.Urgencia = requestDto.Urgencia;
            request.EnvioCorreo = requestDto.Correo;
            request.EnvioWhatsApp = requestDto.Whatsapp;
            request.Observaciones = requestDto.Observaciones;
            request.UsuarioModificoId = requestDto.UsuarioId;
            request.FechaModifico = DateTime.Now;

            await _repository.Update(request);
        }

        public async Task SendTestEmail(RequestSendDto requestDto)
        {
            var request = await GetExistingRequest(requestDto.ExpedienteId, requestDto.SolicitudId);

            var subject = RequestTemplates.Subjects.TestMessage;
            var title = RequestTemplates.Titles.RequestCode(request.Clave);
            var message = RequestTemplates.Messages.TestMessage;

            var emailToSend = new EmailContract(requestDto.Correo, null, subject, title, message)
            {
                Notificar = true,
                RemitenteId = requestDto.UsuarioId.ToString()
            };

            var endpoint = await _sendEndpointProvider.GetSendEndpoint(new Uri(string.Concat(_rabbitMQSettings.Host, _queueNames.Email)));

            await endpoint.Send(emailToSend);
        }

        public async Task SendTestWhatsapp(RequestSendDto requestDto)
        {
            _ = await GetExistingRequest(requestDto.ExpedienteId, requestDto.SolicitudId);

            var message = RequestTemplates.Messages.TestMessage;

            var phone = requestDto.Telefono.Replace("-", "");
            phone = phone.Length == 10 ? "52" + phone : phone;
            var emailToSend = new WhatsappContract(phone, message)
            {
                Notificar = true,
                RemitenteId = requestDto.UsuarioId.ToString()
            };

            var endpoint = await _sendEndpointProvider.GetSendEndpoint(new Uri(string.Concat(_rabbitMQSettings.Host, _queueNames.Whatsapp)));

            await endpoint.Send(emailToSend);
        }

        public async Task UpdateTotals(RequestTotalDto requestDto)
        {
            var request = await GetExistingRequest(requestDto.ExpedienteId, requestDto.SolicitudId);

            await _repository.Update(request);
        }

        public async Task UpdateStudies(RequestStudyUpdateDto requestDto)
        {
            try
            {
                _transaction.BeginTransaction();

                var request = await GetExistingRequest(requestDto.ExpedienteId, requestDto.SolicitudId);

                var studiesDto = requestDto.Estudios ?? new List<RequestStudyDto>();
                var packStudiesDto = new List<RequestStudyDto>();

                if (requestDto.Paquetes != null)
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

                var duplicates = requestDto.Estudios.GroupBy(x => x.Clave).Where(x => x.Count() > 1).Select(x => x.Key);

                if (duplicates != null && duplicates.Any())
                {
                    throw new CustomException(HttpStatusCode.BadRequest, RecordResponses.Request.RepeatedStudies(string.Join(", ", duplicates)));
                }

                var currentPacks = await _repository.GetPacksByRequest(requestDto.SolicitudId);

                var currentSudies = await _repository.GetStudiesByRequest(requestDto.SolicitudId);

                var packs = requestDto.Paquetes.ToModel(requestDto.SolicitudId, currentPacks, requestDto.UsuarioId);

                await _repository.BulkUpdatePacks(requestDto.SolicitudId, packs);

                var studies = studiesDto.ToModel(requestDto.SolicitudId, currentSudies, requestDto.UsuarioId);

                await _repository.BulkUpdateStudies(requestDto.SolicitudId, studies);

                _transaction.CommitTransaction();
            }
            catch (Exception)
            {
                _transaction.RollbackTransaction();
                throw;
            }
        }

        public async Task CancelStudies(RequestStudyUpdateDto requestDto)
        {
            var request = await GetExistingRequest(requestDto.ExpedienteId, requestDto.SolicitudId);

            var currentSudies = await _repository.GetStudiesByRequest(requestDto.SolicitudId);

            var studies = currentSudies.Where(x => !requestDto.Estudios.Select(y => y.EstudioId).Contains(x.EstudioId)).ToList();

            if (studies == null || studies.Count == 0)
            {
                throw new CustomException(HttpStatusCode.BadRequest, RecordResponses.Request.NoStudySelected);
            }

            await _repository.BulkUpdateStudies(requestDto.SolicitudId, studies);
        }

        public async Task<int> SendStudiesToSampling(RequestStudyUpdateDto requestDto)
        {
            var request = await GetExistingRequest(requestDto.ExpedienteId, requestDto.SolicitudId);

            var studiesIds = requestDto.Estudios.Select(x => x.EstudioId);
            var studies = await _repository.GetStudyById(requestDto.SolicitudId, studiesIds);

            studies = studies.Where(x => x.EstatusId == Status.Request.Pendiente).ToList();

            if (studies == null || studies.Count == 0)
            {
                throw new CustomException(HttpStatusCode.BadRequest, RecordResponses.Request.NoStudySelected);
            }

            foreach (var study in studies)
            {
                study.EstatusId = Status.Request.TomaDeMuestra;
                study.UsuarioModificoId = requestDto.UsuarioId;
                study.FechaModifico = DateTime.Now;
            }

            await _repository.BulkUpdateStudies(requestDto.SolicitudId, studies);

            return studies.Count;
        }

        public async Task<int> SendStudiesToRequest(RequestStudyUpdateDto requestDto)
        {
            var request = await GetExistingRequest(requestDto.ExpedienteId, requestDto.SolicitudId);

            var studiesIds = requestDto.Estudios.Select(x => x.EstudioId);
            var studies = await _repository.GetStudyById(requestDto.SolicitudId, studiesIds);

            studies = studies.Where(x => x.EstatusId == Status.Request.TomaDeMuestra).ToList();

            if (studies == null || studies.Count == 0)
            {
                throw new CustomException(HttpStatusCode.BadRequest, RecordResponses.Request.NoStudySelected);
            }

            foreach (var study in studies)
            {
                study.EstatusId = Status.Request.Solicitado;
                study.UsuarioModificoId = requestDto.UsuarioId;
                study.FechaModifico = DateTime.Now;
            }

            await _repository.BulkUpdateStudies(requestDto.SolicitudId, studies);

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

        public async Task<byte[]> PrintTicket(Guid recordId, Guid requestId)
        {
            var request = await GetExistingRequest(recordId, requestId);

            var orderData = request.ToRequestOrderDto();

            return await _pdfClient.GenerateTicket();
        }

        public async Task<byte[]> PrintOrder(Guid recordId, Guid requestId)
        {
            var request = await _repository.GetById(requestId);

            if (request == null || request.ExpedienteId != recordId)
            {
                throw new CustomException(HttpStatusCode.NotFound, SharedResponses.NotFound);
            }

            var order = request.ToRequestOrderDto();

            return await _pdfClient.GenerateOrder(order);
        }

        public async Task SaveImage(RequestImageDto requestDto)
        {
            var request = await GetExistingRequest(requestDto.ExpedienteId, requestDto.SolicitudId);

            var typeOk = requestDto.Tipo.In("orden", "ine", "formato");

            var isImage = requestDto.Imagen.IsImage();

            if (!typeOk || !isImage)
            {
                throw new CustomException(HttpStatusCode.BadRequest, SharedResponses.InvalidImage);
            }

            requestDto.Clave = request.Clave;
            var path = await SaveImageGetPath(requestDto);

            if (requestDto.Tipo == "orden")
            {
                request.RutaOrden = path;
            }
            else if (requestDto.Tipo == "ine")
            {
                request.RutaINE = path;
            }
            else
            {
                request.RutaFormato = path;
            }

            request.UsuarioModificoId = requestDto.UsuarioId;
            request.FechaModifico = DateTime.Now;

            await _repository.Update(request);
        }

        private static async Task<string> SaveImageGetPath(RequestImageDto requestDto)
        {
            var path = Path.Combine("wwwroot/images/requests", requestDto.Clave);
            var name = string.Concat(requestDto.Tipo, ".png");

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
    }
}
