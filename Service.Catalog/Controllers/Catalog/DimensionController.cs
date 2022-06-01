﻿using Microsoft.AspNetCore.Authorization;
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
        [HttpGet("dimension/all/{search?}")]
        [Authorize(Policies.Access)]
        public async Task<IEnumerable<DimensionListDto>> GetAllDimension(string search = null)
        {
            return await _dimensionService.GetAll(search);
        }

        [HttpGet("dimension/active")]
        public async Task<IEnumerable<DimensionListDto>> GetActiveDimension()
        {
            return await _dimensionService.GetActive();
        }

        [HttpGet("dimension/{id}")]
        [Authorize(Policies.Access)]
        public async Task<DimensionFormDto> GetDimensionById(int id)
        {
            return await _dimensionService.GetById(id);
        }

        [HttpPost("dimension")]
        [Authorize(Policies.Create)]
        public async Task<DimensionListDto> CreateDimension(DimensionFormDto catalog)
        {
            catalog.UsuarioId = (Guid)HttpContext.Items["userId"];
            return await _dimensionService.Create(catalog);
        }

        [HttpPut("dimension")]
        [Authorize(Policies.Update)]
        public async Task<DimensionListDto> UpdateDimension(DimensionFormDto catalog)
        {
            catalog.UsuarioId = (Guid)HttpContext.Items["userId"];
            return await _dimensionService.Update(catalog);
        }

        [HttpPost("dimension/export/list/{search}")]
        [Authorize(Policies.Download)]
        public async Task<IActionResult> ExportListDimension(string search)
        {
            var file = await _dimensionService.ExportList(search);
            return File(file, MimeType.XLSX, "Catálogo de Dimensiones.xlsx");
        }

        [HttpPost("dimension/export/form/{id}")]
        [Authorize(Policies.Download)]
        public async Task<IActionResult> ExportFormDimension(int id)
        {
            var (file, code) = await _dimensionService.ExportForm(id);
            return File(file, MimeType.XLSX, $"Catálogo de Dimensiones ({code}).xlsx");
        }
    }
}