﻿using Microsoft.AspNetCore.Mvc;
using Service.Catalog.Dtos.Catalog;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Service.Catalog.Controllers.Catalog
{
    public partial class CatalogController : ControllerBase
    {
        [HttpGet("format/all/{search?}")]
        public async Task<IEnumerable<CatalogListDto>> GetAllFormat(string search = null)
        {
            return await _formatService.GetAll(search);

        }

        [HttpGet("format/active")]
        public async Task<IEnumerable<CatalogListDto>> GetActiveFormat()
        {
            return await _formatService.GetActive();
        }

        [HttpGet("format/{id}")]
        public async Task<CatalogFormDto> GetByIdFormat(int id)
        {
            return await _formatService.GetById(id);
        }

        [HttpPost("format")]
        public async Task<CatalogListDto> CreateFormat(CatalogFormDto catalog)
        {
            catalog.UsuarioId = System.Guid.NewGuid();
            return await _formatService.Create(catalog);
        }

        [HttpPut("format")]
        public async Task<CatalogListDto> UpdateFormat(CatalogFormDto catalog)
        {
            catalog.UsuarioId = System.Guid.NewGuid();
            return await _formatService.Update(catalog);
        }
    }
}
