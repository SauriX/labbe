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
        [HttpPost("descuento/filter")]
        [Authorize(Policies.Access)]
        public async Task<IEnumerable<TypeRequestDto>> GetDescountRequestNow(ReportFilterDto search)
        {
            return await _descountrequestService.GetByFilter(search);
        }

        [HttpPost("descuento/table/filter")]
        [Authorize(Policies.Access)]
        public async Task<TypeDto> GetFilterDescountRequestTable(ReportFilterDto search)
        {
            return await _descountrequestService.GetTableByFilter(search);
        }

        [HttpPost("descuento/chart/filter")]
        [Authorize(Policies.Access)]
        public async Task<IEnumerable<TypeRequestChartDto>> GetDescountRequestFilterChart(ReportFilterDto search)
        {
            return await _descountrequestService.GetChartByFilter(search);
        }

        [HttpPost("descuento/download/pdf")]
        [Authorize(Policies.Download)]
        public async Task<IActionResult> DescountRequestPDF(ReportFilterDto search)
        {
            var file = await _descountrequestService.DownloadReportPdf(search);
            return File(file, MimeType.PDF, "Sol_Descuento.pdf");
        }
    }
}
