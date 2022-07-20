using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Service.Report.Dtos;
using Service.Report.Dtos.ContactStats;
using Shared.Dictionary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Service.Report.Controllers.Report
{
    public partial class ReportController : ControllerBase
    {
        [HttpGet("contact/getAll")]
        public async Task<IEnumerable<ContactStatsDto>> GetByContact()
        {
            return await _contactstatsService.GetByContact();
        }

        [HttpPost("contact/filter")]
        public async Task<IEnumerable<ContactStatsDto>> GetContactNow(ReportFiltroDto search)
        {
            return await _contactstatsService.GetFilter(search);
        }

        [HttpGet("contact/chart/getAll")]
        public async Task<IEnumerable<ContactStatsChartDto>> GetForChart()
        {
            return await _contactstatsService.GetForChart();
        }

        [HttpPost("contact/chart/filter")]
        public async Task<IEnumerable<ContactStatsChartDto>> GetFilterChart(ReportFiltroDto search)
        {
            return await _contactstatsService.GetFilterChart(search);
        }

        [HttpPost("contact/download/pdf")]
        [AllowAnonymous]
        public async Task<IActionResult> ContactPDF(ReportFiltroDto search)
        {
            var file = await _contactstatsService.GenerateReportPDF(search);
            return File(file, MimeType.PDF, "Solic_Contacto.pdf");
        }
    }
}
