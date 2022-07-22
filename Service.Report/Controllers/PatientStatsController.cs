using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Service.Report.Dtos;
using Service.Report.Dtos.PatientStats;
using Shared.Dictionary;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Service.Report.Controllers.Report
{
    public partial class ReportController : ControllerBase
    {
        [HttpPost("estadistica/filter")]
        [Authorize(Policies.Access)]
        public async Task<IEnumerable<PatientStatsDto>> GetNameNow(ReportFilterDto search)
        {
            return await _patientstatsService.GetByFilter(search);
        }

        [HttpPost("estadistica/download/pdf")]
        [Authorize(Policies.Download)]
        public async Task<IActionResult> PacientePDF(ReportFilterDto search)
        {
            var file = await _patientstatsService.DownloadReportPdf(search);
            return File(file, MimeType.PDF, "EstadisticaPaciente.pdf");
        }
    }
}
