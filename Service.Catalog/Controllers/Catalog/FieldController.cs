using Microsoft.AspNetCore.Mvc;
using Service.Catalog.Dtos.Catalog;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Service.Catalog.Controllers.Catalog
{
    public partial class CatalogController : ControllerBase
    {
        [HttpGet("field/all/{search?}")]
        public async Task<IEnumerable<CatalogListDto>> GetAllField(string search = null)
        {
            return await _fieldService.GetAll(search);
        }

        [HttpGet("field/{id}")]
        public async Task<CatalogFormDto> GetFieldById(int id)
        {
            return await _fieldService.GetById(id);
        }

        [HttpGet("field/active")]
        public async Task<IEnumerable<CatalogListDto>> GetActiveField(int id)
        {
            return await _departmentService.GetActive();
        }

        [HttpPost("field")]
        public async Task<CatalogListDto> CreateField(CatalogFormDto catalog)
        {
            catalog.UsuarioId = "userId";
            return await _fieldService.Create(catalog);
        }

        [HttpPut("field")]
        public async Task<CatalogListDto> UpdateField(CatalogFormDto catalog)
        {
            catalog.UsuarioId = "userId";
            return await _fieldService.Update(catalog);
        }
    }
}