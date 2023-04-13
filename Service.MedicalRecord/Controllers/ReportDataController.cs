using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Service.MedicalRecord.Application.IApplication;
using Service.MedicalRecord.Dtos.Quotation;
using Service.MedicalRecord.Dtos.Reports;
using Service.MedicalRecord.Dtos.Reports.BudgetStats;
using Service.MedicalRecord.Dtos.Reports.StudyStats;
using Service.MedicalRecord.Dtos.Request;
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
        private readonly ILogger<ReportDataController> _logger;
        public ReportDataController(IReportApplication service, ILogger<ReportDataController> logger) 
        {
            _service = service;
            _logger = logger
        }

        [HttpPost("cotizaciones/filter")]
        [Authorize(Policies.Access)]
        public async Task<IEnumerable<BudgetStatsDto>> GetBudgetRequestNow(ReportFilterDto search)
        {
            return await _service.GetQuotationByFilter(search);
        }
        
        [HttpPost("solicitudes/filter")]
        [Authorize(Policies.Access)]
        public async Task<IEnumerable<ReportInfoDto>> GetRequestNow(ReportFilterDto search)
        {
            return await _service.GetRequestByFilter(search);
        }
        
        [HttpPost("estudios/filter")]
        [Authorize(Policies.Access)]
        public async Task<IEnumerable<StudiesDto>> GetRequestStudiesNow(ReportFilterDto search)
        {
            return await _service.GetStudiesByFilter(search);
        }

        [HttpPost("payment/filter")]
        [Authorize(Policies.Access)]
        public async Task<IEnumerable<RequestPaymentStatsDto>> GetPayments(ReportFilterDto search)
        {
            _logger.LogError("IN CONTROLLER REPORTDATA payment/filter");
            var user = HttpContext.Items["userName"].ToString();

            return await _service.GetPaymentsByFilter(search, user);
        }
    }
}
