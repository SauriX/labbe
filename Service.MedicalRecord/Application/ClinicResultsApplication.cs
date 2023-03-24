using Service.MedicalRecord.Application.IApplication;
using Service.MedicalRecord.Dtos;
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
using Microsoft.Extensions.Configuration;
using Service.MedicalRecord.Domain.Request;
using Service.MedicalRecord.Dtos.MassSearch;
using System.Text;
using ClosedXML.Report.Utils;
using Service.MedicalRecord.Dtos.WeeClinic;
using Service.MedicalRecord.Dtos.Appointment;
using System.Text.Json;
using Service.MedicalRecord.Dtos.General;

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
        private readonly IWeeClinicApplication _weeService;
        private readonly string MedicalRecordPath;
        private const byte PARTICULAR = 2;
        private const byte CARGA_RESULTADOS = 0;
        private const byte REEMPLAZO_RESULTADOS = 1;
        private const string NOTA = "";


        public ClinicResultsApplication(IClinicResultsRepository repository,
            IRequestRepository request,
            ICatalogClient catalogClient,
            IPdfClient pdfClient,
            ISendEndpointProvider sendEndpoint,
            IRabbitMQSettings rabbitMQSettings,
            IQueueNames queueNames,
            IConfiguration configuration,
            IWeeClinicApplication weeService)
        {
            _repository = repository;
            _request = request;
            _catalogClient = catalogClient;
            _sendEndpointProvider = sendEndpoint;
            _pdfClient = pdfClient;
            _queueNames = queueNames;
            _rabbitMQSettings = rabbitMQSettings;
            MedicalRecordPath = configuration.GetValue<string>("ClientUrls:MedicalRecord") + configuration.GetValue<string>("ClientRoutes:MedicalRecord");
            _weeService = weeService;
        }

        public async Task<(byte[] file, string fileName)> ExportList(GeneralFilterDto search)
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
            var range = template.Workbook.Worksheet("Captura de resultados clinicos").Range("Expedientes");
            var table = template.Workbook.Worksheet("Captura de resultados clinicos").Range("$A$3:" + range.RangeAddress.LastAddress).CreateTable();
            table.Theme = XLTableTheme.TableStyleMedium2;


            template.Format();

            return (template.ToByteArray(), $"Informe Captura de Resultados (Clínicos).xlsx");
        }

        public async Task<(byte[] file, string fileName)> ExportGlucoseChart(ClinicResultsFormDto result)
        {
            var studies = await _repository.GetResultsById(result.SolicitudId);
            var request = await _repository.GetRequestById(result.SolicitudId);
            var glucoseStudy = studies.Where(x => x.EstudioId == 631).Where(x => x.TipoValorId == "6" || x.TipoValorId == "1").Where(x => x.Clave != "_OB_CTG").Where(x => !string.IsNullOrWhiteSpace(x.Resultado));

            var path = Assets.ToleranciaGlucosa;
            var template = new XLTemplate(path);

            List<object> glucoseParams = new List<object>();
            var lastTime = "";

            foreach (var param in glucoseStudy.OrderBy(x => x.Orden))
            {
                var numericResult = Convert.ToDecimal(param.Resultado);
                if (param.Clave == "_GLU_SU")
                {
                    glucoseParams.Add(new
                    {
                        Estudio = "0",
                        Resultado = numericResult
                    });
                    lastTime = "CERO HORAS";
                }

                if (param.Clave == "_GLU_SU30")
                {
                    glucoseParams.Add(new
                    {
                        Estudio = "30",
                        Resultado = numericResult
                    });
                    lastTime = "MEDIA HORA";
                }

                if (param.Clave == "_GLU_SU60")
                {
                    glucoseParams.Add(new
                    {
                        Estudio = "60",
                        Resultado = numericResult
                    });
                    lastTime = "UNA HORA";
                }
                if (param.Clave == "_GLU_SU90")
                {
                    glucoseParams.Add(new
                    {
                        Estudio = "90",
                        Resultado = numericResult
                    });
                    lastTime = "HORA Y MEDIA";
                }

                if (param.Clave == "_GLU_SU120")
                {
                    glucoseParams.Add(new
                    {
                        Estudio = "120",
                        Resultado = numericResult,
                    });
                    lastTime = "DOS HORAS";
                }

                if (param.Clave == "_GLU_SU180")
                {
                    glucoseParams.Add(new
                    {
                        Estudio = "180",
                        Resultado = numericResult
                    });
                    lastTime = "TRES HORAS";
                }

                if (param.Clave == "_GLU_SU240")
                {
                    glucoseParams.Add(new
                    {
                        Estudio = "240",
                        Resultado = numericResult
                    });
                    lastTime = "CUATRO HORAS";
                }
            }

            template.AddVariable("NombrePaciente", request.Expediente.NombreCompleto);
            template.AddVariable("Medico", request.Medico.Nombre);
            template.AddVariable("Fecha", DateTime.Now.ToString("f"));
            template.AddVariable("Tiempo", lastTime);
            template.AddVariable("Estudios", glucoseParams);

            template.Generate();
            template.Format();

            return (template.ToByteArray(), $"Gráfica Curva de Tolerancia a Glucosa.xlsx");
        }

        public async Task<List<ClinicResultsDto>> GetAll(GeneralFilterDto search)
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
            var studiesDto = studies.ToRequestStudyDto().Where(x => x.AreaId != Catalogs.Area.HISTOPATOLOGIA).ToList();

            var ids = studiesDto.Select(x => x.EstudioId).ToList();
            var studiesParams = await _catalogClient.GetStudies(ids);

            var results = await _repository.GetResultsById(requestId);
            var resultsIds = results.Select(x => x.SolicitudEstudioId).ToList();
            var totalParams = studiesParams.SelectMany(x => x.Parametros).Count();

            List<RequestStudy> methodStudies = new List<RequestStudy>();

            foreach (var studyMethod in studies.Where(x => x.Metodo == null))
            {
                var method = studiesParams.FirstOrDefault(x => x.Id == studyMethod.EstudioId).Metodo;
                var studyOrder = studiesParams.FirstOrDefault(x => x.Id == studyMethod.EstudioId).Orden;
                studyMethod.OrdenEstudio = studyOrder;
                if (method != null)
                {
                    studyMethod.Metodo = method;
                    methodStudies.Add(studyMethod);
                }

            }

            if (methodStudies.Any())
            {
                await _request.BulkInsertUpdateStudies(requestId, methodStudies);
            }

            if (results.Count < totalParams)
            {
                var missingParams = new List<ParameterListDto>();

                foreach (var currentStudy in studiesParams)
                {
                    var parameters = currentStudy.Parametros;
                    var study = studiesDto.FirstOrDefault(x => x.EstudioId == currentStudy.Id && !resultsIds.Contains(x.Id));

                    if (study != null)
                    {
                        foreach (var parameter in parameters)
                        {
                            if (!results.Any(x => x.ParametroId.ToString() == parameter.Id && x.EstudioId == currentStudy.Id))
                            {
                                parameter.SolicitudEstudioId = study.Id;
                                parameter.EstudioId = currentStudy.Id;
                                missingParams.Add(parameter);
                            }
                            else
                            {
                                results.Remove(results.First(x => x.ParametroId.ToString() == parameter.Id && x.EstudioId == currentStudy.Id));
                            }
                        }
                        resultsIds.Add(study.Id);
                        study.Metodo = currentStudy.Metodo;
                        study.OrdenEstudio = currentStudy.Orden;
                    }
                }


                var newResults = missingParams.Select((x, i) => new ClinicResults
                {
                    Id = Guid.NewGuid(),
                    SolicitudId = requestId,
                    Nombre = x.Nombre,
                    Clave = x.Clave,
                    TipoValorId = x.TipoValor,
                    Resultado = null,
                    ValorInicial = x.TipoValores.FirstOrDefault()?.ValorInicial.ToString(),
                    ValorFinal = x.TipoValores.FirstOrDefault()?.ValorFinal.ToString(),
                    CriticoMinimo = x.TipoValores.FirstOrDefault()?.CriticoMinimo,
                    CriticoMaximo = x.TipoValores.FirstOrDefault()?.CriticoMaximo,
                    ParametroId = Guid.Parse(x.Id),
                    SolicitudEstudioId = x.SolicitudEstudioId,
                    Unidades = x.UnidadNombre,
                    NombreCorto = x.NombreCorto,
                    EstudioId = x.EstudioId,
                    Formula = x.Formula,
                    UltimoResultado = x?.UltimoResultado,
                    UltimaSolicitudId = x?.UltimaSolicitudId,
                    DeltaCheck = x.DeltaCheck,
                    Orden = i,
                    FCSI = x.FCSI
                }).ToList();

                // Crear Los que no existen
                await _repository.CreateLabResults(newResults);

                results = await _repository.GetResultsById(requestId);
            }

            foreach (var study in studiesDto)
            {
                var st = studiesParams.FirstOrDefault(x => x.Id == study.EstudioId);
                if (st == null) continue;

                study.Parametros = JsonSerializer.Deserialize<List<ParameterListDto>>(JsonSerializer.Serialize(st.Parametros));
                study.Indicaciones = st.Indicaciones;
                study.Metodo = st.Metodo;
                study.OrdenEstudio = st.Orden;
            }

            foreach (var result in results)
            {
                var study = studiesDto.FirstOrDefault(x => x.Id == result.SolicitudEstudioId);
                if (study == null) continue;

                var st = studiesParams.FirstOrDefault(x => x.Id == study.EstudioId);
                if (st == null) continue;

                var param = study.Parametros.FirstOrDefault(x => x.Id == result.ParametroId.ToString() && !x.Asignado);

                //foreach (var param in study.Parametros)
                //{
                if (!string.IsNullOrWhiteSpace(result.Formula) && !string.IsNullOrWhiteSpace(result.Resultado))
                {
                    param.Resultado = GetFormula(results.Where(x => x.SolicitudEstudioId == study.Id && x.Clave != null).ToList(), result.Formula);
                }
                else
                {
                    param.Resultado = result.Resultado;
                    param.ObservacionesId = result.ObservacionesId ?? "";
                }
                param.ResultadoId = result.Id.ToString();
                param.Formula = result.Formula;
                param.Asignado = true;

                if (param.DeltaCheck)
                {
                    var listRequests = await _repository.GetSecondLastRequest(result.Solicitud.ExpedienteId);
                    var previousResult = listRequests.Where(x => x.Id != result.SolicitudId)
                        .Where(x => x.FechaCreo < result.Solicitud.FechaCreo)
                        .SelectMany(x => x.Estudios)
                        .Where(x => x.EstudioId == st.Id)
                        .SelectMany(x => x.Resultados)
                        .Where(x => x.ParametroId.ToString() == param.Id)
                        .FirstOrDefault();

                    if (previousResult != null)
                    {
                        param.UltimoResultado = previousResult.Resultado == null ? null : previousResult.Resultado;

                        var previousRequest = await _repository.GetRequestById(previousResult.SolicitudId);
                        if (previousRequest != null)
                        {
                            param.UltimaSolicitud = previousRequest.Clave == null ? null : previousRequest.Clave;
                            param.UltimaSolicitudId = previousRequest.Id;
                            param.UltimoExpedienteId = previousRequest.ExpedienteId;
                        }
                    }

                }

                if (param.TipoValores != null && param.TipoValores.Count != 0)
                {
                    var ageRange = param.TipoValores.Where(x => request.Expediente.Edad >= x.RangoEdadInicial && request.Expediente.Edad <= x.RangoEdadFinal);

                    switch (param.TipoValor)
                    {
                        case "1":
                            param.ValorInicial = param.TipoValores.FirstOrDefault().ValorInicial.ToString();
                            param.ValorFinal = param.TipoValores.FirstOrDefault().ValorFinal.ToString();
                            param.CriticoMinimo = param.TipoValores.FirstOrDefault().CriticoMinimo;
                            param.CriticoMaximo = param.TipoValores.FirstOrDefault().CriticoMaximo;
                            break;
                        case "2":
                            if (request.Expediente.Genero == "F")
                            {
                                param.ValorInicial = param.TipoValores.FirstOrDefault().MujerValorInicial.ToString();
                                param.ValorFinal = param.TipoValores.FirstOrDefault().MujerValorFinal.ToString();
                                param.CriticoMinimo = param.TipoValores.FirstOrDefault().MujerCriticoMinimo;
                                param.CriticoMaximo = param.TipoValores.FirstOrDefault().MujerCriticoMaximo;
                            }
                            else if (request.Expediente.Genero == "M")
                            {
                                param.ValorInicial = param.TipoValores.FirstOrDefault().HombreValorInicial.ToString();
                                param.ValorFinal = param.TipoValores.FirstOrDefault().HombreValorFinal.ToString();
                                param.CriticoMinimo = param.TipoValores.FirstOrDefault().HombreCriticoMinimo;
                                param.CriticoMaximo = param.TipoValores.FirstOrDefault().HombreCriticoMaximo;
                            }
                            break;
                        case "3":
                            foreach(var tipoValor in param.TipoValores)
                            {

                            }
                            if (ageRange.Count() > 0)
                            {
                                param.ValorInicial = ageRange.FirstOrDefault().ValorInicialNumerico.ToString();
                                param.ValorFinal = ageRange.FirstOrDefault().ValorFinalNumerico.ToString();
                                param.CriticoMinimo = ageRange.FirstOrDefault().CriticoMinimo;
                                param.CriticoMaximo = ageRange.FirstOrDefault().CriticoMaximo;
                            }
                            break;
                        case "4":
                            if (ageRange.Count() > 0 && request.Expediente.Genero == "F")
                            {
                                param.ValorInicial = ageRange.FirstOrDefault().ValorInicialNumerico.ToString();
                                param.ValorFinal = ageRange.FirstOrDefault().ValorFinalNumerico.ToString();
                                param.CriticoMinimo = ageRange.FirstOrDefault().MujerCriticoMinimo;
                                param.CriticoMaximo = ageRange.FirstOrDefault().MujerCriticoMinimo;
                            }
                            else if (ageRange.Count() > 0 && request.Expediente.Genero == "M")
                            {
                                param.ValorInicial = ageRange.FirstOrDefault().ValorInicialNumerico.ToString();
                                param.ValorFinal = ageRange.FirstOrDefault().ValorFinalNumerico.ToString();
                                param.CriticoMinimo = ageRange.FirstOrDefault().HombreCriticoMinimo;
                                param.CriticoMaximo = ageRange.FirstOrDefault().HombreCriticoMaximo;
                            }
                            break;
                        case "5":
                        case "6":
                        case "11":
                        case "12":
                        case "13":
                        case "14":
                            param.TipoValores = param.TipoValores;
                            break;
                        case "9":
                            param.ValorInicial = "\n";
                            break;
                    }
                }
            }

            var data = new RequestStudyUpdateDto()
            {
                Estudios = studiesDto.OrderBy(x => x.OrdenEstudio).ToList(),
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
        private async Task<WeeUploadFileDto> UploadResultFile(byte[] pdfBytes, string nameFile)
        {
            using var stream = new MemoryStream(pdfBytes);

            IFormFile file = new FormFile(stream, 0, stream.Length, "UploadedImage", nameFile)
            {
                Headers = new HeaderDictionary(),
                ContentType = "application/pdf"
            };

            var assignment = await _weeService.UploadResultFile(file);

            return assignment;

        }
        private async Task<WeeUploadFileDto> RelateResultFile(string idServicio, string idNodo, string idArchivo, string nota, int isRemplazarOrnew)
        {
            var assignment = await _weeService.RelateResultFile(idServicio, idNodo, idArchivo, nota, isRemplazarOrnew);

            return assignment;
        }
        private List<RequestStudy> canSendResultResultsReady(Request request, int requestStudyId)
        {
            //devuelve los estudios que pueden enviar siempre y cuando todos los estudios de ese día se encuentren en status liberado

            var resultsPerDay = request.Estudios
                .ToList()
                .GroupBy(x => x.FechaEntrega.Date);


            List<RequestStudy> requestStudies = new List<RequestStudy>();

            foreach (var results in resultsPerDay)
            {
                if (results
                    .All(x => (x.EstatusId != Status.RequestStudy.Enviado || x.EstatusId != Status.RequestStudy.Cancelado) && (x.EstatusId == Status.RequestStudy.Liberado))
                    )
                {
                    requestStudies.AddRange(results.ToList());

                }
            }

            return requestStudies;
        }

        private bool canSendResultBalance(Request request)
        {

            return request.Procedencia == PARTICULAR && request.Saldo != 0 ? false : true;

        }

        public async Task UpdateLabResults(List<ClinicResultsFormDto> results, bool EnvioManual)
        {
            var request = (await _repository.GetLabResultsById(results.First().SolicitudEstudioId)).FirstOrDefault();
            var existingRequest = await _repository.GetRequestById(request.SolicitudId);

            var user = results.First().Usuario;
            var userId = results.First().UsuarioId;

            if (results.Count() == 0)
            {
                throw new CustomException(HttpStatusCode.Conflict, Responses.NotPossible);
            }

            if (request.SolicitudEstudio.EstatusId == Status.RequestStudy.Solicitado)
            {
                var newResults = results.ToCaptureResults();

                await _repository.UpdateLabResults(newResults);
                await UpdateStatusStudy(request.SolicitudEstudioId, request.SolicitudEstudio.EstatusId, user);
            }
            else if (request.SolicitudEstudio.EstatusId == Status.RequestStudy.Capturado)
            {
                var newResults = results.ToCaptureResults();
                for (int i = 0; i < newResults.Count; i++)
                {
                    if (!string.IsNullOrWhiteSpace(newResults[i].Formula) || newResults[i].Formula.Trim().Length > 0)
                        newResults[i].Resultado = GetFormula(newResults, newResults[i].Formula);
                }
                await _repository.UpdateLabResults(newResults);
                await UpdateStatusStudy(request.SolicitudEstudioId, request.SolicitudEstudio.EstatusId, user);
            }
            else if (request.SolicitudEstudio.EstatusId == Status.RequestStudy.Liberado || EnvioManual)
            {
                if (request.Solicitud.Parcialidad || EnvioManual)
                {
                    List<int> labResults = existingRequest.Estudios
                        .Where(x => x.AreaId != Catalogs.Area.HISTOPATOLOGIA)
                        .Where(x => x.AreaId != Catalogs.Area.CITOLOGIA)
                        .Where(x => x.Id == request.SolicitudEstudioId)
                        .Select(x => x.Id).ToList();

                    List<RequestStudy> labStudies = existingRequest.Estudios
                        .Where(x => x.AreaId != Catalogs.Area.HISTOPATOLOGIA)
                        .Where(x => x.AreaId != Catalogs.Area.CITOLOGIA)
                        .Where(x => x.Id == request.SolicitudEstudioId)
                        .Select(x => x).ToList();

                    List<int> studyLabResults = new List<int> { };
                    List<ClinicResults> resultsTask = new List<ClinicResults>();



                    foreach (var resultPath in labResults)
                    {
                        var finalResult = await _repository.GetLabResultsById(resultPath);
                        resultsTask.AddRange(finalResult);
                        studyLabResults.AddRange(resultsTask.Select(x => x.EstudioId));

                        //--- creacion de archivos individuales weeclinic ---
                        if (finalResult.First().Solicitud.EsWeeClinic)
                        {

                            List<ClinicResults> resultsTaskWee = finalResult;
                            List<int> studyLabResultsWee = resultsTaskWee.Select(x => x.EstudioId).ToList();
                            var paramReferenceValuesWee = await ReferencesValues(studyLabResultsWee);
                            var existingLabResultsPdfWee = resultsTask.ToResults(true, true, true, paramReferenceValuesWee);

                            byte[] pdfBytesWee = await _pdfClient.GenerateLabResults(existingLabResultsPdfWee);
                            string namePdfWee = string.Concat(request.Solicitud.Clave, ".pdf");
                            string pathPdfWee = await SaveResulstPdfPath(pdfBytesWee, namePdfWee);

                            var uploadedFileWee = await UploadResultFile(pdfBytesWee, namePdfWee);

                            await RelateResultFile(finalResult.First().SolicitudEstudio.EstudioWeeClinic.IdServicio, finalResult.First().SolicitudEstudio.EstudioWeeClinic.IdNodo, uploadedFileWee.IdArchivo, NOTA, finalResult.First().SolicitudEstudio.IdArchivoWeeClinic == null ? CARGA_RESULTADOS : REEMPLAZO_RESULTADOS);

                            await UpdateIdArchivoWeeClinic(uploadedFileWee.IdArchivo, finalResult.First().SolicitudEstudio.Id);
                        }
                    }

                    var paramReferenceValues = await ReferencesValues(studyLabResults);

                    var existingLabResultsPdf = resultsTask.ToResults(true, true, true, paramReferenceValues);

                    byte[] pdfBytes = await _pdfClient.GenerateLabResults(existingLabResultsPdf);
                    string namePdf = string.Concat(request.Solicitud.Clave, ".pdf");
                    string pathPdf = await SaveResulstPdfPath(pdfBytes, namePdf);

                    var pathName = Path.Combine(MedicalRecordPath, pathPdf.Replace("wwwroot/", "")).Replace("\\", "/");

                    var files = new List<SenderFiles>()
                        {
                            new SenderFiles(new Uri(pathName), namePdf)
                        };

                    try
                    {
                        //if (canSendResultBalance(request.Solicitud) || EnvioManual)
                        //{
                        var resultsToSend = canSendResultResultsReady(existingRequest, results.First().SolicitudEstudioId);
                        if (resultsToSend.Count() > 0)
                        {

                            if (!string.IsNullOrEmpty(request.Solicitud.EnvioWhatsApp))
                            {
                                await SendTestWhatsapp(files, request.Solicitud.EnvioWhatsApp, userId, existingRequest.Expediente.NombreCompleto, existingRequest.Clave);

                            }
                            if (!string.IsNullOrEmpty(request.Solicitud.EnvioCorreo))
                            {
                                await SendTestEmail(files, request.Solicitud.EnvioCorreo, userId, existingRequest.Expediente.NombreCompleto, existingRequest.Clave);

                            }
                            await UpdateStatusStudy(request.SolicitudEstudioId, Status.RequestStudy.Enviado, user);

                            string descripcion = getDescriptionRecord(request.Clave, existingRequest.EnvioWhatsApp, existingRequest.EnvioCorreo);

                            await CreateHistoryRecord(existingRequest.Id, request.SolicitudEstudioId, descripcion, user, existingRequest.EnvioWhatsApp, existingRequest.EnvioCorreo);
                        }

                        //}
                    }
                    catch (Exception ex)
                    {
                        throw ex;
                    }
                }
                else
                {
                    await UpdateStatusStudy(request.SolicitudEstudioId, Status.RequestStudy.Liberado, user);

                    if (existingRequest.Estudios.All(estudio => estudio.EstatusId == Status.RequestStudy.Liberado || estudio.EstatusId == Status.RequestStudy.Enviado))
                    {
                        //if (canSendResultBalance(request.Solicitud) || EnvioManual)
                        //{
                        

                        await SendResultsFiles(request.SolicitudId, userId, user, existingRequest.Estudios.ToList());
                        //}
                    }
                }
            }
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

        public async Task SaveResultPathologicalStudy(ClinicalResultPathologicalFormDto result)
        {
            var newResult = result.ToClinicalResultPathological();

            await _repository.CreateResultPathological(newResult);

            if (result.ImagenPatologica != null)
            {
                for (int i = 0; i < result.ImagenPatologica.Count; i++)
                {
                    await SaveImageGetPath(result.ImagenPatologica[i], newResult.SolicitudEstudioId);
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
        public static async Task<string> SavePdfGetPath(byte[] pdf, string name)
        {
            var path = "wwwroot/temp/pdf";

            await File.WriteAllBytesAsync(Path.Combine(path, name), pdf);

            return Path.Combine(path, name);
        }

        public static async Task<string> SaveResulstPdfPath(byte[] pdf, string name)
        {
            var path = "wwwroot/temp/labResults";

            await File.WriteAllBytesAsync(Path.Combine(path, name), pdf);

            return Path.Combine(path, name);
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

        public async Task UpdateResultPathologicalStudy(ClinicalResultPathologicalFormDto result, bool EnvioManual)
        {
            var existing = await _repository.GetResultPathologicalById(result.RequestStudyId);

            if (existing == null)
            {
                throw new CustomException(HttpStatusCode.NotFound, SharedResponses.NotFound);
            }
            if (existing.SolicitudEstudio.EstatusId == Status.RequestStudy.Solicitado)
            {

                var newResult = result.ToUpdateClinicalResultPathological(existing);

                await _repository.UpdateResultPathologicalStudy(newResult);

                if (result.ListaImagenesCargadas != null)
                {
                    for (int i = 0; i < result.ListaImagenesCargadas.Length; i++)
                    {
                        await DeleteImageGetPath(result.ListaImagenesCargadas[i], newResult.SolicitudEstudioId);
                    }
                }
                if (result.ImagenPatologica != null)
                {
                    for (int i = 0; i < result.ImagenPatologica.Count; i++)
                    {
                        await SaveImageGetPath(result.ImagenPatologica[i], newResult.SolicitudEstudioId);
                    }
                }
                await this.UpdateStatusStudy(existing.SolicitudEstudioId, existing.SolicitudEstudio.EstatusId, result.Usuario);
            }
            if (existing.SolicitudEstudio.EstatusId == Status.RequestStudy.Capturado)
            {
                await this.UpdateStatusStudy(existing.SolicitudEstudioId, existing.SolicitudEstudio.EstatusId, result.Usuario);
            }
            if (result.Estatus == Status.RequestStudy.Liberado || EnvioManual)
            {
                if (existing.Solicitud.Parcialidad || EnvioManual) // envio manual o parcial de resultados 👻
                {
                    await UpdateStatusStudy(existing.SolicitudEstudioId, existing.SolicitudEstudio.EstatusId, result.Usuario);
                    List<ClinicalResultsPathological> toSendInfoPathological = new List<ClinicalResultsPathological> { existing };

                    var existingResultPathologyPdf = toSendInfoPathological.toInformationPdfResult(true);

                    byte[] pdfBytes = await _pdfClient.GeneratePathologicalResults(existingResultPathologyPdf);

                    string namePdf = string.Concat(existing.Solicitud.Clave, ".pdf");

                    string pathPdf = await SavePdfGetPath(pdfBytes, namePdf);

                    var pathName = Path.Combine(MedicalRecordPath, pathPdf.Replace("wwwroot/", "")).Replace("\\", "/");

                    var uploadedFile = await UploadResultFile(pdfBytes, namePdf);

                    await RelateResultFile(existing.SolicitudEstudio.EstudioWeeClinic.IdServicio, existing.SolicitudEstudio.EstudioWeeClinic.IdNodo, uploadedFile.IdArchivo, NOTA, existing.SolicitudEstudio.IdArchivoWeeClinic == null ? CARGA_RESULTADOS : REEMPLAZO_RESULTADOS);

                    await UpdateIdArchivoWeeClinic(uploadedFile.IdArchivo, existing.SolicitudEstudio.Id);

                    var files = new List<SenderFiles>()
                    {
                        new SenderFiles(new Uri(pathName), namePdf)
                    };
                    try
                    {

                        var existingRequest = await _repository.GetRequestById(existing.SolicitudId);

                        var resultsToSend = canSendResultResultsReady(existingRequest, existing.RequestStudyId);
                        if (resultsToSend.Count() > 0)
                        {
                            if (!string.IsNullOrEmpty(existing.Solicitud.EnvioWhatsApp))
                            {

                                await SendTestWhatsapp(files, existing.Solicitud.EnvioWhatsApp, result.UsuarioId, existing.Solicitud.Expediente.NombreCompleto, existing.Solicitud.Clave);
                            }
                            if (!string.IsNullOrEmpty(existing.Solicitud.EnvioCorreo))
                            {

                                await SendTestEmail(files, existing.Solicitud.EnvioCorreo, result.UsuarioId, existing.Solicitud.Expediente.NombreCompleto, existing.Solicitud.Clave);
                            }

                            await UpdateStatusStudy(existing.SolicitudEstudioId, Status.RequestStudy.Enviado, result.Usuario);

                            string descripcion = getDescriptionRecord(existing.SolicitudEstudioId.ToString(), existing.Solicitud.EnvioWhatsApp, existing.Solicitud.EnvioCorreo);

                            await CreateHistoryRecord(result.SolicitudId, result.EstudioId, descripcion, result.Usuario, existing.Solicitud.EnvioWhatsApp, existing.Solicitud.EnvioCorreo);
                        }

                    }
                    catch (Exception ex)
                    {
                        throw ex;
                    }

                }
                else
                {
                    //if (canSendResultBalance(existing.Solicitud) || EnvioManual)
                    //{

                    await UpdateStatusStudy(existing.SolicitudEstudioId, existing.SolicitudEstudio.EstatusId, result.Usuario);

                    var existingRequest = await _repository.GetRequestById(existing.SolicitudId);

                    if (existingRequest.Estudios.All(estudio => estudio.EstatusId == Status.RequestStudy.Liberado))
                    {
                        await SendResultsFiles(existing.SolicitudId, result.UsuarioId, result.Usuario, existingRequest.Estudios.ToList());
                    }
                    //}


                }
            }

        }
        public async Task<bool> SendResultFile(DeliverResultsStudiesDto estudios)
        {

            foreach (var estudiosSeleccionados in estudios.Estudios)
            {
                var existingRequest = await _repository.GetRequestById(estudiosSeleccionados.SolicitudId);

                List<RequestStudy> studiesToUpdate = new List<RequestStudy>();

                var files = new List<SenderFiles>();

                List<RequestStudy> pathologicalStudies = new List<RequestStudy>();

                List<RequestStudy> labStudies = new List<RequestStudy>();

                List<ClinicResults> resultsTask = new List<ClinicResults>();

                List<int> labResults = new List<int>();

                foreach (var estudioId in estudiosSeleccionados.EstudiosId)
                {


                    if (estudioId.Tipo == Catalogs.Area.HISTOPATOLOGIA)
                    {
                        var finalResult = await _repository.GetResultPathologicalById(estudioId.EstudioId);

                        List<ClinicalResultsPathological> resultsTaskUnique = new List<ClinicalResultsPathological>();

                        resultsTaskUnique.Add(finalResult);

                        var existingResultPathologyPdf = resultsTaskUnique.toInformationPdfResult(true);

                        byte[] pdfBytes = await _pdfClient.GeneratePathologicalResults(existingResultPathologyPdf);

                        string namePdf = string.Concat(existingRequest.Clave, "-", finalResult.SolicitudEstudio.Id, "-", finalResult.SolicitudEstudio.Clave, ".pdf");

                        string pathPdf = await SavePdfGetPath(pdfBytes, namePdf);

                        var pathName = Path.Combine(MedicalRecordPath, pathPdf.Replace("wwwroot/", "")).Replace("\\", "/");

                        files.Add(new SenderFiles(new Uri(pathName), namePdf));

                        var uploadedFile = await UploadResultFile(pdfBytes, namePdf);

                        await RelateResultFile(finalResult.SolicitudEstudio.EstudioWeeClinic.IdServicio, finalResult.SolicitudEstudio.EstudioWeeClinic.IdNodo, uploadedFile.IdArchivo, NOTA, finalResult.SolicitudEstudio.IdArchivoWeeClinic == null ? CARGA_RESULTADOS : REEMPLAZO_RESULTADOS);

                        await UpdateIdArchivoWeeClinic(uploadedFile.IdArchivo, finalResult.SolicitudEstudio.Id);
                    }
                    else
                    {
                        var finalResult = await _repository.GetLabResultsById(estudioId.EstudioId);

                        resultsTask.AddRange(finalResult);

                        labResults.AddRange(resultsTask.Select(x => x.EstudioId));
                        //--- creacion de archivos individuales weeclinic ---
                        if (finalResult.First().Solicitud.EsWeeClinic)
                        {

                            List<ClinicResults> resultsTaskWee = finalResult;
                            List<int> studyLabResultsWee = resultsTaskWee.Select(x => x.EstudioId).ToList();
                            var paramReferenceValuesWee = await ReferencesValues(studyLabResultsWee);
                            var existingLabResultsPdfWee = resultsTask.ToResults(true, true, true, paramReferenceValuesWee);

                            byte[] pdfBytesWee = await _pdfClient.GenerateLabResults(existingLabResultsPdfWee);
                            string namePdfWee = string.Concat(existingRequest.Clave, ".pdf");
                            //string pathPdfWee = await SaveResulstPdfPath(pdfBytesWee, namePdfWee);

                            var uploadedFileWee = await UploadResultFile(pdfBytesWee, namePdfWee);

                            await RelateResultFile(finalResult.First().SolicitudEstudio.EstudioWeeClinic.IdServicio, finalResult.First().SolicitudEstudio.EstudioWeeClinic.IdNodo, uploadedFileWee.IdArchivo, NOTA, finalResult.First().SolicitudEstudio.IdArchivoWeeClinic == null ? CARGA_RESULTADOS : REEMPLAZO_RESULTADOS);

                            await UpdateIdArchivoWeeClinic(uploadedFileWee.IdArchivo, finalResult.First().SolicitudEstudio.Id);
                        }
                    }

                    RequestStudy estudioActual = await _repository.GetRequestStudyById(estudioId.EstudioId);

                    if (estudioActual != null)
                    {

                        studiesToUpdate.Add(estudioActual);
                    }

                    List<string> mediosActuales = estudioActual.MedioSolicitado == null ? new List<string>() : estudioActual.MedioSolicitado?.Split(",").ToList();

                    if (estudios.MediosEnvio.Contains("Whatsapp") && !mediosActuales.Contains("Whatsapp"))
                    {
                        mediosActuales.Add("Whatsapp");
                    }
                    if (estudios.MediosEnvio.Contains("Correo") && !mediosActuales.Contains("Correo"))
                    {
                        mediosActuales.Add("Correo");
                    }
                    if (estudios.MediosEnvio.Contains("Fisico") && !mediosActuales.Contains("Fisico"))
                    {
                        mediosActuales.Add("Fisico");
                    }

                    estudioActual.MedioSolicitado = String.Join(",", mediosActuales).Trim();

                    await _repository.UpdateMedioSolicitado(estudioActual);


                }
                if (resultsTask.Count > 0)
                {
                    var paramReferenceValues = await ReferencesValues(labResults);

                    var existingLabResultsPdf = resultsTask.ToResults(true, true, true, paramReferenceValues);

                    byte[] pdfBytes = await _pdfClient.GenerateLabResults(existingLabResultsPdf);
                    string namePdf = string.Concat(existingRequest.Clave, ".pdf");
                    string pathPdf = await SaveResulstPdfPath(pdfBytes, namePdf);

                    var pathName = Path.Combine(MedicalRecordPath, pathPdf.Replace("wwwroot/", "")).Replace("\\", "/");


                    files.Add(new SenderFiles(new Uri(pathName), namePdf));
                }

                try
                {
                    //if (files.Count > 0 && canSendResultBalance(existingRequest))
                    if (files.Count > 0)
                    {

                        string correo = "";
                        string telefono = "";

                        if (estudios.MediosEnvio.Contains("Whatsapp") && !string.IsNullOrEmpty(existingRequest.EnvioWhatsApp))
                        {
                            await SendTestWhatsapp(files, existingRequest.EnvioWhatsApp, estudios.UsuarioId, existingRequest.Expediente.NombreCompleto, existingRequest.Clave);
                            telefono = existingRequest.EnvioWhatsApp;
                        }

                        if (estudios.MediosEnvio.Contains("Correo") && !string.IsNullOrEmpty(existingRequest.EnvioCorreo))
                        {

                            await SendTestEmail(files, existingRequest.EnvioCorreo, estudios.UsuarioId, existingRequest.Expediente.NombreCompleto, existingRequest.Clave);
                            correo = existingRequest.EnvioCorreo;
                        }

                        foreach (var estudio in studiesToUpdate)
                        {
                            await UpdateStatusStudy(estudio.Id, Status.RequestStudy.Enviado, estudios.Usuario);

                            string descripcion = getDescriptionRecord(estudio.Clave, telefono, correo);

                            await CreateHistoryRecord(existingRequest.Id, estudio.Id, descripcion, estudios.Usuario, telefono, correo);
                        }
                    }

                }
                catch (Exception ex)
                {
                    throw ex;
                }

            }

            return true;


        }
        public async Task SendResultsFiles(Guid solicitudId, Guid usuarioId, string usuario, List<RequestStudy> studiesToSend)
        {
            var existingRequest = await _repository.GetRequestById(solicitudId);

            List<int> labResults = studiesToSend
                .Where(x => x.AreaId != Catalogs.Area.HISTOPATOLOGIA)
                .Where(x => x.AreaId != Catalogs.Area.CITOLOGIA)
                .Where(x => x.EstatusId != Status.RequestStudy.Enviado)
                .Select(x => x.Id).ToList();

            List<int> pathologicalResults = studiesToSend
                .Where(x => x.AreaId == Catalogs.Area.HISTOPATOLOGIA)
                .Where(x => x.AreaId == Catalogs.Area.CITOLOGIA)
                .Where(x => x.EstatusId != Status.RequestStudy.Enviado)
                .Select(x => x.Id)
                .ToList();

            List<int> studyLabResults = new List<int> { };
            var files = new List<SenderFiles>();

            if (pathologicalResults.Count > 0)
            {
                List<ClinicalResultsPathological> resultsTask = new List<ClinicalResultsPathological>();

                foreach (var resultPathId in pathologicalResults)
                {
                    var finalResult = await _repository.GetResultPathologicalById(resultPathId);

                    resultsTask.Add(finalResult);
                }

                //var files = new List<SenderFiles>();

                foreach (var resultTask in resultsTask)
                {
                    List<ClinicalResultsPathological> resultsTaskUnique = new List<ClinicalResultsPathological>();

                    resultsTaskUnique.Add(resultTask);

                    var existingResultPathologyPdf = resultsTaskUnique.toInformationPdfResult(true);

                    byte[] pdfBytes = await _pdfClient.GeneratePathologicalResults(existingResultPathologyPdf);

                    string namePdf = string.Concat(existingRequest.Clave, "-", resultTask.SolicitudEstudio.Id, "-", resultTask.SolicitudEstudio.Clave, ".pdf");

                    string pathPdf = await SavePdfGetPath(pdfBytes, namePdf);

                    var pathName = Path.Combine(MedicalRecordPath, pathPdf.Replace("wwwroot/", "")).Replace("\\", "/");

                    files.Add(new SenderFiles(new Uri(pathName), namePdf));

                    var uploadedFile = await UploadResultFile(pdfBytes, namePdf);

                    await RelateResultFile(resultTask.SolicitudEstudio.EstudioWeeClinic.IdServicio, resultTask.SolicitudEstudio.EstudioWeeClinic.IdNodo, uploadedFile.IdArchivo, NOTA, resultTask.SolicitudEstudio.IdArchivoWeeClinic == null ? CARGA_RESULTADOS : REEMPLAZO_RESULTADOS);

                    await UpdateIdArchivoWeeClinic(uploadedFile.IdArchivo, resultTask.SolicitudEstudio.Id);
                }
            }

            if (labResults.Count > 0)
            {
                List<ClinicResults> resultsTask = new List<ClinicResults>();

                foreach (var resultPath in labResults)
                {
                    var finalResult = await _repository.GetLabResultsById(resultPath);
                    resultsTask.AddRange(finalResult);
                    studyLabResults.AddRange(resultsTask.Select(x => x.EstudioId));

                    //--- creacion de archivos individuales weeclinic ---
                    if (finalResult.First().Solicitud.EsWeeClinic)
                    {

                        List<ClinicResults> resultsTaskWee = finalResult;
                        List<int> studyLabResultsWee = resultsTaskWee.Select(x => x.EstudioId).ToList();
                        var paramReferenceValuesWee = await ReferencesValues(studyLabResultsWee);
                        var existingLabResultsPdfWee = resultsTask.ToResults(true, true, true, paramReferenceValuesWee);

                        byte[] pdfBytesWee = await _pdfClient.GenerateLabResults(existingLabResultsPdfWee);
                        string namePdfWee = string.Concat(existingRequest.Clave, ".pdf");
                        //string pathPdfWee = await SaveResulstPdfPath(pdfBytesWee, namePdfWee);

                        var uploadedFileWee = await UploadResultFile(pdfBytesWee, namePdfWee);

                        await RelateResultFile(finalResult.First().SolicitudEstudio.EstudioWeeClinic.IdServicio, finalResult.First().SolicitudEstudio.EstudioWeeClinic.IdNodo, uploadedFileWee.IdArchivo, NOTA, finalResult.First().SolicitudEstudio.IdArchivoWeeClinic == null ? CARGA_RESULTADOS : REEMPLAZO_RESULTADOS);

                        await UpdateIdArchivoWeeClinic(uploadedFileWee.IdArchivo, finalResult.First().SolicitudEstudio.Id);
                    }
                }

                var paramReferenceValues = await ReferencesValues(studyLabResults);

                var existingLabResultsPdf = resultsTask.ToResults(true, true, true, paramReferenceValues);

                byte[] pdfBytes = await _pdfClient.GenerateLabResults(existingLabResultsPdf);
                string namePdf = string.Concat(existingRequest.Clave, ".pdf");
                string pathPdf = await SaveResulstPdfPath(pdfBytes, namePdf);

                var pathName = Path.Combine(MedicalRecordPath, pathPdf.Replace("wwwroot/", "")).Replace("\\", "/");

                files.Add(new SenderFiles(new Uri(pathName), namePdf));

            }
            try
            {
                //if (files.Count > 0 && canSendResultBalance(existingRequest))
                //{


                if (!string.IsNullOrEmpty(existingRequest.EnvioWhatsApp))
                {
                    await SendTestWhatsapp(files, existingRequest.EnvioWhatsApp, usuarioId, existingRequest.Expediente.NombreCompleto, existingRequest.Clave);
                }

                if (!string.IsNullOrEmpty(existingRequest.EnvioCorreo))
                {
                    await SendTestEmail(files, existingRequest.EnvioCorreo, usuarioId, existingRequest.Expediente.NombreCompleto, existingRequest.Clave);
                }

                foreach (var estudio in existingRequest.Estudios)
                {
                    await UpdateStatusStudy(estudio.Id, Status.RequestStudy.Enviado, usuario);

                    string descripcion = getDescriptionRecord(estudio.Clave, existingRequest.EnvioWhatsApp, existingRequest.EnvioCorreo);

                    await CreateHistoryRecord(existingRequest.Id, estudio.Id, descripcion, usuario, existingRequest.EnvioWhatsApp, existingRequest.EnvioCorreo);

                }
                //}
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public async Task SendTestEmail(List<SenderFiles> senderFiles, string correo, Guid usuario, string nombrePaciente, string claveSolicitud)
        {

            var subject = RequestTemplates.Subjects.PathologicalSubject;
            var title = RequestTemplates.Titles.PathologicalTitle;
            var message = $"{nombrePaciente}, para LABPRATORIOS RAMOS ha sido un placer atenderte, a continuación se brindan los resultados de la solicitud ${claveSolicitud}\n" +
                "\n" +
                "Te recordamos que también puedes descargar tu resultados desde nuestra página web https://www.laboratorioramos.com.mx necesitaras tu número de expediente y contraseña proporcionados en tu recibo de pago."; ;


            var emailToSend = new EmailContract(correo, subject, title, message, senderFiles)
            {
                Notificar = true,
                RemitenteId = usuario.ToString()
            };

            var endpoint = await _sendEndpointProvider.GetSendEndpoint(new Uri(string.Concat(_rabbitMQSettings.Host, "/", _queueNames.Email)));

            await endpoint.Send(emailToSend);


        }

        public async Task SendTestWhatsapp(List<SenderFiles> senderFiles, string telefono, Guid usuario, string nombrePaciente, string claveSolicitud)
        {

            var message = $"{nombrePaciente}, para LABPRATORIOS RAMOS ha sido un placer atenderte, a continuación se brindan los resultados de la solicitud {claveSolicitud}\n" +
                "\n" +
                "Te recordamos que también puedes descargar tu resultados desde nuestra página web https://www.laboratorioramos.com.mx necesitaras tu número de expediente y contraseña proporcionados en tu recibo de pago.";


            var phone = telefono.Replace("-", "");
            phone = phone.Length == 10 ? "52" + phone : phone;

            var emailToSend = new WhatsappContract(phone, message, senderFiles)
            {
                Notificar = true,
                RemitenteId = usuario.ToString()
            };

            var endpoint = await _sendEndpointProvider.GetSendEndpoint(new Uri(string.Concat(_rabbitMQSettings.Host, "/", _queueNames.Whatsapp)));

            await endpoint.Send(emailToSend);


        }
        private string getDescriptionRecord(string clave, string numero = "", string correo = "")
        {
            string descripcion = "Resultado del estudio " + clave + " ";
            if (!string.IsNullOrEmpty(numero) || !string.IsNullOrEmpty(correo))
            {
                List<string> medios = new List<string>();
                descripcion += "[";
                if (!string.IsNullOrEmpty(numero))
                {
                    medios.Add("WhatsApp ");
                }
                
                if (!string.IsNullOrEmpty(correo))
                {
                    medios.Add("Correo");
                }
                descripcion += string.Join(" | ", medios) + "]";
            }
            else
            {
                descripcion += "Sin medios configurados";
            }
            return descripcion.Trim();
        }
        public async Task CreateHistoryRecord(Guid solicituId, int solicitudEstudioId, string descripcion, string usuario, string numero = "", string correo = "")
        {
            if (!string.IsNullOrEmpty(numero) || !string.IsNullOrEmpty(correo))
            {

                var record = new DeliveryHistory
                {
                    Id = Guid.NewGuid(),
                    Numero = numero,
                    Correo = correo,
                    FechaCreo = DateTime.Now,
                    UsuarioNombre = usuario,
                    Descripcion = descripcion,
                    SolicitudEstudioId = solicitudEstudioId,
                    SolicitudId = solicituId

                };

                await _repository.CreateHistoryRecord(record);
            }


        }
        public async Task<List<DeliveryHistoryDto>> CreateNoteHistoryRecord(HistoryRecordInfo record)
        {
            var registro = new DeliveryHistory
            {
                Id = Guid.NewGuid(),
                FechaCreo = DateTime.Now,
                UsuarioNombre = record.Usuario,
                Descripcion = record.Descripcion,
                SolicitudId = record.SolicitudId
            };

            await _repository.CreateHistoryRecord(registro);

            return await GetDeliveryHistoryByRequestId(record.SolicitudId);
        }
        public async Task<List<DeliveryHistoryDto>> GetDeliveryHistoryByRequestId(Guid Id)
        {
            var deliveryHistory = await _repository.GetHistoryRecordsByRequestId(Id);

            return deliveryHistory.Select(x => new DeliveryHistoryDto
            {
                Fecha = x.FechaCreo.ToString("dd/MM/yyyy HH:mm"),
                Numero = x.Numero,
                Correo = x.Correo,
                Usuario = x.UsuarioNombre,
                Descripcion = x.Descripcion
                
            }).ToList();

        }
        public async Task<byte[]> PrintSelectedStudies(ConfigurationToPrintStudies configuration)
        {
            List<int> labResults = new List<int> { };
            List<int> studyLabResults = new List<int> { };
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

            List<ClinicalResultsPathological> resultsTask = new List<ClinicalResultsPathological>();
            List<ClinicResults> labResultsTask = new List<ClinicResults>();

            foreach (var resultPathId in pathologicalResults)
            {
                var finalResult = await _repository.GetResultPathologicalById(resultPathId);

                resultsTask.Add(finalResult);
            }

            foreach (var resultLabPathId in labResults)
            {
                var finalResult = await _repository.GetLabResultsById(resultLabPathId);
                labResultsTask.AddRange(finalResult.Where(x => x.SolicitudEstudioId == resultLabPathId));
                studyLabResults.AddRange(finalResult.Where(x => x.TipoValorId == "11").Select(x => x.EstudioId));
            }

            var paramReferenceValues = await ReferencesValues(studyLabResults);

            var existingResultPathologyPdf = resultsTask.toInformationPdfResult(configuration.ImprimirLogos);
            var existingLabResultPdf = labResultsTask.ToList().ToResults(configuration.ImprimirLogos, configuration.ImprimirCriticos, configuration.ImprimirPrevios, paramReferenceValues);

            byte[] pdfBytes = await _pdfClient.MergeResults(existingResultPathologyPdf, existingLabResultPdf);

            return pdfBytes;
        }

        public async Task<List<ParameterValueDto>> ReferencesValues(List<int> studies)
        {
            var studiesParams = await _catalogClient.GetStudies(studies);
            var paramReferenceValues = studiesParams.SelectMany(x => x.Parametros.Where(y => y.TipoValor == "11")).SelectMany(x => x.TipoValores).ToList();

            return paramReferenceValues;
        }
        public async Task UpdateIdArchivoWeeClinic(string idArchivo, int RequestStudyId)
        {
            var existingStudy = await _repository.GetStudyById(RequestStudyId);

            if (existingStudy == null)
            {
                throw new CustomException(HttpStatusCode.NotFound, SharedResponses.NotFound);
            }

            existingStudy.IdArchivoWeeClinic = idArchivo;

            await _repository.UpdateStatusStudy(existingStudy);

        }

        public async Task UpdateStatusStudy(int RequestStudyId, byte status, string usuario)
        {
            var existingStudy = await _repository.GetStudyById(RequestStudyId);

            if (existingStudy == null)
            {
                throw new CustomException(HttpStatusCode.NotFound, SharedResponses.NotFound);
            }

            if (Status.RequestStudy.Capturado == status && existingStudy.EstatusId == Status.RequestStudy.Solicitado)
            {
                existingStudy.FechaCaptura = DateTime.Now;
                existingStudy.UsuarioCaptura = usuario.ToString();
            }
            if (Status.RequestStudy.Validado == status && existingStudy.EstatusId == Status.RequestStudy.Capturado)
            {
                existingStudy.FechaValidacion = DateTime.Now;
                existingStudy.UsuarioValidacion = usuario.ToString();
            }
            if (Status.RequestStudy.Liberado == status && existingStudy.EstatusId == Status.RequestStudy.Validado)
            {
                existingStudy.FechaLiberado = DateTime.Now;
                existingStudy.UsuarioLiberado = usuario.ToString();
            }
            if (Status.RequestStudy.Enviado == status)
            {
                existingStudy.FechaEnviado = DateTime.Now;
                existingStudy.UsuarioEnviado = usuario.ToString();
            }


            existingStudy.EstatusId = status;
            existingStudy.FechaModifico = DateTime.Now;

            await _repository.UpdateStatusStudy(existingStudy);
        }

        public async Task<ClinicResultsPathologicalInfoDto> GetResultPathological(int RequestStudyId)
        {
            var results = await _repository.GetResultPathologicalById(RequestStudyId);
            if (results == null)
            {
                //throw new CustomException(HttpStatusCode.NotFound, Responses.NotFound);
                return null;
            }
            return results.ToPathologicalInfoDto();
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

        private string GetFormula(List<ClinicResults> parameters, string formula)
        {
            StringBuilder message = new(formula);

            foreach (var par in parameters.OrderByDescending(x => x.Clave.Length).Where(x => !string.IsNullOrEmpty(x.Resultado)))
            {
                message.Replace(par.Clave, par.Resultado);
            }

            var str4 = "(" + message.Replace(" ", "").ToString().ToLower() + ")";
            str4 = str4.Replace(")(", ")*(");

            while (str4.Contains('('))
            {
                Console.WriteLine(str4);

                var sub1 = str4[(str4.LastIndexOf("(") + 1)..];
                var sub1Bef = str4[..(str4.IndexOf(sub1) - 1)];

                var isRound = sub1Bef.EndsWith("round");
                var isFormat = sub1Bef.EndsWith("format");

                var sub = sub1[..sub1.IndexOf(isRound || isFormat ? "," : ")")];
                var sub2 = sub;

                string str21 = sub2.Replace("^", "~^~").Replace("/", "~/~").Replace("*", "~*~").Replace("+", "~+~").Replace("-", "~-~");
                List<string> str31 = str21.Split('~').ToList();

                while (str31.Count > 1)
                {
                    str31 = str31.Where(x => !string.IsNullOrEmpty(x)).ToList();

                    if (str31.Contains("-"))
                    {
                        for (int i = 0; i < str31.Count; i++)
                        {
                            if (str31[i] == "-" && (i == 0 || str31[i - 1].In("-", "+", "*", "/")))
                            {
                                str31[i + 1] = "-" + str31[i + 1];
                                str31.RemoveRange(i, 1);
                                i--;
                            }
                        }
                    }

                    while (str31.Contains("*"))
                    {
                        for (int i = 0; i < str31.Count; i++)
                        {
                            if (str31[i] == "*")
                            {
                                var val = Convert.ToDecimal(str31[i - 1]) * Convert.ToDecimal(str31[i + 1]);
                                str31.RemoveRange(i - 1, 3);
                                str31.Insert(i - 1, val.ToString());
                            }
                        }
                    }
                    while (str31.Contains("/"))
                    {
                        for (int i = 0; i < str31.Count; i++)
                        {
                            if (str31[i] == "/")
                            {
                                var val = Convert.ToDecimal(str31[i - 1]) / Convert.ToDecimal(str31[i + 1]);
                                str31.RemoveRange(i - 1, 3);
                                str31.Insert(i - 1, val.ToString());
                            }
                        }
                    }
                    while (str31.Contains("+"))
                    {
                        for (int i = 0; i < str31.Count; i++)
                        {
                            if (str31[i] == "+")
                            {
                                var val = Convert.ToDecimal(str31[i - 1]) + Convert.ToDecimal(str31[i + 1]);
                                str31.RemoveRange(i - 1, 3);
                                str31.Insert(i - 1, val.ToString());
                            }
                        }
                    }
                    while (str31.Contains("-"))
                    {
                        for (int i = 0; i < str31.Count; i++)
                        {
                            if (str31[i] == "-")
                            {
                                var val = Convert.ToDecimal(str31[i - 1]) - Convert.ToDecimal(str31[i + 1]);
                                str31.RemoveRange(i - 1, 3);
                                str31.Insert(i - 1, val.ToString());
                            }
                        }
                    }
                }

                if (isRound || isFormat)
                {
                    var formVal = sub1.Substring(sub1.IndexOf(",") + 1, sub1.IndexOf(")") - sub1.IndexOf(",") - 1);
                    str31[0] = Math.Round(Convert.ToDecimal(str31[0]), Convert.ToInt32(formVal)).ToString();
                    str4 = str4.Replace((isRound ? "round" : "format") + "(" + sub + $",{formVal})", str31[0].ToString());
                }
                else
                {
                    str4 = str4.Replace("(" + sub + ")", str31[0].ToString());
                }
            }

            return str4;
        }

    }
}
