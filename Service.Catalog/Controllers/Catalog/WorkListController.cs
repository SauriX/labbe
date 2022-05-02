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
        [HttpGet("workList/all/{search?}")]
        [Authorize(Policies.Access)]
        public async Task<IEnumerable<CatalogListDto>> GetAllWorkList(string search = null)
        {
            return await _workListService.GetAll(search);
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
    }
}