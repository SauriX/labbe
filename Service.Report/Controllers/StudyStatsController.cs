using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Service.Report.Dtos;
using Service.Report.Dtos.StudyStats;
using Shared.Dictionary;
using System.Collections.Generic;
using System.Threading.Tasks;


namespace Service.Report.Controllers.Report
{
    public partial class ReportController : ControllerBase
    {
        [HttpPost("estudios/filter")]
        [Authorize(Policies.Access)]
        public async Task<IEnumerable<StudyStatsDto>> GetStudyNow(ReportFilterDto search)
        {
            return await _studystatsService.GetByFilter(search);
        }

        [HttpPost("estudios/chart/filter")]
        [Authorize(Policies.Access)]
        public async Task<IEnumerable<StudyStatsChartDto>> GetStudyFilterChart(ReportFilterDto search)
        {
            return await _studystatsService.GetChartByFilter(search);
        }

        [HttpPost("estudios/download/pdf")]
        [Authorize(Policies.Download)]
        public async Task<IActionResult> StudyPDF(ReportFilterDto search)
        {
            var file = await _studystatsService.DownloadReportPdf(search);
            return File(file, MimeType.PDF, "Rel_Estudios.pdf");
        }
    }
}
