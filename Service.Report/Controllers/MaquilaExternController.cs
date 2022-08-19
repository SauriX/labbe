using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Service.Report.Dtos;
using Service.Report.Dtos.BondedRequest;
using Shared.Dictionary;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Service.Report.Controllers.Report
{
    public partial class ReportController : ControllerBase
    {
        [HttpPost("maquila_externa/filter")]
        [Authorize(Policies.Access)]
        public async Task<IEnumerable<MaquilaRequestDto>> GetExternalNow(ReportFilterDto search)
        {
            return await _maquilaexternService.GetByStudies(search);
        }

        [HttpPost("maquila_externa/chart/filter")]
        [Authorize(Policies.Access)]
        public async Task<IEnumerable<MaquilaRequestChartDto>> GetExternalFilterChart(ReportFilterDto search)
        {
            return await _maquilaexternService.GetChartByFilter(search);
        }

        [HttpPost("maquila_externa/download/pdf")]
        [Authorize(Policies.Download)]
        public async Task<IActionResult> ExternalPDF(ReportFilterDto search)
        {
            var file = await _maquilaexternService.DownloadReportPdf(search);
            return File(file, MimeType.PDF, "Maquila_Externa.pdf");
        }
    }
}