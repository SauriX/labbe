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
        [HttpGet("department/all/{search?}")]
        [Authorize(Policies.Access)]
        public async Task<IEnumerable<CatalogListDto>> GetAllDepartment(string search = null)
        {
            return await _departmentService.GetAll(search);
        }

        [HttpGet("department/active")]
        [Authorize(Policies.Access)]
        public async Task<IEnumerable<CatalogListDto>> GetActiveDepartment(int id)
        {
            return await _departmentService.GetActive();
        }        
        
        [HttpGet("department/{id}")]
        [Authorize(Policies.Access)]
        public async Task<CatalogFormDto> GetDepartmentById(int id)
        {
            return await _departmentService.GetById(id);
        }

        [HttpPost("department")]
        [Authorize(Policies.Create)]
        public async Task<CatalogListDto> CreateDepartment(CatalogFormDto catalog)
        {
            catalog.UsuarioId = (Guid)HttpContext.Items["userId"];
            return await _departmentService.Create(catalog);
        }

        [HttpPut("department")]
        [Authorize(Policies.Update)]
        public async Task<CatalogListDto> UpdateDepartment(CatalogFormDto catalog)
        {
            catalog.UsuarioId = (Guid)HttpContext.Items["userId"];
            return await _departmentService.Update(catalog);
        }
    }
}
