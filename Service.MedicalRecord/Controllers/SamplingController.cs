using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Service.MedicalRecord.Application.IApplication;
using Service.MedicalRecord.Dtos.Sampling;
using Shared.Dictionary;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

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
        public async Task<List<SamplingListDto>> GetAll(rRequestedStudySearchDto search)
        {
            var sampling = await _service.GetAll(search);
            return sampling;
        }
        [HttpPut]
        [Authorize(Policies.Update)]
        public async Task UpdateStatus(UpdateDto dates)
        {
            await _service.UpdateStatus(dates);
        }

        [HttpPost("order/{recordId}/{requestId}")]
        //[Authorize(Policies.Print)]
        [Authorize(Policies.Access)]
        public async Task<IActionResult> PrintOrder(Guid recordId, Guid requestId)
        {
            var file = await _requestService.PrintOrder(recordId, requestId);

            return File(file, MimeType.PDF, "order.pdf");
        }
    }
}
