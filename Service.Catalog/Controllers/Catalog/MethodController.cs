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
        [HttpGet("method/all/{search?}")]
        [Authorize(Policies.Access)]
        public async Task<IEnumerable<CatalogListDto>> GetAllMethod(string search = null)
        {
            return await _methodService.GetAll(search);
        }

        [HttpGet("method/{id}")]
        [Authorize(Policies.Access)]
        public async Task<CatalogFormDto> GetMethodById(int id)
        {
            return await _methodService.GetById(id);
        }

        [HttpPost("method")]
        [Authorize(Policies.Create)]
        public async Task<CatalogListDto> CreateMethod(CatalogFormDto catalog)
        {
            catalog.UsuarioId = (Guid)HttpContext.Items["userId"];
            return await _methodService.Create(catalog);
        }

        [HttpPut("method")]
        [Authorize(Policies.Update)]
        public async Task<CatalogListDto> UpdateMethod(CatalogFormDto catalog)
        {
            catalog.UsuarioId = (Guid)HttpContext.Items["userId"];
            return await _methodService.Update(catalog);
        }
    }
}