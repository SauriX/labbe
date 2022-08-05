using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Service.Report.Dtos;
using Service.Report.Dtos.TypeRequest;
using Shared.Dictionary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Service.Report.Controllers.Report
{
    public partial class ReportController : ControllerBase
    {
        [HttpPost("cargo/filter")]
        [Authorize(Policies.Access)]
        public async Task<IEnumerable<TypeRequestDto>> GetChargeRequestNow(ReportFilterDto search)
        {
            return await _chargerequestService.GetByFilter(search);
        }

        [HttpPost("cargo/table/filter")]
        [Authorize(Policies.Access)]
        public async Task<TypeDto> GetFilterChargeRequestTable(ReportFilterDto search)
        {
            return await _chargerequestService.GetTableByFilter(search);
        }

        [HttpPost("cargo/chart/filter")]
        [Authorize(Policies.Access)]
        public async Task<IEnumerable<TypeRequestChartDto>> GetChargeRequestFilterChart(ReportFilterDto search)
        {
            return await _chargerequestService.GetChartByFilter(search);
        }

        [HttpPost("cargo/download/pdf")]
        [Authorize(Policies.Download)]
        public async Task<IActionResult> ChargeRequestPDF(ReportFilterDto search)
        {
            var file = await _chargerequestService.DownloadReportPdf(search);
            return File(file, MimeType.PDF, "Sol_Cargo.pdf");
        }
    }
}
