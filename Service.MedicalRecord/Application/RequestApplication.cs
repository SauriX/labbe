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

namespace Service.MedicalRecord.Application
{
    public class RequestApplication : IRequestApplication
    {
        private readonly string _imageUrl;
        private readonly IRequestRepository _repository;
        private readonly ICatalogClient _catalogClient;
        private readonly IPdfClient _pdfClient;
        private readonly ISendEndpointProvider _sendEndpointProvider;

        public RequestApplication(
            IConfiguration configuration,
            IRequestRepository repository,
            ICatalogClient catalogClient,
            IPdfClient pdfClient,
            ISendEndpointProvider sendEndpoint)
        {
            _imageUrl = configuration.GetValue<string>("Request:ImageUrl");
            _repository = repository;
            _catalogClient = catalogClient;
            _pdfClient = pdfClient;
            _sendEndpointProvider = sendEndpoint;
        }

        public async Task<RequestDto> Create(RequestDto requestDto)
        {
            var date = DateTime.Now.ToString("ddMMyy");

            var codeRange = await _catalogClient.GetCodeRange(requestDto.SucursalId);
            var lastCode = await _repository.GetLastCode(requestDto.SucursalId, date);

            var consecutive = Code.GetCode(codeRange, lastCode);
            var code = $"{consecutive}{date}";

            requestDto.Clave = code;
            var newRequest = requestDto.ToModel();

            await _repository.Create(newRequest);

            return newRequest.ToRequestDto();
        }

        public async Task SendTestEmail(Guid requestId, string email)
        {
            var request = await _repository.FindAsync(requestId);

            if (request == null)
            {
                throw new CustomException(HttpStatusCode.NotFound, SharedResponses.NotFound);
            }

            var emailToSend = new EmailContract("mike_fa96@hotmail.com", null, "hola", "test", "test");

            var endpoint = await _sendEndpointProvider.GetSendEndpoint(new Uri("rabbitmq://axsishost.online:5672/labramos-dev/email-queue"));

            await endpoint.Send(emailToSend);
        }

        public async Task SendTestWhatsapp(Guid requestId, string phone)
        {
            var request = await _repository.FindAsync(requestId);

            if (request == null)
            {
                throw new CustomException(HttpStatusCode.NotFound, SharedResponses.NotFound);
            }

            var emailToSend = new EmailContract("mike_fa96@hotmail.com", null, "hola", "test", "test");

            var endpoint = await _sendEndpointProvider.GetSendEndpoint(new Uri("rabbitmq://axsishost.online:5672/labramos-dev/email-queue"));

            await endpoint.Send(emailToSend);
        }

        public async Task UpdateGeneral(RequestGeneralDto requestGeneralDto)
        {
            var request = await _repository.FindAsync(requestGeneralDto.Id);

            if (request == null)
            {
                throw new CustomException(HttpStatusCode.NotFound, SharedResponses.NotFound);
            }

            request.Procedencia = requestGeneralDto.Procedencia;
            request.CompañiaId = requestGeneralDto.Procedencia == 1 ? null : requestGeneralDto.CompañiaId;
            request.MedicoId = requestGeneralDto.MedicoId;
            request.Afiliacion = requestGeneralDto.Afiliacion;
            request.Urgencia = requestGeneralDto.Urgencia;
            request.EnvioCorreo = requestGeneralDto.EnvioCorreo;
            request.EnvioWhatsApp = requestGeneralDto.EnvioWhatsApp;
            request.Observaciones = requestGeneralDto.Observaciones;
            request.UsuarioModificoId = requestGeneralDto.UsuarioId;
            request.FechaModifico = DateTime.Now;

            await _repository.Update(request);
        }

        public async Task UpdateTotals(RequestTotalDto requestTotalDto)
        {
            var request = await _repository.FindAsync(requestTotalDto.Id);

            if (request == null)
            {
                throw new CustomException(HttpStatusCode.NotFound, SharedResponses.NotFound);
            }

            await _repository.Update(request);
        }

        public async Task AddStudies(Guid requestId, List<RequestStudyDto> studiesDto)
        {
            var request = await _repository.FindAsync(requestId);

            if (request == null)
            {
                throw new CustomException(HttpStatusCode.NotFound, SharedResponses.NotFound);
            }

            var duplicates = studiesDto.GroupBy(x => x.Clave).Where(x => x.Count() > 1).Select(x => x.Key);

            if (duplicates != null && duplicates.Any())
            {
                throw new CustomException(HttpStatusCode.BadRequest, RecordResponses.Request.RepeatedStudies(string.Join(", ", duplicates)));
            }

            var currentSudies = await _repository.GetStudiesByRequest(requestId);

            var studies = studiesDto.ToModel(requestId, currentSudies);

            await _repository.BulkUpdateStudies(requestId, studies);
        }

