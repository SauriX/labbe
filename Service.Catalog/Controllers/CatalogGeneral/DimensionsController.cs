using Microsoft.AspNetCore.Mvc;
using Service.Catalog.Dtos.Catalog;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Service.Catalog.Controllers
{
    public partial class CatalogController : ControllerBase
    {

        [HttpGet("dimensions/all/{search?}")]
        public async Task<IEnumerable<CatalogListDto>> GetAllDimensions(string search = null)
        {
            return await _dimensionsService.GetAll(search);
        }

        [HttpGet("dimensions/{id}")]
        public async Task<CatalogFormDto> GetDimensionsById(int id)
        {
            return await _dimensionsService.GetById(id);
        }

        [HttpPost("dimensions")]
        public async Task CreateDimensions(CatalogFormDto catalog)
        {
            catalog.UsuarioId = "userId";
            await _dimensionsService.Create(catalog);
        }

        [HttpPut("dimensions")]
        public async Task UpdateDimensions(CatalogFormDto catalog)
        {
            catalog.UsuarioId = "userId";
            await _dimensionsService.Update(catalog);
        }
    }
}