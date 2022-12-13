using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Service.MedicalRecord.Application.IApplication;
using Service.MedicalRecord.Dtos.Quotation;
using Service.MedicalRecord.Dtos.Reports;
using Service.MedicalRecord.Dtos.Reports.BudgetStats;
using Service.MedicalRecord.Dtos.Reports.StudyStats;
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
    }
}
