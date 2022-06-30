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
        [HttpGet("clinic/all/{search}")]
        [Authorize(Policies.Access)]
        public async Task<IEnumerable<CatalogListDto>> GetAllClinic(string search)
        {
            return await _clinicService.GetAll(search);
        }

        [HttpGet("clinic/active")]
        [Authorize(Policies.Access)]
        public async Task<IEnumerable<CatalogListDto>> GetActiveClinic()
        {
            return await _clinicService.GetActive();
        }

        [HttpGet("clinic/{id}")]
        [Authorize(Policies.Access)]
        public async Task<CatalogFormDto> GetClinicById(int id)
        {
            return await _clinicService.GetById(id);
        }

        [HttpPost("clinic")]
        [Authorize(Policies.Create)]
        public async Task<CatalogListDto> CreateClinic(CatalogFormDto catalog)
        {
            catalog.UsuarioId = (Guid)HttpContext.Items["userId"];
            return await _clinicService.Create(catalog);
        }

        [HttpPut("clinic")]
        [Authorize(Policies.Update)]
        public async Task<CatalogListDto> UpdateClinic(CatalogFormDto catalog)
        {
            catalog.UsuarioId = (Guid)HttpContext.Items["userId"];
            return await _clinicService.Update(catalog);
        }

        [HttpPost("clinic/export/list/{search}")]
        [Authorize(Policies.Download)]
        public async Task<IActionResult> ExportListClinic(string search)
        {
            var file = await _clinicService.ExportList(search, "Clínicas");
            return File(file, MimeType.XLSX, "Catálogo de Clínicas.xlsx");
        }

        [HttpPost("clinic/export/form/{id}")]
        [Authorize(Policies.Download)]
        public async Task<IActionResult> ExportFormClinic(int id)
        {
            var (file, code) = await _clinicService.ExportForm(id, "Clínicas");
            return File(file, MimeType.XLSX, $"Catálogo de Clínicas ({code}).xlsx");
        }
    }
}
