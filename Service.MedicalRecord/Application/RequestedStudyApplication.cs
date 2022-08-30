using System;
using System.Net;
using System.Linq;
using Shared.Error;
using System.Threading.Tasks;
using System.Collections.Generic;
using Service.MedicalRecord.Mapper;
using Service.MedicalRecord.Dictionary;
using Service.MedicalRecord.Dtos.Request;
using Service.MedicalRecord.Dtos.Sampling;
using Service.MedicalRecord.Domain.Request;
using Service.MedicalRecord.Dtos.RequestedStudy;
using Service.MedicalRecord.Repository.IRepository;
using Service.MedicalRecord.Application.IApplication;
using SharedResponses = Shared.Dictionary.Responses;
using RecordResponses = Service.MedicalRecord.Dictionary.Response;
using ClosedXML.Report;
using ClosedXML.Excel;
using Shared.Extensions;
using MoreLinq;

namespace Service.MedicalRecord.Application
{
    public class RequestedStudyApplication : IRequestedStudyApplication
    {
        public readonly IRequestedStudyRepository _repository;
        public RequestedStudyApplication(IRequestedStudyRepository repository)
        {
            _repository = repository;
        }

        public async Task<(byte[] file, string fileName)> ExportList(RequestedStudySearchDto search)
        {
            var studies = await GetAll(search);

            var path = Assets.InformeExpedientes;

            var template = new XLTemplate(path);

            template.AddVariable("Direccion", "Avenida Humberto Lobo #555");
            template.AddVariable("Sucursal", "San Pedro Garza García, Nuevo León");
            template.AddVariable("Titulo", "Registrar Solicitud de Estudio");
            template.AddVariable("FechaInicio", search.Fecha.First().ToString("dd/MM/yyyy"));
            template.AddVariable("FechaFinal", search.Fecha.Last().ToString("dd/MM/yyyy"));
            template.AddVariable("Expedientes", studies);

            template.Generate();

            //var range = template.Workbook.Worksheet("Expedientes").Range("Expedientes");
            //var table = template.Workbook.Worksheet("Expedientes").Range("$A$3:" + range.RangeAddress.LastAddress).CreateTable();
            //table.Theme = XLTableTheme.TableStyleMedium2;

            template.Workbook.Worksheets.ToList().ForEach(x =>
            {
                var cuenta = 0;
                int[] posiciones = { 0, 0 };
                x.Worksheet.Rows().ForEach(y =>
                {
                    var descripcion = template.Workbook.Worksheets.FirstOrDefault().Cell(y.RowNumber(), "B").Value.ToString();
                    if (descripcion == "Clave" && cuenta == 0)
                    {
                        posiciones[0] = y.RowNumber();
                        cuenta++;
                    }
                    if (descripcion == "" && cuenta == 1)
                    {
                        posiciones[1] = y.RowNumber() - 1;
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

            return (template.ToByteArray(), $"Informe Solicitud de Estudio.xlsx");

            //var template = new XLTemplate(AppDomain.CurrentDomain.BaseDirectory + "Archivos\\Formatos\\Reporte_Auditoria_Finalizada.xlsx");

            //template.AddVariable(auditoria);

            //template.Generate();



            //// Agrupar Hallazgos




            //MemoryStream stream = ObtenerStream(template);

            //return stream.ToArray();
        }

        public async Task<List<SamplingListDto>> GetAll(RequestedStudySearchDto search)
        {
            var requestedStudy = await _repository.GetAll(search);
            if(requestedStudy != null)
            {
                return requestedStudy.ToRequestedStudyDto();
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
                } else
                {
                    study.EstatusId = Status.RequestStudy.TomaDeMuestra;
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
