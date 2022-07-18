using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Service.Report.Application.IApplication;
using Service.Report.Dtos.Request;
using Service.Report.PdfModel;
using Shared.Dictionary;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Service.Report.Controllers.Report
{
    public partial class ReportController : ControllerBase
    {    
        [HttpGet("expediente/getBranchByCount")]
        //[Authorize(Policies.Access)]
        public async Task<IEnumerable<RequestFiltroDto>> GetBranchByCount() 
        {
            return await _requestService.GetBranchByCount();
        }
        [HttpPost("expediente/filter")]
        //[Authorize(Policies.Access)]
        public async Task<IEnumerable<RequestFiltroDto>> GetNow(RequestSearchDto search )
        {
            return await _requestService.GetFilter(search);
        }

        [HttpPost("expediente/export/table/{search?}")]
        [Authorize(Policies.Download)]
        public async Task<IActionResult> ExportTableBranch(string search = null)
        {
            var (file, fileName) = await _requestService.ExportTableBranch(search);
            return File(file, MimeType.XLSX, fileName);
        }

        [HttpPost("expediente/export/graphic/{search?}")]
        [Authorize(Policies.Download)]
        public async Task<IActionResult> ExportGraphicBranch(string search = null)
        {
            var (file, fileName) = await _requestService.ExportGraphicBranch(search);
            return File(file, MimeType.XLSX, fileName);
        }

        [HttpPost("expediente/download/pdf")]
        [AllowAnonymous]
        public async Task<IActionResult> ExpedientePDF(RequestSearchDto search)
        {
            var file = await _requestService.GenerateReportPDF(search);

            return File(file, MimeType.PDF, "EstadísticaExpediente.pdf");
        }
    }
}