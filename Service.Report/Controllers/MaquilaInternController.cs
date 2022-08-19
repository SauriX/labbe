using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Service.Report.Dtos;
using Service.Report.Dtos.BondedRequest;
using Shared.Dictionary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Service.Report.Controllers.Report
{
    public partial class ReportController : ControllerBase
    {
        [HttpPost("maquila_interna/filter")]
        [Authorize(Policies.Access)]
        public async Task<IEnumerable<MaquilaRequestDto>> GetInternalNow(ReportFilterDto search)
        {
            return await _maquilainternService.GetByStudies(search);
        }

        [HttpPost("maquila_interna/chart/filter")]
        [Authorize(Policies.Access)]
        public async Task<IEnumerable<MaquilaRequestChartDto>> GetInternalFilterChart(ReportFilterDto search)
        {
            return await _maquilainternService.GetChartByFilter(search);
        }

        [HttpPost("maquila_interna/download/pdf")]
        [Authorize(Policies.Download)]
        public async Task<IActionResult> InternalPDF(ReportFilterDto search)
        {
            var file = await _maquilainternService.DownloadReportPdf(search);
            return File(file, MimeType.PDF, "Maquila_Interna.pdf");
        }
    }
}