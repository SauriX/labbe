using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Service.MedicalRecord.Application.IApplication;
using Service.MedicalRecord.Dtos.Quotation;
using Service.MedicalRecord.Dtos.Reports;
using Service.MedicalRecord.Dtos.Reports.BudgetStats;
using Shared.Dictionary;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Service.MedicalRecord.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReportDataController : ControllerBase
    {
        private readonly IReportApplication _service;
        public ReportDataController(IReportApplication service) 
        {
            _service = service;
        }

        // Cotizaciones
        [HttpPost("presupuestos/filter")]
        [Authorize(Policies.Access)]
        public async Task<IEnumerable<BudgetStatsDto>> GetBudgetRequestNow(ReportFilterDto search)
        {
            return await _service.GetQuotationByFilter(search);
        }

        [HttpPost("presupuestos/table/filter")]
        [Authorize(Policies.Access)]
        public async Task<BudgetDto> GetFilterBudgetRequestTable(ReportFilterDto search)
        {
            return await _service.GetQuotationTableByFilter(search);
        }

        [HttpPost("presupuestos/chart/filter")]
        [Authorize(Policies.Access)]
        public async Task<IEnumerable<BudgetStatsChartDto>> GetBudgetRequestFilterChart(ReportFilterDto search)
        {
            return await _service.GetQuotationChartByFilter(search);
        }
    }
}
