using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Service.Report.Dtos;
using Service.Report.Dtos.Indicators;
using Shared.Dictionary;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Service.Report.Controllers.Report
{
    public partial class ReportController : ControllerBase
    {
        [HttpPost("indicadores/filter")]
        [Authorize(Policies.Access)]
        public async Task<List<Dictionary<string, object>>> GetIndicatorstNow(ReportFilterDto search)
        {
            return await _indicatorsStatsService.GetByFilter(search);
        }
        
        [HttpPost("indicadores/servicios/filter")]
        [Authorize(Policies.Access)]
        public async Task<List<ServicesCostDto>> GetServicesNow(ReportFilterDto search)
        {
            return await _indicatorsStatsService.GetServicesCosts(search);
        }

        [HttpPost("indicadores")]
        [Authorize(Policies.Create)]
        public async Task Create(IndicatorsStatsDto indicator)
        {
            await _indicatorsStatsService.Create(indicator);
        }

        [HttpPut("indicadores")]
        [Authorize(Policies.Update)]
        public async Task Update(IndicatorsStatsDto indicator)
        {
            await _indicatorsStatsService.Update(indicator);
        }

        [HttpPost("indicadores/getForm")]
        [Authorize(Policies.Access)]
        public async Task GetForm(IndicatorsStatsDto indicator)
        {
            await _indicatorsStatsService.GetIndicatorForm(indicator);
        }

        [HttpPost("indicadores/export/list")]
        [Authorize(Policies.Download)]
        public async Task<IActionResult> ExportIndicatorsExcel(ReportFilterDto search)
        {
            var (file, fileName) = await _indicatorsStatsService.ExportList(search);
            return File(file, MimeType.XLSX, fileName);
        }
        
        [HttpPost("indicadores/export/samplingList")]
        [Authorize(Policies.Download)]
        public async Task<IActionResult> ExportSamplingsCostExcel(ReportFilterDto search)
        {
            var (file, fileName) = await _indicatorsStatsService.ExportSamplingsCost(search);
            return File(file, MimeType.XLSX, fileName);
        }

        [HttpPost("indicadores/export/serviceList")]
        [Authorize(Policies.Download)]
        public async Task<IActionResult> ExportServicesCostExcel(ReportFilterDto search)
        {
            var (file, fileName) = await _indicatorsStatsService.ExportServicesCost(search);
            return File(file, MimeType.XLSX, fileName);
        }
    }
}
