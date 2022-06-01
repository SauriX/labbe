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
        [HttpGet("bank/all/{search}")]
        [Authorize(Policies.Access)]
        public async Task<IEnumerable<CatalogListDto>> GetAllBank(string search)
        {
            return await _bankService.GetAll(search);

        }

        [HttpGet("bank/active")]
        public async Task<IEnumerable<CatalogListDto>> GetActiveBank(int id)
        {
            return await _bankService.GetActive();
        }

        [HttpGet("bank/{id}")]
        [Authorize(Policies.Access)]
        public async Task<CatalogFormDto> GetBankById(int id)
        {
            return await _bankService.GetById(id);
        }

        [HttpPost("bank")]
        [Authorize(Policies.Create)]
        public async Task<CatalogListDto> CreateBank(CatalogFormDto catalog)
        {
            catalog.UsuarioId = (Guid)HttpContext.Items["userId"];
            return await _bankService.Create(catalog);
        }

        [HttpPut("bank")]
        [Authorize(Policies.Update)]
        public async Task<CatalogListDto> UpdateBank(CatalogFormDto catalog)
        {
            catalog.UsuarioId = (Guid)HttpContext.Items["userId"];
            return await _bankService.Update(catalog);
        }

        [HttpPost("bank/export/list/{search}")]
        [Authorize(Policies.Download)]
        public async Task<IActionResult> ExportListBank(string search)
        {
            var file = await _bankService.ExportList(search);
            return File(file, MimeType.XLSX, "Catálogo de Bancos.xlsx");
        }

        [HttpPost("bank/export/form/{id}")]
        [Authorize(Policies.Download)]
        public async Task<IActionResult> ExportFormBank(int id)
        {
            var (file, code) = await _bankService.ExportForm(id);
            return File(file, MimeType.XLSX, $"Catálogo de Bancos ({code}).xlsx");
        }
    }
}
