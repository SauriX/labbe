﻿using Service.MedicalRecord.Application.IApplication;
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
using Service.MedicalRecord.Domain;
using System.IO;
using Microsoft.AspNetCore.Http;
using Shared.Helpers;
using Service.MedicalRecord.Dtos.Catalogs;

namespace Service.MedicalRecord.Application
{
    public class ClinicResultsApplication : IClinicResultsApplication
    {
        private readonly IClinicResultsRepository _repository;
        private readonly IRequestRepository _request;
        private readonly ICatalogClient _catalogClient;
        private readonly IPdfClient _pdfClient;
        private readonly ISendEndpointProvider _sendEndpointProvider;
        private readonly IRabbitMQSettings _rabbitMQSettings;
        private readonly IQueueNames _queueNames;

        public ClinicResultsApplication(IClinicResultsRepository repository, IRequestRepository request, ICatalogClient catalogClient, IPdfClient pdfClient, ISendEndpointProvider sendEndpoint, IRabbitMQSettings rabbitMQSettings,
            IQueueNames queueNames)
        {
            _repository = repository;
            _request = request;
            _catalogClient = catalogClient;
            _sendEndpointProvider = sendEndpoint;
            _pdfClient = pdfClient;
            _queueNames = queueNames;
            _rabbitMQSettings = rabbitMQSettings;
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

        public async Task<RequestStudyUpdateDto> GetStudies(Guid recordId, Guid requestId)
        {
            var request = await GetExistingRequest(recordId, requestId);

            var studies = await _request.GetAllStudies(request.Id);
            var studiesDto = studies.ToRequestStudyDto();
            //var studiesIds = studiesDto.Select(x => x.Id).ToList();

            var ids = studiesDto.Select(x => x.EstudioId).ToList();
            var studiesParams = await _catalogClient.GetStudies(ids);

            var results = await _repository.GetResultsById(requestId);
            var resultsIds = results.Select(x => x.SolicitudEstudioId).ToList();
            var totalParams = studiesParams.SelectMany(x => x.Parametros).Count();

            if (results.Count < totalParams)
            {
                var missingParams = new List<ParameterListDto>();

                foreach (var currentStudy in studiesParams)
                {
                    var parameters = currentStudy.Parametros;

                    foreach (var parameter in parameters)
                    {
                        if (!results.Any(x => x.ParametroId.ToString() == parameter.Id && x.EstudioId == currentStudy.Id))
                        {
                            var study = studiesDto.FirstOrDefault(x => x.EstudioId == currentStudy.Id && !resultsIds.Contains(x.Id));
                            parameter.SolicitudEstudioId = study.Id;
                            parameter.EstudioId = currentStudy.Id;
                            resultsIds.Add(study.Id);
                            missingParams.Add(parameter);
                        }
                        else
                        {
                            results.Remove(results.First(x => x.ParametroId.ToString() == parameter.Id && x.EstudioId == currentStudy.Id));
                        }
                    }
                }

                var newResults = missingParams.Select(x => new ClinicResults
                {
                    Id = Guid.NewGuid(),
                    SolicitudId = requestId,
                    Nombre = x.Nombre,
                    TipoValorId = x.TipoValor,
                    Resultado = null,
                    ValorInicial = x.ValorInicial,
                    ValorFinal = x.ValorFinal,
                    ParametroId = Guid.Parse(x.Id),
                    SolicitudEstudioId = x.SolicitudEstudioId,
                    EstudioId = x.EstudioId
                });

                // Crear Los que no existen
            }

            foreach (var study in studiesDto)
            {
                var st = studiesParams.FirstOrDefault(x => x.Id == study.EstudioId);
                if (st == null) continue;

                study.Parametros = st.Parametros;
                study.Indicaciones = st.Indicaciones;
            }



            var data = new RequestStudyUpdateDto()
            {
                Estudios = studiesDto,
            };

            return data;
        }

        public async Task SaveLabResults(List<ClinicResultsFormDto> results)
        {
            if (results.Count() == 0)
            {
                throw new CustomException(HttpStatusCode.Conflict, Responses.NotPossible);
            }

            var newResults = results.ToCaptureResults();
            await _repository.CreateLabResults(newResults);
        }

        public async Task UpdateLabResults(List<ClinicResultsFormDto> results)
        {
            if (results.Count() == 0)
            {
                throw new CustomException(HttpStatusCode.Conflict, Responses.NotPossible);
            }

            var newResults = results.ToCaptureResults();
            await _repository.UpdateLabResults(newResults);

            foreach (var result in results)
            {
                if (result.Estatus == Status.RequestStudy.Liberado)
                {
                    await this.DeliverFilesMedicalResults(result.SolicitudId, result.EstudioId, result.DepartamentoEstudio);
                    //validate partial or not
                }
                await this.UpdateStatusStudy(result.EstudioId, result.Estatus, result.UsuarioId);
            }

        }

        /*public async Task<byte[]> PrintResults(Guid recordId, Guid requestId)
        {
            var results = await _repository.GetByRequest(requestId);

            if (results == null || !results.Any())
            {
                throw new CustomException(HttpStatusCode.NotFound, SharedResponses.NotFound);
            }

            var order = results.ToResults();

            return await _pdfClient.GenerateLabResults(order);
        }*/

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

        public async Task SaveResultPathologicalStudy(ClinicalResultPathologicalFormDto result)
        {
            var newResult = result.ToClinicalResultPathological();

            await _repository.CreateResultPathological(newResult);

            if (result.ImagenPatologica != null)
            {
                for (int i = 0; i < result.ImagenPatologica.Count; i++)
                {
                    await SaveImageGetPath(result.ImagenPatologica[i], newResult.EstudioId);
                }
            }

        }
        private static async Task<string> SaveImageGetPath(IFormFile result, int id)
        {
            var path = Path.Combine("wwwroot/images/ResultsPathological", id.ToString());
            var name = string.Concat(result.FileName, "");

            var isSaved = await result.SaveFileAsync(path, name);


            if (isSaved)
            {
                return Path.Combine(path, name);
            }

            return null;
        }
        private static async Task<string> DeleteImageGetPath(string result, int id)
        {
            var path = Path.Combine("wwwroot/images/ResultsPathological", id.ToString(), result);
            if (File.Exists(path))
            {
                try
                {
                    File.Delete(path);

                }
                catch (Exception ex)
                {
                    throw new CustomException(HttpStatusCode.NotFound, SharedResponses.NotPossible);
                }
            }

            return path;

        }
        public async Task UpdateResultPathologicalStudy(ClinicalResultPathologicalFormDto result)
        {
            var existing = await _repository.GetResultPathologicalById(result.RequestStudyId);

            var existingStudy = await _repository.GetStudyById(result.EstudioId);

            if (existing == null)
            {
                throw new CustomException(HttpStatusCode.NotFound, SharedResponses.NotFound);
            }
            if (existingStudy.EstatusId == Status.RequestStudy.Solicitado)
            {

                var newResult = result.ToUpdateClinicalResultPathological(existing);

                await _repository.UpdateResultPathologicalStudy(newResult);

                if (result.ListaImagenesCargadas != null)
                {
                    for (int i = 0; i < result.ListaImagenesCargadas.Length; i++)
                    {
                        await DeleteImageGetPath(result.ListaImagenesCargadas[i], newResult.EstudioId);
                    }
                }
                if (result.ImagenPatologica != null)
                {
                    for (int i = 0; i < result.ImagenPatologica.Count; i++)
                    {
                        await SaveImageGetPath(result.ImagenPatologica[i], newResult.EstudioId);
                    }
                }
            }
            if (result.Estatus == Status.RequestStudy.Liberado)
            {
                await this.DeliverFilesMedicalResults(result.SolicitudId, result.EstudioId, result.DepartamentoEstudio);
                //validate partial or not
            }
            await this.UpdateStatusStudy(result.EstudioId, result.Estatus, result.UsuarioId);

        }
        public async Task<byte[]> PrintSelectedStudies(ConfigurationToPrintStudies configuration)
        {
            //throw new NotImplementedException();
            //List<ClinicalResultsPathological> results = new List<ClinicalResultsPathological> { };
            //foreach (var item in configuration.Estudios)
            //{

            //    var existingResultPath = await _repository.GetResultPathologicalById(item);

            //    if (existingResultPath == null)
            //    {
            //        throw new CustomException(HttpStatusCode.NotFound, SharedResponses.NotFound);
            //    }
            //    results.Add(existingResultPath);

            //}

            List<int> labResults = new List<int> { };
            List<int> pathologicalResults = new List<int> { };

            foreach (var config in configuration.Estudios)
            {
                if (config.Tipo == "LABORATORY")
                {
                    labResults.Add(config.Id);
                }

                if (config.Tipo == "PATHOLOGICAL")
                {
                    pathologicalResults.Add(config.Id);
                }
            }

            var tasks = pathologicalResults.Select(x => _repository.GetResultPathologicalById(x));
            var resultsTask = await Task.WhenAll(tasks);


            var existingResultPathologyPdf = resultsTask.ToList().toInformationPdfResult(configuration.ImprimirLogos);

            var taskLab = labResults.Select(x => _repository.GetLabResultsById(x));
            var labResultsTasks = await Task.WhenAll(taskLab);


            var existingLabResultPdf = labResultsTasks.ToList().ToResults(configuration.ImprimirLogos);

            byte[] pdfBytes = await _pdfClient.GenerateLabResults(existingLabResultPdf);
            return pdfBytes;
        }
        private async Task<bool> DeliverFilesMedicalResults(Guid requestId, int estudioId, string DepartamentoEstudio)
        {

            var existingRequest = await _repository.GetRequestById(requestId);

            if (existingRequest == null)
            {
                throw new CustomException(HttpStatusCode.NotFound, SharedResponses.NotFound);
            }

            if (existingRequest.Parcialidad)
            {
                if (DepartamentoEstudio == "HISTOPATOLÓGICO" || DepartamentoEstudio == "CITOLÓGICO")
                {

                    var existingResultPath = await _repository.GetResultPathologicalById(estudioId);


                    if (existingResultPath == null)
                    {
                        throw new CustomException(HttpStatusCode.NotFound, SharedResponses.NotFound);
                    }

                    var existingResultPathPdf = existingResultPath.toInformationPdf(existingRequest, DepartamentoEstudio, true);

                    byte[] pdfBytes = await _pdfClient.GeneratePathologicalResults(existingResultPathPdf);

                }

            }
            else
            {
                if (existingRequest.Estudios.All(estudio => estudio.EstatusId == Status.RequestStudy.Liberado))
                {
                    existingRequest.Estudios.ForEach(async (estudio) =>
                    {
                        if (DepartamentoEstudio == "HISTOPATOLÓGICO" || DepartamentoEstudio == "CITOLÓGICO")
                        {

                            var existingResultPath = await _repository.GetResultPathologicalById(estudio.EstudioId);

                            if (existingResultPath == null)
                            {
                                throw new CustomException(HttpStatusCode.NotFound, SharedResponses.NotFound);
                            }

                            var existingResultPathPdf = existingResultPath.toInformationPdf(existingRequest, DepartamentoEstudio, true);

                            byte[] pdfBytes = await _pdfClient.GeneratePathologicalResults(existingResultPathPdf);
                        }
                    });
                }

            }

            return true;
        }

        public async Task UpdateStatusStudy(int RequestStudyId, byte status, Guid usuarioId)
        {
            var existingStudy = await _repository.GetStudyById(RequestStudyId);

            if (existingStudy == null)
            {
                throw new CustomException(HttpStatusCode.NotFound, SharedResponses.NotFound);
            }

            if (Status.RequestStudy.Capturado == status)
            {
                existingStudy.FechaCaptura = DateTime.Now;
                existingStudy.UsuarioCaptura = usuarioId.ToString();
            }
            if (Status.RequestStudy.Validado == status)
            {
                existingStudy.FechaValidacion = DateTime.Now;
                existingStudy.UsuarioCaptura = usuarioId.ToString();
            }
            if (Status.RequestStudy.Liberado == status)
            {
                existingStudy.FechaLiberado = DateTime.Now;
                existingStudy.UsuarioCaptura = usuarioId.ToString();
            }
            if (Status.RequestStudy.Enviado == status)
            {
                existingStudy.FechaEnviado = DateTime.Now;
                existingStudy.UsuarioCaptura = usuarioId.ToString();
            }


            existingStudy.EstatusId = status;

            await _repository.UpdateStatusStudy(existingStudy);
        }

        public async Task<ClinicResults> GetLaboratoryResults(int RequestStudyId)
        {
            return await _repository.GetLabResultsById(RequestStudyId);
        }

        public async Task<ClinicalResultsPathological> GetResultPathological(int RequestStudyId)
        {
            return await _repository.GetResultPathologicalById(RequestStudyId);
        }

        public async Task<List<ClinicResultsFormDto>> GetLabResultsById(string id)
        {
            Helpers.ValidateGuid(id, out Guid guid);

            var results = await _repository.GetResultsById(guid);

            if (results == null)
            {
                throw new CustomException(HttpStatusCode.NotFound, Responses.NotFound);
            }

            return results.ResultsGeneric();
        }

        public async Task<Domain.Request.RequestStudy> GetRequestStudyById(int RequestStudyId)
        {
            return await _repository.GetRequestStudyById(RequestStudyId);
        }


    }
}
