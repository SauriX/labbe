using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Service.Catalog.Dtos.Catalog;
using Shared.Dictionary;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Service.Catalog.Controllers.Catalog
{
    public partial class CatalogController : ControllerBase
    {
        [HttpGet("invoiceconcepts/all/{search}")]
        [Authorize(Policies.Access)]
        public async Task<IEnumerable<CatalogDescriptionListDto>> GetAllInvoiceConcepts(string search)
        {
            return await _invoiceConceptsService.GetAll(search);
        }

        [HttpGet("invoiceconcepts/active")]
        public async Task<IEnumerable<CatalogDescriptionListDto>> GetActiveInvoiceConcepts()
        {
            return await _invoiceConceptsService.GetActive();
        }

        [HttpGet("invoiceconcepts/{id}")]
        [Authorize(Policies.Access)]
        public async Task<CatalogDescriptionFormDto> GetInvoiceConceptsById(int id)
        {
            return await _invoiceConceptsService.GetById(id);
        }

        [HttpPost("invoiceconcepts")]
        [Authorize(Policies.Create)]
        public async Task<CatalogListDto> CreateInvoiceConcepts(CatalogDescriptionFormDto catalog)
        {
            catalog.UsuarioId = (Guid)HttpContext.Items["userId"];
            return await _invoiceConceptsService.Create(catalog);
        }

        [HttpPut("invoiceconcepts")]
        [Authorize(Policies.Update)]
        public async Task<CatalogListDto> UpdateInvoiceConcepts(CatalogDescriptionFormDto catalog)
        {
            catalog.UsuarioId = (Guid)HttpContext.Items["userId"];
            return await _invoiceConceptsService.Update(catalog);
        }

        [HttpPost("invoiceconcepts/export/list/{search}")]
        [Authorize(Policies.Download)]
        public async Task<IActionResult> ExportListInvoiceConcepts(string search)
        {
            var file = await _invoiceConceptsService.ExportList(search, "Usos de CFDI");
            return File(file, MimeType.XLSX, "Catálogo de Uso de CFDI.xlsx");
        }

        [HttpPost("invoiceconcepts/export/form/{id}")]
        [Authorize(Policies.Download)]
        public async Task<IActionResult> ExportFormInvoiceConcepts(int id)
        {
            var (file, code) = await _invoiceConceptsService.ExportForm(id, "Usos de CFDI");
            return File(file, MimeType.XLSX, $"Catálogo de Uso de CFDI ({code}).xlsx");
        }
    }
}
