using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Service.Report.Dtos;
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
        public async Task<IEnumerable<BondedRequestDto>> GetInternalNow(ReportFilterDto search)
        {
            return await _bondedrequestApplication.GetByFilter(search);
        }

        //[HttpPost("estudios/chart/filter")]
        //[Authorize(Policies.Access)]
        //public async Task<IEnumerable<BondedRequestChartDto>> GetInternalFilterChart(ReportFilterDto search)
        //{
        //    return await _bondedrequestApplication.GetChartByFilter(search);
        //}

        [HttpPost("maquila_interna/download/pdf")]
        [Authorize(Policies.Download)]
        public async Task<IActionResult> InternalPDF(ReportFilterDto search)
        {
            var file = await _bondedrequestApplication.DownloadReportPdf(search);
            return File(file, MimeType.PDF, "Maquila_Interna.pdf");
        }

        [HttpPost("maquila_externa/filter")]
        [Authorize(Policies.Access)]
        public async Task<IEnumerable<BondedRequestDto>> GetExternalNow(ReportFilterDto search)
        {
            return await _bondedrequestApplication.GetByFilter(search);
        }

        //[HttpPost("estudios/chart/filter")]
        //[Authorize(Policies.Access)]
        //public async Task<IEnumerable<BondedRequestChartDto>> GetInternalFilterChart(ReportFilterDto search)
        //{
        //    return await _bondedrequestApplication.GetChartByFilter(search);
        //}

        [HttpPost("maquila_externa/download/pdf")]
        [Authorize(Policies.Download)]
        public async Task<IActionResult> ExternalPDF(ReportFilterDto search)
        {
            var file = await _bondedrequestApplication.DownloadReportPdf(search);
            return File(file, MimeType.PDF, "Maquila_Externa.pdf");
        }
    }
}