using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Service.Report.Dtos;
using Service.Report.Dtos.StudyStats;
using Shared.Dictionary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Service.Report.Controllers.Report
{
    public partial class ReportController : ControllerBase
    {
        [HttpPost("urgentes/filter")]
        [Authorize(Policies.Access)]
        public async Task<IEnumerable<StudyStatsDto>> GetUrgentNow(ReportFilterDto search)
        {
            return await _urgentstatsService.GetByFilter(search);
        }

        [HttpPost("urgentes/chart/filter")]
        [Authorize(Policies.Access)]
        public async Task<IEnumerable<StudyStatsChartDto>> GetUrgentFilterChart(ReportFilterDto search)
        {
            return await _urgentstatsService.GetChartByFilter(search);
        }

        [HttpPost("urgentes/download/pdf")]
        [Authorize(Policies.Download)]
        public async Task<IActionResult> UrgentPDF(ReportFilterDto search)
        {
            var file = await _urgentstatsService.DownloadReportPdf(search);
            return File(file, MimeType.PDF, "Rel_Estudios_Urgentes.pdf");
        }
    }
}
