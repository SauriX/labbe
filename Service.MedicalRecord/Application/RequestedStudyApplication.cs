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

namespace Service.MedicalRecord.Application
{
    public class RequestedStudyApplication : IRequestedStudyApplication
    {
        public readonly IRequestedStudyRepository _repository;
        public RequestedStudyApplication(IRequestedStudyRepository repository)
        {
            _repository = repository;
        }

        public async Task<(byte[] file, string fileName)> ExportList(RequestedStudySearchDto search = null)
        {
            var studies = await GetAll(search);

            var path = Assets.ExpedientetList;

            var template = new XLTemplate(path);

            template.AddVariable("Direccion", "Avenida Humberto Lobo #555");
            template.AddVariable("Sucursal", "San Pedro Garza García, Nuevo León");
            template.AddVariable("Titulo", "Expedientes");
            template.AddVariable("Fecha", DateTime.Now.ToString("dd/MM/yyyy"));
            template.AddVariable("Expedientes", studies.Distinct());

            template.Generate();

            var range = template.Workbook.Worksheet("Expedientes").Range("Expedientes");
            var table = template.Workbook.Worksheet("Expedientes").Range("$A$3:" + range.RangeAddress.LastAddress).CreateTable();
            table.Theme = XLTableTheme.TableStyleMedium2;

            template.Format();

            return (template.ToByteArray(), $"Informe Solicitud de Estudio.xlsx");
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

        public async Task<int> UpdateStatus(RequestStudyUpdateDto requestDto)
        {
            var request = await GetExistingRequest(requestDto.ExpedienteId, requestDto.SolicitudId);

            var studiesIds = requestDto.Estudios.Select(x => x.EstudioId);
            var studies = await _repository.GetStudyById(requestDto.SolicitudId, studiesIds);

            studies = studies.Where(x => x.EstatusId == Status.RequestStudy.TomaDeMuestra).Where(x => x.EstatusId == Status.RequestStudy.Solicitado).ToList();

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

            await _repository.BulkUpdateStudies(requestDto.SolicitudId, studies);

            return studies.Count;
        }

        private async Task<Request> GetExistingRequest(Guid recordId, Guid requestId)
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
