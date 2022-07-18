using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Service.Report.Application.IApplication;
using Service.Report.Dtos.PatientStats;
using Service.Report.PdfModel;
using Shared.Dictionary;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Service.Report.Controllers.Report
{
    public partial class ReportController : ControllerBase
    {
        [HttpGet("estadistica/getByName")]
        public async Task<IEnumerable<PatientStatsFiltroDto>> GetByName()
        {
            return await _patientstatsService.GetByName();
        }

        [HttpPost("estadistica/filter")]
        public async Task<IEnumerable<PatientStatsFiltroDto>> GetNow(PatientStatsSearchDto search)
        {
            return await _patientstatsService.GetFilter(search);
        }

        [HttpPost("estadistica/export/table/{search?}")]
        [Authorize(Policies.Download)]
        public async Task<IActionResult> ExportTableStats(string search = null)
        {
            var (file, fileName) = await _patientstatsService.ExportTableStats(search);
            return File(file, MimeType.XLSX, fileName);
        }

        [HttpPost("estadistica/export/graphic/{search?}")]
        [Authorize(Policies.Download)]
        public async Task<IActionResult> ExportChartStats(string search = null)
        {
            var (file, fileName) = await _patientstatsService.ExportChartStats(search);
            return File(file, MimeType.XLSX, fileName);
        }

        [HttpPost("estadistica/download/pdf")]
        [AllowAnonymous]
        public async Task<IActionResult> StatsPDF(PatientStatsSearchDto search)
        {
            var file = await _patientstatsService.GenerateReportPDF(search);
            return File(file, MimeType.PDF, "EstadisticaPaciente.pdf");
        }
    }
}
