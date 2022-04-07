using Microsoft.AspNetCore.Mvc;
using Service.Catalog.Dtos.Catalog;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Service.Catalog.Controllers.Catalog
{
    public partial class CatalogController : ControllerBase
    {
        [HttpGet("area/all/{search?}")]
        public async Task<IEnumerable<AreaListDto>> GetAllArea(string search = null)
        {
            return await _areaService.GetAll(search);
        }

        [HttpGet("area/{id}")]
        public async Task<AreaFormDto> GetAreaById(int id)
        {
            return await _areaService.GetById(id);
        }

        [HttpPost("area")]
        public async Task<AreaListDto> CreateArea(AreaFormDto catalog)
        {
            catalog.UsuarioId = "userId";
            return await _areaService.Create(catalog);
        }

        [HttpPut("area")]
        public async Task<AreaListDto> UpdateArea(AreaFormDto catalog)
        {
            catalog.UsuarioId = "userId";
            return await _areaService.Update(catalog);
        }
    }
}
