using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Service.MedicalRecord.Application.IApplication;
using Service.MedicalRecord.Dtos.RequestedStudy;
using Service.MedicalRecord.Dtos.ResultValidation;
using Service.MedicalRecord.Dtos.Sampling;
using Shared.Dictionary;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Service.MedicalRecord.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ResultValidationController : ControllerBase
    {
        private readonly IValidationApplication _service;
        private readonly IRequestApplication _requestService;
        public ResultValidationController(IValidationApplication service, IRequestApplication requestService)
        {
            _service = service;
            _requestService = requestService;   
        }
        [HttpPost("getList")]
        [Authorize(Policies.Access)]
        public async Task<List<ValidationListDto>> GetAll(SearchValidation search)
        {
            var requestedStudy = await _service.GetAll(search);
            return requestedStudy;
        }
        [HttpPut]
        [Authorize(Policies.Update)]
        public async Task UpdateStatus(List<RequestedStudyUpdateDto> requestDto)
        {
            await _service.UpdateStatus(requestDto);
        }

        [HttpPost("order/{recordId}/{requestId}")]
        //[Authorize(Policies.Print)]
        [Authorize(Policies.Access)]
        public async Task<IActionResult> PrintOrder(Guid recordId, Guid requestId)
        {
            var file = await _requestService.PrintOrder(recordId, requestId);

            return File(file, MimeType.PDF, "Orden.pdf");
        }

        [HttpPost("export/list")]
        [Authorize(Policies.Download)]
        public async Task<IActionResult> ExportStudyExcel(SearchValidation search)
        {
            var (file, fileName) = await _service.ExportList(search);
            return File(file, MimeType.XLSX, fileName);
        }
    }
}
