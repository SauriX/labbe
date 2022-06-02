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
        [HttpGet("delivery/all/{search?}")]
        [Authorize(Policies.Access)]
        public async Task<IEnumerable<CatalogListDto>> GetAllDelivery(string search)
        {
            return await _deliveryService.GetAll(search);
        }

        [HttpGet("delivery/active")]
        [Authorize(Policies.Access)]
        public async Task<IEnumerable<CatalogListDto>> GetActiveDelivery()
        {
            return await _deliveryService.GetActive();
        }

        [HttpGet("delivery/{id}")]
        [Authorize(Policies.Access)]
        public async Task<CatalogFormDto> GetDeliveryById(int id)
        {
            return await _deliveryService.GetById(id);
        }

        [HttpPost("delivery")]
        [Authorize(Policies.Create)]
        public async Task<CatalogListDto> CreateDelivery(CatalogFormDto catalog)
        {
            catalog.UsuarioId = (Guid)HttpContext.Items["userId"];
            return await _deliveryService.Create(catalog);
        }

        [HttpPut("delivery")]
        [Authorize(Policies.Update)]
        public async Task<CatalogListDto> UpdateDelivery(CatalogFormDto catalog)
        {
            catalog.UsuarioId = (Guid)HttpContext.Items["userId"];
            return await _deliveryService.Update(catalog);
        }

        [HttpPost("delivery/export/list/{search}")]
        [Authorize(Policies.Download)]
        public async Task<IActionResult> ExportListDelivery(string search)
        {
            var file = await _deliveryService.ExportList(search);
            return File(file, MimeType.XLSX, "Catálogo de Paqueterías.xlsx");
        }

        [HttpPost("delivery/export/form/{id}")]
        [Authorize(Policies.Download)]
        public async Task<IActionResult> ExportFormDelivery(int id)
        {
            var (file, code) = await _deliveryService.ExportForm(id);
            return File(file, MimeType.XLSX, $"Catálogo de Paqueterías ({code}).xlsx");
        }
    }
}
