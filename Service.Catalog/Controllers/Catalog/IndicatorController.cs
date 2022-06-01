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
        [HttpGet("indicator/all/{search?}")]
        [Authorize(Policies.Access)]
        public async Task<IEnumerable<CatalogDescriptionListDto>> GetAllIndicator(string search = null)
        {
            return await _indicatorService.GetAll(search);
        }

        [HttpGet("indicator/active")]
        public async Task<IEnumerable<CatalogListDto>> GetActiveIndicator()
        {
            return await _indicatorService.GetActive();
        }

        [HttpGet("indicator/{id}")]
        [Authorize(Policies.Access)]
        public async Task<CatalogDescriptionFormDto> GetindIcatorById(int id)
        {
            return await _indicatorService.GetById(id);
        }

        [HttpPost("indicator")]
        [Authorize(Policies.Create)]
        public async Task<CatalogListDto> CreateIndicator(CatalogDescriptionFormDto catalog)
        {
            catalog.UsuarioId = (Guid)HttpContext.Items["userId"];
            return await _indicatorService.Create(catalog);
        }

        [HttpPut("indicator")]
        [Authorize(Policies.Update)]
        public async Task<CatalogListDto> UpdateIndicator(CatalogDescriptionFormDto catalog)
        {
            catalog.UsuarioId = (Guid)HttpContext.Items["userId"];
            return await _indicatorService.Update(catalog);
        }

        [HttpPost("indicator/export/list/{search}")]
        [Authorize(Policies.Download)]
        public async Task<IActionResult> ExportListIndicator(string search)
        {
            var file = await _indicatorService.ExportList(search);
            return File(file, MimeType.XLSX, "Catálogo de Indicadores.xlsx");
        }

        [HttpPost("indicator/export/form/{id}")]
        [Authorize(Policies.Download)]
        public async Task<IActionResult> ExportFormIndicator(int id)
        {
            var (file, code) = await _indicatorService.ExportForm(id);
            return File(file, MimeType.XLSX, $"Catálogo de Indicadores ({code}).xlsx");
        }
    }
}