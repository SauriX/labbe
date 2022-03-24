using Microsoft.AspNetCore.Mvc;
using Service.Catalog.Dtos.Catalog;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Service.Catalog.Controllers
{
    public partial class CatalogController : ControllerBase
    {

        [HttpGet("area/all/{search?}")]
        public async Task<IEnumerable<CatalogListDto>> GetAllArea(string search = null)
        {
            return await _areaService.GetAll(search);
        }

        [HttpGet("area/{id}")]
        public async Task<CatalogFormDto> GetAreaById(int id)
        {
            return await _areaService.GetById(id);
        }

        [HttpPost("area")]
        public async Task CreateArea(CatalogFormDto catalog)
        {
            catalog.UsuarioId = "userId";
            await _areaService.Create(catalog);
        }

        [HttpPut("area")]
        public async Task UpdateArea(CatalogFormDto catalog)
        {
            catalog.UsuarioId = "userId";
            await _areaService.Update(catalog);
        }
    }
}
