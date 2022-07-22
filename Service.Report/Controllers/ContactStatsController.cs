using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Service.Report.Dtos;
using Service.Report.Dtos.ContactStats;
using Shared.Dictionary;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Service.Report.Controllers.Report
{
    public partial class ReportController : ControllerBase
    {
        [HttpPost("contacto/filter")]
        [Authorize(Policies.Access)]
        public async Task<IEnumerable<ContactStatsDto>> GetContactNow(ReportFilterDto search)
        {
            return await _contactstatsService.GetByFilter(search);
        }

        [HttpPost("contacto/chart/filter")]
        [Authorize(Policies.Access)]
        public async Task<IEnumerable<ContactStatsChartDto>> GetFilterChart(ReportFilterDto search)
        {
            return await _contactstatsService.GetCharByFilter(search);
        }

        [HttpPost("contacto/download/pdf")]
        [Authorize(Policies.Download)]
        public async Task<IActionResult> ContactPDF(ReportFilterDto search)
        {
            var file = await _contactstatsService.DownloadReportPdf(search);
            return File(file, MimeType.PDF, "Solic_Contacto.pdf");
        }
    }
}
