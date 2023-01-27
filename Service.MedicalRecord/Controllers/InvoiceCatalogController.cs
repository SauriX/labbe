using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Service.MedicalRecord.Application.IApplication;
using Service.MedicalRecord.Dtos.InvoiceCatalog;
using Shared.Dictionary;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Service.MedicalRecord.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InvoiceCatalogController : ControllerBase
    {
        private readonly IInvoiceCatalogApplication _service;
        
        public InvoiceCatalogController(IInvoiceCatalogApplication service)
        {
            _service = service;
            
        }
        [HttpPost]
        [Authorize(Policies.Access)]
        public async Task<List<InvoiceCatalogList>> GetAll(InvoiceCatalogSearch search)
        {
            var clinicResults = await _service.getAll(search);
            return clinicResults;
        }

        [HttpPost("export/list")]
        [Authorize(Policies.Download)]
        public async Task<IActionResult> ExportListPriceList(InvoiceCatalogSearch search  = null)
        {
            var (file, fileName) = await _service.ExportList(search);
            return File(file, MimeType.XLSX, fileName);
        }

    }
}
