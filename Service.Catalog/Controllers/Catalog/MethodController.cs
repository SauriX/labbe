using Microsoft.AspNetCore.Mvc;
using Service.Catalog.Dtos.Catalog;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Service.Catalog.Controllers.Catalog
{
    public partial class CatalogController : ControllerBase
    {
        [HttpGet("method/all/{search?}")]
        public async Task<IEnumerable<CatalogListDto>> GetAllMethod(string search = null)
        {
            return await _methodService.GetAll(search);
        }

        [HttpGet("method/{id}")]
        public async Task<CatalogFormDto> GetMethodById(int id)
        {
            return await _methodService.GetById(id);
        }

        [HttpPost("method")]
        public async Task<CatalogListDto> CreateMethod(CatalogFormDto catalog)
        {
            catalog.UsuarioId = "userId";
            return await _methodService.Create(catalog);
        }

        [HttpPut("method")]
        public async Task<CatalogListDto> UpdateMethod(CatalogFormDto catalog)
        {
            catalog.UsuarioId = "userId";
            return await _methodService.Update(catalog);
        }
    }
}