        public async Task CancelStudies(Guid requestId, List<int> studiesIds)
        {
            var request = await _repository.FindAsync(requestId);

            if (request == null)
            {
                throw new CustomException(HttpStatusCode.NotFound, SharedResponses.NotFound);
            }

            var currentSudies = await _repository.GetStudiesByRequest(requestId);

            var studies = currentSudies.Where(x => !studiesIds.Contains(x.EstudioId)).ToList();

            await _repository.BulkUpdateStudies(requestId, studies);
        }

        public async Task<byte[]> PrintTicket(Guid requestId)
        {
            var request = await _repository.GetById(requestId);

            if (request == null)
            {
                throw new CustomException(HttpStatusCode.NotFound, SharedResponses.NotFound);
            }

            var orderData = request.ToRequestOrderDto();

            return await _pdfClient.GenerateTicket();
        }

        public async Task<int> SendStudiesToSampling(Guid requestId, List<int> studiesIds, Guid userId)
        {
            var request = await _repository.FindAsync(requestId);

            if (request == null)
            {
                throw new CustomException(HttpStatusCode.NotFound, SharedResponses.NotFound);
            }

            var studies = await _repository.GetStudyById(requestId, studiesIds);

            studies = studies.Where(x => x.EstatusId == Status.Request.Pendiente).ToList();

            if (studies == null || studies.Count == 0)
            {
                throw new CustomException(HttpStatusCode.BadRequest, RecordResponses.Request.NoStudySelected);
            }

            studies.ForEach(x =>
            {
                x.EstatusId = Status.Request.TomaDeMuestra;
                x.UsuarioModificoId = userId;
                x.FechaModifico = DateTime.Now;
            });

            await _repository.BulkUpdateStudies(requestId, studies);

            return studies.Count;
        }

        public async Task<int> SendStudiesToRequest(Guid requestId, List<int> studiesIds, Guid userId)
        {
            var request = await _repository.FindAsync(requestId);

            if (request == null)
            {
                throw new CustomException(HttpStatusCode.NotFound, SharedResponses.NotFound);
            }

            var studies = await _repository.GetStudyById(requestId, studiesIds);

            studies = studies.Where(x => x.EstatusId == Status.Request.TomaDeMuestra).ToList();

            if (studies == null || studies.Count == 0)
            {
                throw new CustomException(HttpStatusCode.BadRequest, RecordResponses.Request.NoStudySelected);
            }

            studies.ForEach(x =>
            {
                x.EstatusId = Status.Request.Solicitado;
                x.UsuarioModificoId = userId;
                x.FechaModifico = DateTime.Now;
            });

            await _repository.BulkUpdateStudies(requestId, studies);

            return studies.Count;
        }

        public async Task AddPartiality(Guid requestId, bool apply, Guid userId)
        {
            var request = await _repository.FindAsync(requestId);

            if (request == null)
            {
                throw new CustomException(HttpStatusCode.NotFound, SharedResponses.NotFound);
            }

            request.Parcialidad = apply;
            request.UsuarioModificoId = userId;
            request.FechaModifico = DateTime.Now;

            await _repository.Update(request);
        }

        public async Task<byte[]> PrintOrder(Guid requestId)
        {
            var request = await _repository.FindAsync(requestId);

            if (request == null)
            {
                throw new CustomException(HttpStatusCode.NotFound, SharedResponses.NotFound);
            }

            var order = request.ToRequestOrderDto();

            return await _pdfClient.GenerateOrder(order);
        }

        public async Task SaveImage(RequestImageDto requestImageDto)
        {
            var request = await _repository.FindAsync(requestImageDto.SolicitudId);

            if (request == null)
            {
                throw new CustomException(HttpStatusCode.NotFound, SharedResponses.NotFound);
            }

            var typeOk = requestImageDto.Tipo.In("orden", "ine", "formato");

            var isImage = requestImageDto.Imagen.IsImage();

            if (!typeOk || !isImage)
            {
                throw new CustomException(HttpStatusCode.BadRequest, SharedResponses.InvalidImage);
            }

            requestImageDto.Clave = request.Clave;
            var path = await SaveImageGetPath(requestImageDto);

            if (requestImageDto.Tipo == "orden")
            {
                request.RutaOrden = path;
            }
            else if (requestImageDto.Tipo == "ine")
            {
                request.RutaINE = path;
            }
            else
            {
                request.RutaFormato = path;
            }

            await _repository.Update(request);
        }

        private async Task<string> SaveImageGetPath(RequestImageDto requestDto)
        {
            var path = Path.Combine(_imageUrl, "Solicitudes", requestDto.Clave);
            var name = string.Concat(requestDto.Tipo, ".png");

            var isSaved = await requestDto.Imagen.SaveFileAsync(path, name);

            if (isSaved)
            {
                return Path.Combine(path, name);
            }

            return null;
        }
    }
}
