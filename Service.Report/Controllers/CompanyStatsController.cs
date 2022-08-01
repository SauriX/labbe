using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Service.Report.Dtos;
using Service.Report.Dtos.CompanyStats;
using Shared.Dictionary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Service.Report.Controllers.Report
{
    public partial class ReportController : ControllerBase
    {
        [HttpPost("empresa/filter")]
        [Authorize(Policies.Access)]
        public async Task<IEnumerable<CompanyStatsDto>> GetCompanyNow(ReportFilterDto search)
        {
            return await _companystatsService.GetByFilter(search);
        }

        [HttpPost("empresa/table/filter")]
        [Authorize(Policies.Access)]
        public async Task<CompanyDto> GetFilterTable(ReportFilterDto search)
        {
            return await _companystatsService.GetTableByFilter(search);
        }

        [HttpPost("empresa/chart/filter")]
        [Authorize(Policies.Access)]
        public async Task<IEnumerable<CompanyStatsChartDto>> GetCompanyFilterChart(ReportFilterDto search)
        {
            return await _companystatsService.GetChartByFilter(search);
        }

        [HttpPost("empresa/download/pdf")]
        [Authorize(Policies.Download)]
        public async Task<IActionResult> CompanyPDF(ReportFilterDto search)
        {
            var file = await _companystatsService.DownloadReportPdf(search);
            return File(file, MimeType.PDF, "Sol_Compañia.pdf");
        }
    }
}