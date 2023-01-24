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
        [HttpGet("area/all/{search}")]
        [Authorize(Policies.Access)]
        public async Task<IEnumerable<AreaListDto>> GetAllArea(string search)
        {
            return await _areaService.GetAll(search);
        }

        [HttpGet("area/active")]
        [Authorize(Policies.Access)]
        public async Task<IEnumerable<AreaListDto>> GetActiveArea()
        {
            return await _areaService.GetActive();
        }

        [HttpGet("area/department/{departmentId}/active")]
        [Authorize(Policies.Access)]
        public async Task<IEnumerable<AreaListDto>> GetAreaByDepartment(int departmentId)
        {
            return await _areaService.GetAreaByDepartment(departmentId);
        }

        [HttpGet("area/{id}")]
        [Authorize(Policies.Access)]
        public async Task<AreaFormDto> GetAreaById(int id)
        {
            return await _areaService.GetById(id);
        }

        [HttpPost("area")]
        [Authorize(Policies.Create)]
        public async Task<AreaListDto> CreateArea(AreaFormDto catalog)
        {
            catalog.UsuarioId = (Guid)HttpContext.Items["userId"];
            return await _areaService.Create(catalog);
        }

        [HttpPut("area")]
        [Authorize(Policies.Update)]
        public async Task<AreaListDto> UpdateArea(AreaFormDto catalog)
        {
            catalog.UsuarioId = (Guid)HttpContext.Items["userId"];
            return await _areaService.Update(catalog);
        }

        [HttpPost("area/export/list/{search}")]
        [Authorize(Policies.Download)]
        public async Task<IActionResult> ExportListArea(string search)
        {
            var file = await _areaService.ExportList(search);
            return File(file, MimeType.XLSX, "Catálogo de Áreas.xlsx");
        }

        [HttpPost("area/export/form/{id}")]
        [Authorize(Policies.Download)]
        public async Task<IActionResult> ExportFormArea(int id)
        {
            var (file, code) = await _areaService.ExportForm(id);

            return File(file, MimeType.XLSX, $"Catálogo de Áreas ({code}).xlsx");
        }

        [HttpGet("area/getAreaByDeparment")]
        public async Task<IEnumerable<DepartmentAreaListDto>> GetAreaByDeparment()
        {
            return await _areaService.GetAreasByDeparments();
        }
    }
}
