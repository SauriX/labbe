using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Service.Report.Dtos;
using Service.Report.Dtos.Request;
using Shared.Dictionary;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Service.Report.Controllers.Report
{
    public partial class ReportController : ControllerBase
    {
        [HttpPost("expediente/filter")]
        [Authorize(Policies.Access)]
        public async Task<IEnumerable<RequestStatsDto>> GetBranchNow(ReportFilterDto search)
        {
            return await _requestService.GetByFilter(search);
        }

        [HttpPost("expediente/download/pdf")]
        [Authorize(Policies.Download)]
        public async Task<IActionResult> ExpedientePDF(ReportFilterDto search)
        {
            var file = await _requestService.DownloadReportPdf(search);

            return File(file, MimeType.PDF, "EstadísticaExpediente.pdf");
        }
    }
}