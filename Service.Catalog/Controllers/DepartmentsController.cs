using Microsoft.AspNetCore.Mvc;
using Service.Catalog.Dtos.Catalog;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Service.Catalog.Controllers
{
    public partial class CatalogController : ControllerBase
    {

        [HttpGet("departments/all/{search?}")]
        public async Task<IEnumerable<CatalogListDto>> GetAllDepartments(string search = null)
        {
            return await _departmentsService.GetAll(search);
        }

        [HttpGet("departments/{id}")]
        public async Task<CatalogFormDto> GetDepartmentsById(int id)
        {
            return await _departmentsService.GetById(id);
        }

        [HttpPost("departments")]
        public async Task CreateDepartments(CatalogFormDto catalog)
        {
            catalog.UsuarioId = "userId";
            await _departmentsService.Create(catalog);
        }

        [HttpPut("departments")]
        public async Task UpdateDepartments(CatalogFormDto catalog)
        {
            catalog.UsuarioId = "userId";
            await _departmentsService.Update(catalog);
        }
    }
}
