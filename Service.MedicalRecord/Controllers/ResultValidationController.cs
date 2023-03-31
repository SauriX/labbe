using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Service.MedicalRecord.Application.IApplication;
using Service.MedicalRecord.Dtos.General;
using Service.MedicalRecord.Dtos.MassSearch;
using Service.MedicalRecord.Dtos.RequestedStudy;
using Service.MedicalRecord.Dtos.ResultValidation;
using Service.MedicalRecord.Dtos.Sampling;
using Shared.Dictionary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Service.MedicalRecord.Controllers
{
    public partial class RelaseResultController : ControllerBase
    {
        [HttpPost("validation/getList")]
        [Authorize(Policies.Access)]
        public async Task<List<ValidationListDto>> GetAllValidation(GeneralFilterDto search)
        {
            var requestedStudy = await _validationService.GetAll(search);
            return requestedStudy;
        }
        [HttpPut("validation")]
        [Authorize(Policies.Update)]
        public async Task UpdateValidationStatus(List<RequestedStudyUpdateDto> requestDto)
        {
            requestDto.First().Usuario = HttpContext.Items["userName"].ToString();
            await _validationService.UpdateStatus(requestDto);
        }

        [HttpPost("validation/order/{recordId}/{requestId}")]
        //[Authorize(Policies.Print)]
        [Authorize(Policies.Access)]
        public async Task<IActionResult> PrintOrderValidation(Guid recordId, Guid requestId)
        {
            var userName = HttpContext.Items["userName"].ToString();

            var file = await _requestService.PrintOrder(recordId, requestId, userName);

            return File(file, MimeType.PDF, "Orden.pdf");
        }

        [HttpPost("validation/export/list")]
        //  [Authorize(Policies.Download)]
        public async Task<IActionResult> ExportStudyValidationExcel(GeneralFilterDto search)
        {
            var (file, fileName) = await _validationService.ExportList(search);
            return File(file, MimeType.XLSX, fileName);
        }

        [HttpPost("validation/view/list")]
        //[Authorize(Policies.Download)]
        public async Task<IActionResult> SendResultFileValidation(DeliverResultsStudiesDto estudios)
        {
            var file = await _validationService.SendResultFile(estudios);
            return File(file, MimeType.PDF, "Orden.pdf");

        }
    }
}
