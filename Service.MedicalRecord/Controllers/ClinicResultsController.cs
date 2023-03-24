using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Service.MedicalRecord.Application.IApplication;
using Service.MedicalRecord.Domain;
using Service.MedicalRecord.Domain.Request;
using Service.MedicalRecord.Dtos;
using Service.MedicalRecord.Dtos.ClinicResults;
using Service.MedicalRecord.Dtos.General;
using Service.MedicalRecord.Dtos.MassSearch;
using Service.MedicalRecord.Dtos.Request;
using Service.MedicalRecord.Dtos.RequestedStudy;
using Shared.Dictionary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Service.MedicalRecord.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClinicResultsController : ControllerBase 
    {
        private readonly IClinicResultsApplication _service;
        public ClinicResultsController(IClinicResultsApplication service)
        {
            _service = service;
        }

        [HttpPost("getList")]
        [Authorize(Policies.Access)]
        public async Task<List<ClinicResultsDto>> GetAll(GeneralFilterDto search)
        {
            search.SucursalesId = (List<Guid>)HttpContext.Items["sucursales"];
            var clinicResults = await _service.GetAll(search);
            return clinicResults;
        }

        [HttpPost("export/list")]
        [Authorize(Policies.Download)]
        public async Task<IActionResult> ExportClinicsExcel(GeneralFilterDto search)
        {
            search.SucursalesId = (List<Guid>)HttpContext.Items["sucursales"];
            var (file, fileName) = await _service.ExportList(search);
            return File(file, MimeType.XLSX, fileName);
        }
        
        [HttpPost("export/glucose")]
        [Authorize(Policies.Download)]
        public async Task<IActionResult> ExportGlucoseChart(ClinicResultsFormDto results)
        {
            var (file, fileName) = await _service.ExportGlucoseChart(results);
            return File(file, MimeType.XLSX, fileName);
        }

        [HttpPost("saveResults")]
        [Authorize(Policies.Create)]
        public async Task SaveLabResults(List<ClinicResultsFormDto> results)
        {
            await _service.SaveLabResults(results);
        }

        [HttpPost("getLabResultsById/{id}")]
        [Authorize(Policies.Access)]
        public async Task<List<ClinicResultsFormDto>> GetLabResultsByid(string id)
        {
            var clinicResults = await _service.GetLabResultsById(id);
            return clinicResults;
        }

        [HttpPut("updateResults/{EnvioManual}")]
        [Authorize(Policies.Update)]
        public async Task UpdateLabResults(List<ClinicResultsFormDto> results, bool EnvioManual)
        {
            results.First().UsuarioId = (Guid)HttpContext.Items["userId"];
            results.First().Usuario = HttpContext.Items["userName"].ToString();
            await _service.UpdateLabResults(results, EnvioManual);
        }

        [HttpGet("studies_params/{recordId}/{requestId}")]
        public async Task<RequestStudyUpdateDto> GetStudies(Guid recordId, Guid requestId)
        {
            return await _service.GetStudies(recordId, requestId);
        }

        [HttpPost("savePathological")]
        [Authorize(Policies.Create)]
        public async Task SaveResultPathologicalStudy([FromForm] ClinicalResultPathologicalFormDto result)
        {
            await _service.SaveResultPathologicalStudy(result);
        }

        [HttpPut("updatePathological/{EnvioManual}")]
        [Authorize(Policies.Update)]
        public async Task UpdateResultPathologicalStudy([FromForm] ClinicalResultPathologicalFormDto result, bool EnvioManual)
        {
            result.UsuarioId = (Guid)HttpContext.Items["userId"];
            result.Usuario = HttpContext.Items["userName"].ToString();
            await _service.UpdateResultPathologicalStudy(result, EnvioManual);
        }
        [HttpPut("sendResultFile")]
        [Authorize(Policies.Update)]
        public async Task<bool> SendResultFile([FromBody] DeliverResultsStudiesDto estudios)
        {
            estudios.UsuarioId = (Guid)HttpContext.Items["userId"];
            estudios.Usuario = HttpContext.Items["userName"].ToString();
            return await _service.SendResultFile(estudios);
        }


        [HttpPost("getPathological")]
        [Authorize(Policies.Access)]
        public async Task<ClinicResultsPathologicalInfoDto> GetResultPathological([FromBody] int RequestStudyId)
        {
            var clinicResults = await _service.GetResultPathological(RequestStudyId);
            return clinicResults;
        }

        [HttpGet("getHisoty/request/{Id}")]
        [Authorize(Policies.Access)]
        public async Task<List<DeliveryHistoryDto>> GetDeliveryHistoryByRequestId(Guid Id)
        {

            return await _service.GetDeliveryHistoryByRequestId(Id);
        }
        [HttpPost("create/record")]
        [Authorize(Policies.Access)]
        public async Task<List<DeliveryHistoryDto>> GetDeliveryHistoryByRequestId(HistoryRecordInfo record)
        {
            record.Usuario = HttpContext.Items["userName"].ToString();

            return await _service.CreateNoteHistoryRecord(record);
        }

        [HttpPost("getRequestStudyById")]
        [Authorize(Policies.Access)]
        public async Task<RequestStudy> GetRequestStudyById([FromBody] int RequestStudyId)
        {
            var requestStudy = await _service.GetRequestStudyById(RequestStudyId);
            return requestStudy;
        }

        [HttpPut("updateStatusStudy")]
        [Authorize(Policies.Update)]
        public async Task UpdateStatusStudy(UpdateStatusDto updateStatus)
        {
            updateStatus.UsuarioId = (Guid)HttpContext.Items["userId"];
            updateStatus.Usuario = HttpContext.Items["userName"].ToString();
            await _service.UpdateStatusStudy(updateStatus.RequestStudyId, updateStatus.status, updateStatus.Usuario);
        }

        [HttpPost("printSelectedStudies")]
        public async Task<IActionResult> PrintSelectedStudies(ConfigurationToPrintStudies configuration)
        {
            var file = await _service.PrintSelectedStudies(configuration);

            return File(file, MimeType.PDF, "Estudios.pdf");
        }
    }
}
