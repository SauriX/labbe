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
        [HttpGet("Equipo/all/{search?}")]
        [Authorize(Policies.Access)]
        public async Task<IEnumerable<CatalogListDto>> GetAllEquipo(string search = null)
        {
            return await _equipmentService.GetAll(search);
        }

        [HttpGet("Equipo/active")]
        public async Task<IEnumerable<CatalogListDto>> GetActiveEquipo()
        {
            return await _equipmentService.GetActive();
        }

        [HttpGet("Equipo/{id}")]
        [Authorize(Policies.Access)]
        public async Task<CatalogFormDto> GetEquipoById(int id)
        {
            return await _equipmentService.GetById(id);
        }

        [HttpPost("Equipo")]
        [Authorize(Policies.Create)]
        public async Task<CatalogListDto> CreateEquipo(CatalogFormDto catalog)
        {
            catalog.UsuarioId = (Guid)HttpContext.Items["userId"];
            return await _equipmentService.Create(catalog);
        }

        [HttpPut("Equipo")]
        [Authorize(Policies.Update)]
        public async Task<CatalogListDto> UpdateEquipo(CatalogFormDto catalog)
        {
            catalog.UsuarioId = (Guid)HttpContext.Items["userId"];
            return await _equipmentService.Update(catalog);
        }

        [HttpPost("Equipo/export/list/{search}")]
        [Authorize(Policies.Download)]
        public async Task<IActionResult> ExportListEquipo(string search)
        {
            var file = await _equipmentService.ExportList(search, "Especialidades");
            return File(file, MimeType.XLSX, "Catálogo de Especialidad.xlsx");
        }

        [HttpPost("Equipo/export/form/{id}")]
        [Authorize(Policies.Download)]
        public async Task<IActionResult> ExportFormEquipo(int id)
        {
            var (file, code) = await _equipmentService.ExportForm(id, "Especialidades");
            return File(file, MimeType.XLSX, $"Catálogo de Especialidad ({code}).xlsx");
        }
    }
}
