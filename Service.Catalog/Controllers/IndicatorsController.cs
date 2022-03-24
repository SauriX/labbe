﻿using Microsoft.AspNetCore.Mvc;
using Service.Catalog.Dtos.Catalog;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Service.Catalog.Controllers
{
    public partial class CatalogController : ControllerBase
    {

        [HttpGet("indicators/all/{search?}")]
        public async Task<IEnumerable<CatalogListDto>> GetAllIndicators(string search = null)
        {
            return await _indicatorsService.GetAll(search);
        }

        [HttpGet("indicators/{id}")]
        public async Task<CatalogFormDto> GetindIcatorsById(int id)
        {
            return await _indicatorsService.GetById(id);
        }

        [HttpPost("indicators")]
        public async Task CreateIndicators(CatalogFormDto catalog)
        {
            catalog.UsuarioId = "userId";
            await _indicatorsService.Create(catalog);
        }

        [HttpPut("indicators")]
        public async Task UpdateIndicators(CatalogFormDto catalog)
        {
            catalog.UsuarioId = "userId";
            await _indicatorsService.Update(catalog);
        }
    }
}