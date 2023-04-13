using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Service.Report.Dtos;
using Service.Report.Dtos.CashRegister;
using Shared.Dictionary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

namespace Service.Report.Controllers.Report
{
    public partial class ReportController : ControllerBase
    {
        [HttpPost("corte_caja/filter")]
        [Authorize(Policies.Access)]
        public async Task<CashDto> GetCashRegistertNow(ReportFilterDto search)
        {
            _logger.LogError("IN CONTROLLER CASH/FILTER");
            return await _cashregisterService.GetByFilter(search);
        }

        [HttpPost("corte_caja/download/pdf")]
        [Authorize(Policies.Download)]
        public async Task<IActionResult> CashRegisterPDF(ReportFilterDto search)
        {
            search.User = HttpContext.Items["fullname"].ToString();
            var file = await _cashregisterService.DownloadReportPdf(search);
            return File(file, MimeType.PDF, "CorteCaja.pdf");
        }
    }
}
