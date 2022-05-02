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
        [HttpGet("sampleType/all/{search?}")]
        [Authorize(Policies.Access)]
        public async Task<IEnumerable<CatalogListDto>> GetAllSampleType(string search = null)
        {
            return await _sampleTypeService.GetAll(search);
        }

        [HttpGet("sampleType/{id}")]
        [Authorize(Policies.Access)]
        public async Task<CatalogFormDto> GetSampleTypeById(int id)
        {
            return await _sampleTypeService.GetById(id);
        }

        [HttpPost("sampleType")]
        [Authorize(Policies.Create)]
        public async Task<CatalogListDto> CreateSampleType(CatalogFormDto catalog)
        {
            catalog.UsuarioId = (Guid)HttpContext.Items["userId"];
            return await _sampleTypeService.Create(catalog);
        }

        [HttpPut("sampleType")]
        [Authorize(Policies.Update)]
        public async Task<CatalogListDto> UpdateSampleType(CatalogFormDto catalog)
        {
            catalog.UsuarioId = (Guid)HttpContext.Items["userId"];
            return await _sampleTypeService.Update(catalog);
        }
    }
}