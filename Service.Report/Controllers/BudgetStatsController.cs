using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Service.Report.Dtos.TypeRequest;
using Service.Report.Dtos;
using Shared.Dictionary;
using System.Collections.Generic;
using System.Threading.Tasks;
using Service.Report.Dtos.BudgetStats;

namespace Service.Report.Controllers.Report
{
    public partial class ReportController : ControllerBase 
    {
        [HttpPost("presupuestos/filter")]
        [Authorize(Policies.Access)]
        public async Task<IEnumerable<BudgetStatsDto>> GetBudgetRequestNow(ReportFilterDto search)
        {
            return await _budgetStatsService.GetByFilter(search);
        }

        [HttpPost("presupuestos/table/filter")]
        [Authorize(Policies.Access)]
        public async Task<BudgetDto> GetFilterBudgetRequestTable(ReportFilterDto search)
        {
            return await _budgetStatsService.GetTableByFilter(search);
        }

        [HttpPost("presupuestos/chart/filter")]
        [Authorize(Policies.Access)]
        public async Task<IEnumerable<BudgetStatsChartDto>> GetBudgetRequestFilterChart(ReportFilterDto search)
        {
            return await _budgetStatsService.GetChartByFilter(search);
        }

        [HttpPost("presupuestos/download/pdf")]
        [Authorize(Policies.Download)]
        public async Task<IActionResult> BudgetRequestPDF(ReportFilterDto search)
        {
            var file = await _budgetStatsService.DownloadReportPdf(search);
            return File(file, MimeType.PDF, "Presupuesto_Sucursales.pdf");
        }
    }
}
