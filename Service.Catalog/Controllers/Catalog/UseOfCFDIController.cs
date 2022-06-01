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
        [HttpGet("useOfCFDI/all/{search}")]
        [Authorize(Policies.Access)]
        public async Task<IEnumerable<CatalogDescriptionListDto>> GetAllUseOfCFDI(string search)
        {
            return await _useOfCFDIService.GetAll(search);
        }

        [HttpGet("useOfCFDI/active")]
        public async Task<IEnumerable<CatalogDescriptionListDto>> GetActiveUseOfCFDI()
        {
            return await _useOfCFDIService.GetActive();
        }

        [HttpGet("useOfCFDI/{id}")]
        [Authorize(Policies.Access)]
        public async Task<CatalogDescriptionFormDto> GetUseOfCFDIById(int id)
        {
            return await _useOfCFDIService.GetById(id);
        }

        [HttpPost("useOfCFDI")]
        [Authorize(Policies.Create)]
        public async Task<CatalogListDto> CreateUseOfCFDI(CatalogDescriptionFormDto catalog)
        {
            catalog.UsuarioId = (Guid)HttpContext.Items["userId"];
            return await _useOfCFDIService.Create(catalog);
        }

        [HttpPut("useOfCFDI")]
        [Authorize(Policies.Update)]
        public async Task<CatalogListDto> UpdateUseOfCFDI(CatalogDescriptionFormDto catalog)
        {
            catalog.UsuarioId = (Guid)HttpContext.Items["userId"];
            return await _useOfCFDIService.Update(catalog);
        }

        [HttpPost("useOfCFDI/export/list/{search}")]
        [Authorize(Policies.Download)]
        public async Task<IActionResult> ExportListUseOfCFDI(string search)
        {
            var file = await _useOfCFDIService.ExportList(search);
            return File(file, MimeType.XLSX, "Catálogo de Uso de CFDI.xlsx");
        }

        [HttpPost("useOfCFDI/export/form/{id}")]
        [Authorize(Policies.Download)]
        public async Task<IActionResult> ExportFormUseOfCFDI(int id)
        {
            var (file, code) = await _useOfCFDIService.ExportForm(id);
            return File(file, MimeType.XLSX, $"Catálogo de Uso de CFDI ({code}).xlsx");
        }
    }
}