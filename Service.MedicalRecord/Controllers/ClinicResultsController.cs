using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Service.MedicalRecord.Application.IApplication;
using Service.MedicalRecord.Domain;
using Service.MedicalRecord.Dtos;
using Service.MedicalRecord.Dtos.ClinicResults;
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
        public async Task<List<ClinicResultsDto>> GetAll(RequestedStudySearchDto search)
        {
            var clinicResults = await _service.GetAll(search);
            return clinicResults;
        }

        [HttpPost("export/list")]
        [Authorize(Policies.Download)]
        public async Task<IActionResult> ExportClinicsExcel(RequestedStudySearchDto search)
        {
            var (file, fileName) = await _service.ExportList(search);
            return File(file, MimeType.XLSX, fileName);
        }

        [HttpPost("savePathological")]
        [Authorize(Policies.Create)]
        public async Task SaveResultPathologicalStudy(ClinicalResultPathologicalFormDto result)
        {
            await _service.SaveResultPathologicalStudy(result);
        }

        [HttpPut("updatePathological")]
        [Authorize(Policies.Update)]
        public async Task UpdateResultPathologicalStudy(ClinicalResultPathologicalFormDto result)
        {
            await _service.UpdateResultPathologicalStudy(result);
        }
        [HttpPost("getPathological")]
        [Authorize(Policies.Access)]
        public async Task<ClinicalResultsPathological> GetResultPathological(int RequestStudyId)
        {
            var clinicResults = await _service.GetResultPathological(RequestStudyId);
            return clinicResults;
        }
    }
}
