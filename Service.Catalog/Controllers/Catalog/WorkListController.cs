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
        [HttpGet("workList/all/{search}")]
        [Authorize(Policies.Access)]
        public async Task<IEnumerable<CatalogListDto>> GetAllWorkList(string search)
        {
            return await _workListService.GetAll(search);
        }

        [HttpGet("workList/active")]
        public async Task<IEnumerable<CatalogListDto>> GetActiveWorkList()
        {
            return await _workListService.GetActive();
        }

        [HttpGet("workList/{id}")]
        [Authorize(Policies.Access)]
        public async Task<CatalogFormDto> GetWorkListById(int id)
        {
            return await _workListService.GetById(id);
        }

        [HttpPost("workList")]
        [Authorize(Policies.Create)]
        public async Task<CatalogListDto> CreateWorkList(CatalogFormDto catalog)
        {
            catalog.UsuarioId = (Guid)HttpContext.Items["userId"];
            return await _workListService.Create(catalog);
        }

        [HttpPut("workList")]
        [Authorize(Policies.Update)]
        public async Task<CatalogListDto> UpdateWorkList(CatalogFormDto catalog)
        {
            catalog.UsuarioId = (Guid)HttpContext.Items["userId"];
            return await _workListService.Update(catalog);
        }

        [HttpPost("workList/export/list/{search}")]
        [Authorize(Policies.Download)]
        public async Task<IActionResult> ExportListWorkList(string search)
        {
            var file = await _workListService.ExportList(search);
            return File(file, MimeType.XLSX, "Catálogo de Listas de trabajo.xlsx");
        }

        [HttpPost("workList/export/form/{id}")]
        [Authorize(Policies.Download)]
        public async Task<IActionResult> ExportFormWorkList(int id)
        {
            var (file, code) = await _workListService.ExportForm(id);
            return File(file, MimeType.XLSX, $"Catálogo de Listas de trabajo ({code}).xlsx");
        }
    }
}