using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Service.Report.Application.IApplication;
using Service.Report.Dtos.Request;
using Service.Report.PdfModel;
using Shared.Dictionary;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Service.Report.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RequestController: ControllerBase
    {
        private readonly IRequestApplication _requestService;

        public RequestController(IRequestApplication indicationService)
        {
            _requestService = indicationService;
        }
         
        [HttpGet("getBranchByCount")]
        //[Authorize(Policies.Access)]
        public async Task<IEnumerable<RequestFiltroDto>> GetBranchByCount()
        {
            return await _requestService.GetBranchByCount();
        }
        [HttpPost("filter")]
        //[Authorize(Policies.Access)]
        public async Task<IEnumerable<RequestFiltroDto>> GetNow(RequestSearchDto search )
        {
            return await _requestService.GetFilter(search);
        }

        [HttpPost("export/table/{search?}")]
        [Authorize(Policies.Download)]
        public async Task<IActionResult> ExportTableBranch(string search = null)
        {
            var (file, fileName) = await _requestService.ExportTableBranch(search);
            return File(file, MimeType.XLSX, fileName);
        }

        [HttpPost("export/graphic/{search?}")]
        [Authorize(Policies.Download)]
        public async Task<IActionResult> ExportGraphicBranch(string search = null)
        {
            var (file, fileName) = await _requestService.ExportGraphicBranch(search);
            return File(file, MimeType.XLSX, fileName);
        }

        [HttpPost("download/pdf")]
        [AllowAnonymous]
        public async Task<IActionResult> ExpedientePDF(RequestSearchDto search)
        {
            var file = await _requestService.GenerateReportPDF(search);

            return File(file, MimeType.PDF, "EstadísticaExpediente.pdf");
        }
    }
}