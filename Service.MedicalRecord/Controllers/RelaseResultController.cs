
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Service.MedicalRecord.Application.IApplication;
using Service.MedicalRecord.Dtos.MassSearch;
using Service.MedicalRecord.Dtos.RelaseResult;
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
    public partial class RelaseResultController : ControllerBase
    {
        private readonly IRelaseResultApplication _service;
        IValidationApplication _validationService;
        private readonly IRequestApplication _requestService;
        public RelaseResultController(IRelaseResultApplication service, IRequestApplication requestService, IValidationApplication validationService)
        {
            _service = service;
            _validationService = validationService;
            _requestService = requestService;
        }
        [HttpPost("release/getList")]
        [Authorize(Policies.Access)]
        public async Task<List<RelaceList>> GetAll(SearchRelase search)
        {
            search.SucursalesId = (List<Guid>)HttpContext.Items["sucursales"];
            var requestedStudy = await _service.GetAll(search);
            return requestedStudy;
        }
        [HttpPut("release")]
        [Authorize(Policies.Update)]
        public async Task UpdateStatus(List<RequestedStudyUpdateDto> requestDto)
        {
            requestDto.First().Usuario = HttpContext.Items["userName"].ToString();
            await _service.UpdateStatus(requestDto);
        }

        [HttpPost("release/order/{recordId}/{requestId}")]
        //[Authorize(Policies.Print)]
        [Authorize(Policies.Access)]
        public async Task<IActionResult> PrintOrder(Guid recordId, Guid requestId)
        {
            var file = await _requestService.PrintOrder(recordId, requestId,"");

            return File(file, MimeType.PDF, "Orden.pdf");
        }

        [HttpPost("release/export/list")]
        //  [Authorize(Policies.Download)]
        public async Task<IActionResult> ExportStudyExcel(SearchRelase search)
        {
            var (file, fileName) = await _service.ExportList(search);
            return File(file, MimeType.XLSX, fileName);
        }

        [HttpPost("release/view/list")]
        //[Authorize(Policies.Download)]
        public async Task<IActionResult> SendResultFile(DeliverResultsStudiesDto estudios)
        {
            var file = await _service.SendResultFile(estudios);
            return File(file, MimeType.PDF, "Orden.pdf");

        }
    }
}
