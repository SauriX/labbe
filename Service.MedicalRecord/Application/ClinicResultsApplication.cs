using Service.MedicalRecord.Application.IApplication;
using Service.MedicalRecord.Dtos;
using Service.MedicalRecord.Dtos.RequestedStudy;
using Service.MedicalRecord.Dtos.Sampling;
using Service.MedicalRecord.Repository.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ClosedXML.Report;
using ClosedXML.Excel;
using Service.MedicalRecord.Dictionary;
using MoreLinq;
using Shared.Extensions;
using Service.MedicalRecord.Mapper;
using Service.MedicalRecord.Dtos.Request;
using Service.MedicalRecord.Dtos.ClinicResults;
using System.Net;
using SharedResponses = Shared.Dictionary.Responses;
using Shared.Error;
using Shared.Dictionary;
using Service.MedicalRecord.Client.IClient;
using MassTransit;
using RequestTemplates = Service.MedicalRecord.Dictionary.EmailTemplates.Request;
using EventBus.Messages.Common;
using Service.MedicalRecord.Settings.ISettings;

namespace Service.MedicalRecord.Application
{
    public class ClinicResultsApplication : IClinicResultsApplication
    {
        public readonly IClinicResultsRepository _repository;
        private readonly IPdfClient _pdfClient;
        private readonly ISendEndpointProvider _sendEndpointProvider;
        private readonly IRabbitMQSettings _rabbitMQSettings;
        private readonly IQueueNames _queueNames;

        public ClinicResultsApplication(IClinicResultsRepository repository, IPdfClient pdfClient, ISendEndpointProvider sendEndpoint, IRabbitMQSettings rabbitMQSettings,
            IQueueNames queueNames)
        {
            _repository = repository;
            _sendEndpointProvider = sendEndpoint;
            _pdfClient = pdfClient;
            _queueNames = queueNames;
            _rabbitMQSettings = rabbitMQSettings;
        }

        public Task DeleteImage(Guid recordId, Guid requestId, string code)
        {
            throw new NotImplementedException();
        }

        public async Task<(byte[] file, string fileName)> ExportList(RequestedStudySearchDto search)
        {
            var studies = await GetAll(search);

            foreach (var request in studies)
            {
                if (studies.Count > 0)
                {
                    request.Estudios.Insert(0, new StudyDto { Clave = "Clave", Nombre = "Nombre Estudio", Registro = "Fecha de Registro", Entrega = "Fecha de Entrega", NombreEstatus = "Estatus" });
                }
            }

            var path = Assets.InformeClinicos;

            var template = new XLTemplate(path);

            template.AddVariable("Direccion", "Avenida Humberto Lobo #555");
            template.AddVariable("Sucursal", "San Pedro Garza García, Nuevo León");
            template.AddVariable("Titulo", "Captura de resultados (Clínicos)");
            template.AddVariable("FechaInicio", search.Fecha.First().ToString("dd/MM/yyyy"));
            template.AddVariable("FechaFinal", search.Fecha.Last().ToString("dd/MM/yyyy"));
            template.AddVariable("Expedientes", studies);

            template.Generate();

            template.Workbook.Worksheets.ToList().ForEach(x =>
            {
                var cuenta = 0;
                int[] posiciones = { 0, 0 };
                x.Worksheet.Rows().ForEach(y =>
                {
                    var descripcion = template.Workbook.Worksheets.FirstOrDefault().Cell(y.RowNumber(), "B").Value.ToString();
                    var next = template.Workbook.Worksheets.FirstOrDefault().Cell(y.RowNumber() + 2, "B").Value.ToString();
                    var plusOne = template.Workbook.Worksheets.FirstOrDefault().Cell(y.RowNumber() + 1, "B").Value.ToString();
                    if (descripcion == "Clave" && cuenta == 0)
                    {
                        posiciones[0] = y.RowNumber();
                        cuenta++;
                    }
                    if ((next == "Clave" || (next == "" && plusOne == "")) && cuenta == 1)
                    {
                        posiciones[1] = y.RowNumber();
                        cuenta++;
                    }
                    if (cuenta == 2)
                    {
                        x.Rows(posiciones[0], posiciones[1]).Group();
                        x.Rows(posiciones[0], posiciones[1]).Collapse();
                        cuenta = 0;
                        posiciones.ForEach(z => z = 0);
                    }
                });
            });
            template.Format();

            return (template.ToByteArray(), $"Informe Captura de Resultados (Clínicos).xlsx");
        }

        public async Task<List<ClinicResultsDto>> GetAll(RequestedStudySearchDto search)
        {
            var clinicResults = await _repository.GetAll(search);
            if (clinicResults != null)
            {
                return clinicResults.ToClinicResultsDto();
            }
            else
            {
                return null;
            }
        }

        /*public async Task<ClinicResultsCaptureDto> Create(ClinicResults results)
        {
            if (!string.IsNullOrEmpty(results.Id.ToString()))
            {
                throw new CustomException(HttpStatusCode.Conflict, Responses.NotPossible);
            }

            var newResults = results.ToCaptureResults();

            await CheckDuplicate(newResults);

            await _repository.Create(newResults);

            newResults = await _repository.GetById(newResults.Id);

            return newResults.ToCaptureResults();
        }*/

        private Task CheckDuplicate(object newResults)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<string>> GetImages(Guid recordId, Guid requestId)
        {
            throw new NotImplementedException();
        }

        /*public async Task<byte[]> PrintOrder(Guid recordId, Guid requestId)
        {
            var results = await _repository.GetById(requestId);

            if (results == null || results.ExpedienteId != recordId)
            {
                throw new CustomException(HttpStatusCode.NotFound, SharedResponses.NotFound);
            }

            var order = results.ToRequestDto();

            return await _pdfClient.GenerateLabResults(order);
        }*/

        public Task<string> SaveImage(RequestImageDto requestDto)
        {
            throw new NotImplementedException();
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

            var endpoint = await _sendEndpointProvider.GetSendEndpoint(new Uri(string.Concat(_rabbitMQSettings.Host, "/", _queueNames.Email)));

            await endpoint.Send(emailToSend);
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

            var endpoint = await _sendEndpointProvider.GetSendEndpoint(new Uri(string.Concat(_rabbitMQSettings.Host, "/", _queueNames.Whatsapp)));

            await endpoint.Send(emailToSend);
        }

        public Task<int> UpdateStatus(List<ClinicResultsUpdateDto> requestDto)
        {
            throw new NotImplementedException();
        }
    }
}
