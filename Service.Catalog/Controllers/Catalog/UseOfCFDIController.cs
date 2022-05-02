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
        [HttpGet("useOfCFDI/all/{search?}")]
        [Authorize(Policies.Access)]
        public async Task<IEnumerable<CatalogDescriptionListDto>> GetAllUseOfCFDI(string search = null)
        {
            return await _useOfCFDIService.GetAll(search);
        }

        [HttpGet("useOfCFDI/active")]
        public async Task<IEnumerable<CatalogDescriptionListDto>> GetActiveUseOfCFDI(int id)
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
    }
}