using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
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
        
        [HttpPost("indicadores/toma/filter")]
        [Authorize(Policies.Access)]
        public async Task<List<SamplesCostsDto>> GetSamplesCostNow(ReportModalFilterDto search)
        {
            return await _indicatorsStatsService.GetBySamplesCosts(search);
        }
        
        [HttpPost("indicadores/servicios/filter")]
        [Authorize(Policies.Access)]
        public async Task<InvoiceServicesDto> GetServicesNow(ReportModalFilterDto search)
        {
            return await _indicatorsStatsService.GetServicesCosts(search);
        }

        [HttpPut("indicadores/saveFile")]
        [Authorize(Policies.Update)]
        public async Task SaveImage([FromForm] IFormFile archivo)
        {
            var userId = (Guid)HttpContext.Items["userId"];

            await _indicatorsStatsService.SaveServiceFile(archivo, userId);
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
        
        [HttpPut("indicadores/toma")]
        [Authorize(Policies.Update)]
        public async Task UpdateSamples(SamplesCostsDto sample)
        {
            await _indicatorsStatsService.UpdateSample(sample);
        }
        
        [HttpPut("indicadores/fijo")]
        [Authorize(Policies.Update)]
        public async Task UpdateService(UpdateServiceDto service)
        {
            await _indicatorsStatsService.UpdateService(service);
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
        public async Task<IActionResult> ExportSamplingsCostExcel(ReportModalFilterDto search)
        {
            var (file, fileName) = await _indicatorsStatsService.ExportSamplingsCost(search);
            return File(file, MimeType.XLSX, fileName);
        }

        [HttpPost("indicadores/export/serviceList")]
        [Authorize(Policies.Download)]
        public async Task<IActionResult> ExportServicesCostExcel(ReportModalFilterDto search)
        {
            var (file, fileName) = await _indicatorsStatsService.ExportServicesCost(search);
            return File(file, MimeType.XLSX, fileName);
        }
        
        [HttpPost("indicadores/export/serviceListExample")]
        [Authorize(Policies.Download)]
        public async Task<IActionResult> ExportServicesCostExampleExcel()
        {
            var (file, fileName) = await _indicatorsStatsService.ExportServicesCostSample();
            return File(file, MimeType.XLSX, fileName);
        }
    }
}
