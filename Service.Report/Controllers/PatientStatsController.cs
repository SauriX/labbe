﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Service.Report.Application.IApplication;
using Service.Report.Dtos.PatientStats;
using Service.Report.PdfModel;
using Shared.Dictionary;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Service.Report.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PatientStatsController : ControllerBase
    {
        private readonly IPatientStatsApplication _patientstatsService;

        public PatientStatsController(IPatientStatsApplication indicationService)
        {
            _patientstatsService = indicationService;
        }

        [HttpGet("getByName")]
        public async Task<IEnumerable<PatientStatsFiltroDto>> GetByName()
        {
            return await _patientstatsService.GetByName();
        }

        [HttpPost("filter")]
        public async Task<IEnumerable<PatientStatsFiltroDto>> GetNow(PatientStatsSearchDto search)
        {
            return await _patientstatsService.GetFilter(search);
        }

        [HttpPost("export/table/{search?}")]
        [Authorize(Policies.Download)]
        public async Task<IActionResult> ExportTableStats(string search = null)
        {
            var (file, fileName) = await _patientstatsService.ExportTableStats(search);
            return File(file, MimeType.XLSX, fileName);
        }

        [HttpPost("export/graphic/{search?}")]
        [Authorize(Policies.Download)]
        public async Task<IActionResult> ExportChartStats(string search = null)
        {
            var (file, fileName) = await _patientstatsService.ExportChartStats(search);
            return File(file, MimeType.XLSX, fileName);
        }

        [HttpPost("download/pdf")]
        [AllowAnonymous]
        public async Task<IActionResult> StatsPDF(PatientStatsSearchDto search)
        {
            var file = await _patientstatsService.GenerateReportPDF(search);
            return File(file, MimeType.PDF, "EstadisticaPaciente.pdf");
        }
    }
}
