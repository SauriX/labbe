
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Service.MedicalRecord.Application.IApplication;
using Service.MedicalRecord.Dtos.Request;
using Service.MedicalRecord.Dtos.RportStudy;
using Shared.Dictionary;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Service.MedicalRecord.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReportStudyController : ControllerBase
    {
        private readonly IReportStudyApplication _service;



        public ReportStudyController(IReportStudyApplication service)
        {
            _service = service;
        }

        [HttpPost("filter")]
        [Authorize(Policies.Access)]
        public async Task<List<ReportRequestListDto>> GetByFilter(RequestFilterDto filter)
        {
            filter.SucursalesId = (List<Guid>)HttpContext.Items["sucursales"];
            return await _service.GetByFilter(filter);
        }

        [HttpPost("report")]
        [Authorize(Policies.Access)]
        public async Task<IActionResult> ExportPdf(RequestFilterDto filter)
        {
            var file = await _service.ExportRequest(filter);

            return File(file, MimeType.PDF, "Pendientes a recibir.pdf");
        }


        [HttpPost("export/getList")]
        [Authorize(Policies.Download)]
        public async Task<IActionResult> ExportStudyExcel(RequestFilterDto search)
        {
            var (file, fileName) = await _service.ExportList(search);
            return File(file, MimeType.XLSX, fileName);
        }
    }
}
