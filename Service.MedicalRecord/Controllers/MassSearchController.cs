using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Service.MedicalRecord.Application.IApplication;
using Service.MedicalRecord.Dtos.MassSearch;
using Shared.Dictionary;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Service.MedicalRecord.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MassSearchController : ControllerBase
    {
        private readonly IMassSearchApplication _service;
        private readonly IRequestApplication _requestService;
        public MassSearchController(IMassSearchApplication service,IRequestApplication requestApplication)
        {
            _service = service;
            _requestService = requestApplication;
        }

        [HttpPost("GetByFilter")]
        [Authorize(Policies.Access)]
        public async Task<MassSearchInfoDto> GetByFilter(MassSearchFilterDto filter)
        {
            filter.SucursalesId = (List<Guid>)HttpContext.Items["sucursales"];
            return await _service.GetByFilter(filter);
        }

        [HttpPost("GetAllCaptureResults")]
        [Authorize(Policies.Access)]
        public async Task<List<RequestsInfoDto>> GetAllCaptureResults(DeliverResultsFilterDto search)
        {
            search.SucursalesId = (List<Guid>)HttpContext.Items["sucursales"];
            var clinicResults = await _service.GetAllCaptureResults(search);
            return clinicResults;
        }
        [HttpPost("list")]
        [Authorize(Policies.Download)]
        public async Task<IActionResult> ExportResultsExcel(DeliverResultsFilterDto search)
        {
            search.SucursalesId = (List<Guid>)HttpContext.Items["sucursales"];
            var (file, fileName) = await _service.ExportList(search);
            return File(file, MimeType.XLSX, fileName);
        }

        [HttpPost("download/pdf")]
        [Authorize(Policies.Download)]
        public async Task<IActionResult> ExportResultsPdf(MassSearchFilterDto filter)
        {
            filter.SucursalesId = (List<Guid>)HttpContext.Items["sucursales"];
            var file = await _service.DownloadResultsPdf(filter);
            return File(file, MimeType.PDF, "Resultados Busq. Masiva.pdf");
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

    }
}
