    using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Service.MedicalRecord.Application.IApplication;
using Service.MedicalRecord.Dtos.General;
using Service.MedicalRecord.Dtos.RequestedStudy;
using Service.MedicalRecord.Dtos.Sampling;
using Shared.Dictionary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ZXing;

namespace Service.MedicalRecord.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SamplingController : ControllerBase
    {
        private readonly ISamplingApplication _service;
        private readonly IRequestApplication _requestService;
        public SamplingController(ISamplingApplication service, IRequestApplication requestService)
        {
            _service = service;
            _requestService = requestService;
        }
        [HttpPost("getList")]
        [Authorize(Policies.Access)]
        public async Task<List<SamplingListDto>> GetAll(GeneralFilterDto search)
        {
            search.SucursalesId = (List<Guid>)HttpContext.Items["sucursales"];
            var requestedStudy = await _service.GetAll(search);
            return requestedStudy;
        }
        [HttpPut]
        [Authorize(Policies.Update)]
        public async Task UpdateStatus(List<RequestedStudyUpdateDto> requestDto)
        {
            requestDto.First().Usuario = HttpContext.Items["userName"].ToString();
            await _service.UpdateStatus(requestDto);
        }

        [HttpPost("order/{recordId}/{requestId}")]
        //[Authorize(Policies.Print)]
        [Authorize(Policies.Access)]
        public async Task<IActionResult> PrintOrder(Guid recordId, Guid requestId)
        {
            var userName = HttpContext.Items["userName"].ToString();

            var file = await _requestService.PrintOrder(recordId, requestId, userName);

            return File(file, MimeType.PDF, "Orden.pdf");
        }

        [HttpPost("export/getList")]
        [Authorize(Policies.Download)]
        public async Task<IActionResult> ExportStudyExcel(GeneralFilterDto search)
        {
            search.SucursalesId = (List<Guid>)HttpContext.Items["sucursales"];
            var (file, fileName) = await _service.ExportList(search);
            return File(file, MimeType.XLSX, fileName);
        }
    }
}
