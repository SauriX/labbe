using ClosedXML.Report;
using MoreLinq;
using Service.MedicalRecord.Client.IClient;
using Service.MedicalRecord.Dictionary;
using Service.MedicalRecord.Dtos.RelaseResult;
using Service.MedicalRecord.Dtos.RequestedStudy;
using Service.MedicalRecord.Mapper;
using Service.MedicalRecord.Repository.IRepository;
using Shared.Error;
using Shared.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using SharedResponses = Shared.Dictionary.Responses;
using RecordResponses = Service.MedicalRecord.Dictionary.Response;
using Service.MedicalRecord.Dtos.MassSearch;
using Shared.Dictionary;
using Service.MedicalRecord.Domain;
using Service.MedicalRecord.Domain.Request;
using Service.MedicalRecord.Application.IApplication;

namespace Service.MedicalRecord.Application
{
    public class RelaseResultApplication: IRelaseResultApplication
    {
        public readonly IRelaseResultRepository _repository;
        private readonly IClinicResultsRepository _clinicresultrepository;
        private readonly IClinicResultsApplication _clinicresultapplication;
        private readonly IPdfClient _pdfClient;
        public RelaseResultApplication(IRelaseResultRepository repository, IClinicResultsRepository clinicresultrepository, IPdfClient pdfClient, IClinicResultsApplication clinicresultapplication)
        {

            _repository = repository;
            _clinicresultrepository = clinicresultrepository;
            _pdfClient = pdfClient;
            _clinicresultapplication = clinicresultapplication;
        }

        public async Task<(byte[] file, string fileName)> ExportList(SearchRelase search)
        {
            var studies = await GetAll(search);
            foreach (var request in studies)
            {
                if (studies.Count > 0)
                {
                    request.Estudios.Insert(0, new RelaceStudyDto { Study = "Clave", Registro = "Fecha de Registro", Entrega = "Fecha de Entrega", Status = "Estatus" });
                }
            }


            var path = Assets.InformeExpedientesv;

            var template = new XLTemplate(path);

            template.AddVariable("Direccion", "Avenida Humberto Lobo #555");
            template.AddVariable("Sucursal", "San Pedro Garza García, Nuevo León");
            template.AddVariable("Titulo", "Validación de Estudio");
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

            return (template.ToByteArray(), $"Informe Validacion de Estudio.xlsx");
        }

        public async Task<List<RelaceList>> GetAll(SearchRelase search)
        {
            var requestedStudy = await _repository.GetAll(search);
            if (requestedStudy != null)
            {
                return requestedStudy.ToRelaseListDto();
            }
            else
            {
                return null;
            }
        }

        public async Task<int> UpdateStatus(List<RequestedStudyUpdateDto> requestDto)
        {
            int studyCount = 0;
            var UsuarioId = "";
            var Usuario = "test";
            List<DeliverResultStudieDto> deliveryResultStudie = new List<DeliverResultStudieDto>();

            foreach (var item in requestDto)
            {
                
                var request = await GetExistingRequest(item.SolicitudId);

                var studiesIds = item.EstudioId;
                var studies = await _repository.GetStudyById(item.SolicitudId, studiesIds);

                studies = studies.Where(x => x.EstatusId == Status.RequestStudy.Validado || x.EstatusId == Status.RequestStudy.Liberado).ToList();

                if (studies == null || studies.Count == 0)
                {
                    throw new CustomException(HttpStatusCode.BadRequest, RecordResponses.Request.NoStudySelected);
                }
                 List<SolicitudEstudioDto> StudiestoSend = new List<SolicitudEstudioDto> ();
                foreach (var study in studies)
                {
                    if (study.EstatusId == Status.RequestStudy.Liberado)
                    {   
                        study.EstatusId = Status.RequestStudy.Validado;
                        study.FechaModifico = DateTime.Now;
                    }
                    else
                    {
                        study.EstatusId = Status.RequestStudy.Liberado;
                        study.FechaModifico = DateTime.Now;

                    }
                }
                await _repository.BulkUpdateStudies(item.SolicitudId, studies);

                request = await GetExistingRequest(item.SolicitudId);
                foreach (var estudio in request.Estudios) {


                    if (estudio.EstatusId == Status.RequestStudy.Liberado) {
                        StudiestoSend.Add(new SolicitudEstudioDto { EstudioId = estudio.Id, Tipo = estudio.AreaId ?? 0 });
                    }
                }
                studyCount += studies.Count;

                
                if (request.Parcialidad)
                {

                    var deliver = new DeliverResultStudieDto
                    {
                        SolicitudId = request.Id,
                        EstudiosId = StudiestoSend
                    };
                    deliveryResultStudie.Add(deliver);


                }

                if (request.Estudios.Count() == StudiestoSend.Count()) {

                    var deliver = new DeliverResultStudieDto
                    {
                        SolicitudId = request.Id,
                        EstudiosId = StudiestoSend
                    };
                    deliveryResultStudie.Add(deliver);
                }
                UsuarioId = request.UsuarioCreoId.ToString();
                Usuario = request.UsuarioCreoId.ToString();



            }
            if (deliveryResultStudie.Count() > 0) { 
            List<string>  mediosenvio = new List<string>();
                mediosenvio.Add("Whatsapp");
                mediosenvio.Add("Correo");

                var dataToSend = new DeliverResultsStudiesDto
                {
                    Estudios = deliveryResultStudie,
                    UsuarioId = Guid.Parse(UsuarioId),
                    Usuario = Usuario,
                    MediosEnvio=mediosenvio
                };
                  
                await _clinicresultapplication.SendResultFile(dataToSend);
            }
            return studyCount;
        }

        private async Task<Request> GetExistingRequest(Guid requestId)
        {
            var request = await _repository.FindAsync(requestId);

            if (request == null)
            {
                throw new CustomException(HttpStatusCode.NotFound, SharedResponses.NotFound);
            }

            return request;
        }
        public async Task<byte[]> SendResultFile(DeliverResultsStudiesDto estudios)
        {
            var estudiosSeleccionados = estudios.Estudios[0];
           
            var existingRequest = await _clinicresultrepository.GetRequestById(estudiosSeleccionados.SolicitudId);
            var estudioId = estudiosSeleccionados.EstudiosId[0];
            var existingStudy = existingRequest.Estudios.ToList().Find(x => x.EstudioId == estudiosSeleccionados.EstudiosId[0].EstudioId);
            var area = existingStudy.DepartamentoId;
            if (area == Catalogs.Department.PATOLOGIA)
            {
                var finalResult = await _clinicresultrepository.GetResultPathologicalById(existingStudy.Id);

                List<ClinicalResultsPathological> resultsTaskUnique = new List<ClinicalResultsPathological>();

                resultsTaskUnique.Add(finalResult);

                var existingResultPathologyPdf = resultsTaskUnique.toInformationPdfResult(true);


                return await _pdfClient.GeneratePathologicalResults(existingResultPathologyPdf);



            }
            else
            {
                var finalResult = await _clinicresultrepository.GetLabResultsById(existingStudy.Id);

                return await _pdfClient.GenerateLabResults(finalResult.ToResults(true, true, true));

            }





        }
    }
}
