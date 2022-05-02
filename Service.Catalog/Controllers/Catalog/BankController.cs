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
        [HttpGet("bank/all/{search?}")]
        [Authorize(Policies.Access)]
        public async Task<IEnumerable<CatalogListDto>> GetAllBank(string search = null)
        {
            return await _bankService.GetAll(search);
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
    }
}
