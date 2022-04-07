using Microsoft.AspNetCore.Mvc;
using Service.Catalog.Dtos.Catalog;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Service.Catalog.Controllers.Catalog
{
    public partial class CatalogController : ControllerBase
    {
        [HttpGet("department/all/{search?}")]
        public async Task<IEnumerable<CatalogListDto>> GetAllDepartment(string search = null)
        {
            return await _departmentService.GetAll(search);
        }

        [HttpGet("department/active")]
        public async Task<IEnumerable<CatalogListDto>> GetActiveDepartment(int id)
        {
            return await _departmentService.GetActive();
        }        
        
        [HttpGet("department/{id}")]
        public async Task<CatalogFormDto> GetDepartmentById(int id)
        {
            return await _departmentService.GetById(id);
        }

        [HttpPost("department")]
        public async Task<CatalogListDto> CreateDepartment(CatalogFormDto catalog)
        {
            catalog.UsuarioId = "userId";
            return await _departmentService.Create(catalog);
        }

        [HttpPut("department")]
        public async Task<CatalogListDto> UpdateDepartment(CatalogFormDto catalog)
        {
            catalog.UsuarioId = "userId";
            return await _departmentService.Update(catalog);
        }
    }
}
