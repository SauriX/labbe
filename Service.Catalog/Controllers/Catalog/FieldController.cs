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
        [HttpGet("field/all/{search?}")]
        [Authorize(Policies.Access)]
        public async Task<IEnumerable<CatalogListDto>> GetAllField(string search = null)
        {
            return await _fieldService.GetAll(search);
        }

        [HttpGet("field/active")]
        public async Task<IEnumerable<CatalogListDto>> GetActiveField()
        {
            return await _fieldService.GetActive();
        }

        [HttpGet("field/{id}")]
        [Authorize(Policies.Access)]
        public async Task<CatalogFormDto> GetFieldById(int id)
        {
            return await _fieldService.GetById(id);
        }

        [HttpPost("field")]
        [Authorize(Policies.Create)]
        public async Task<CatalogListDto> CreateField(CatalogFormDto catalog)
        {
            catalog.UsuarioId = (Guid)HttpContext.Items["userId"];
            return await _fieldService.Create(catalog);
        }

        [HttpPut("field")]
        [Authorize(Policies.Update)]
        public async Task<CatalogListDto> UpdateField(CatalogFormDto catalog)
        {
            catalog.UsuarioId = (Guid)HttpContext.Items["userId"];
            return await _fieldService.Update(catalog);
        }

        [HttpPost("field/export/list/{search}")]
        [Authorize(Policies.Download)]
        public async Task<IActionResult> ExportListField(string search)
        {
            var file = await _fieldService.ExportList(search, "Especialidades");
            return File(file, MimeType.XLSX, "Catálogo de Especialidades.xlsx");
        }

        [HttpPost("field/export/form/{id}")]
        [Authorize(Policies.Download)]
        public async Task<IActionResult> ExportFormField(int id)
        {
            var (file, code) = await _fieldService.ExportForm(id, "Especialidades");
            return File(file, MimeType.XLSX, $"Catálogo de Especialidad ({code}).xlsx");
        }
    }
}