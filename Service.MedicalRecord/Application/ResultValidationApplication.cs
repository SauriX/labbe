using ClosedXML.Report;
using Service.MedicalRecord.Application.IApplication;
using Service.MedicalRecord.Dictionary;
using Service.MedicalRecord.Dtos.RequestedStudy;
using Service.MedicalRecord.Dtos.Sampling;
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
using Service.MedicalRecord.Domain.Request;
using MoreLinq;
using Service.MedicalRecord.Dtos.ResultValidation;

namespace Service.MedicalRecord.Application
{
    public class ResultValidationApplication:IValidationApplication
    {
        public readonly IResultaValidationRepository _repository;

        public ResultValidationApplication(IResultaValidationRepository repository)
        {

            _repository = repository;
        }

        public async Task<(byte[] file, string fileName)> ExportList(SearchValidation search)
        {
            var studies = await GetAll(search);

            foreach (var request in studies)
            {
                if (studies.Count > 0)
                {
                    request.Estudios.Insert(0, new ValidationStudyDto{ Study= "Clave", Entrega = DateTime.Now, Estatus = 1 });
                }
            }

            var path = Assets.InformeExpedientes;

            var template = new XLTemplate(path);

            template.AddVariable("Direccion", "Avenida Humberto Lobo #555");
            template.AddVariable("Sucursal", "San Pedro Garza García, Nuevo León");
            template.AddVariable("Titulo", "Toma de Muestra de Estudio");
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

            return (template.ToByteArray(), $"Informe Toma de Muestra de Estudio.xlsx");
        }

        public async Task<List<ValidationListDto>> GetAll(SearchValidation search)
        {
            var requestedStudy = await _repository.GetAll(search);
            if (requestedStudy != null)
            {
                return requestedStudy.ToValidationListDto();
            }
            else
            {
                return null;
            }
        }

        public async Task<int> UpdateStatus(List<RequestedStudyUpdateDto> requestDto)
        {
            int studyCount = 0;
            foreach (var item in requestDto)
            {
                var request = await GetExistingRequest(item.SolicitudId);

                var studiesIds = item.EstudioId;
                var studies = await _repository.GetStudyById(item.SolicitudId, studiesIds);

                studies = studies.Where(x => x.EstatusId == Status.RequestStudy.Capturado).ToList();

                if (studies == null || studies.Count == 0)
                {
                    throw new CustomException(HttpStatusCode.BadRequest, RecordResponses.Request.NoStudySelected);
                }

                foreach (var study in studies)
                {
                    if (study.EstatusId == Status.RequestStudy.Capturado)
                    {
                        study.EstatusId = Status.RequestStudy.Validado;
                    }
                    else
                    {
                        study.EstatusId = Status.RequestStudy.Capturado;
                    }
                }
                studyCount += studies.Count;

                await _repository.BulkUpdateStudies(item.SolicitudId, studies);

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
    }
}
