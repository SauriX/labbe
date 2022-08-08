using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Service.Report.Dtos;
using Service.Report.Dtos.CanceledRequest;
using Shared.Dictionary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Service.Report.Controllers.Report
{
    public partial class ReportController : ControllerBase
    {
        [HttpPost("canceladas/filter")]
        [Authorize(Policies.Access)]
        public async Task<IEnumerable<CanceledRequestDto>> GetCanceledRequestNow(ReportFilterDto search)
        {
            return await _canceledrequestService.GetByFilter(search);
        }

        [HttpPost("canceladas/table/filter")]
        [Authorize(Policies.Access)]
        public async Task<CanceledDto> GetFilterCanceledRequestTable(ReportFilterDto search)
        {
            return await _canceledrequestService.GetTableByFilter(search);
        }

        [HttpPost("canceladas/chart/filter")]
        [Authorize(Policies.Access)]
        public async Task<IEnumerable<CanceledRequestChartDto>> GetCanceledRequestFilterChart(ReportFilterDto search)
        {
            return await _canceledrequestService.GetChartByFilter(search);
        }

        [HttpPost("canceladas/download/pdf")]
        [Authorize(Policies.Download)]
        public async Task<IActionResult> CanceledRequestPDF(ReportFilterDto search)
        {
            var file = await _canceledrequestService.DownloadReportPdf(search);
            return File(file, MimeType.PDF, "Sol_Canceladas.pdf");
        }
    }
}
