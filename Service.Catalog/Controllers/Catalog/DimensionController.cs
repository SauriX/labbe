using Microsoft.AspNetCore.Mvc;
using Service.Catalog.Dtos.Catalog;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Service.Catalog.Controllers.Catalog
{
    public partial class CatalogController : ControllerBase
    {
        [HttpGet("dimension/all/{search?}")]
        public async Task<IEnumerable<DimensionListDto>> GetAllDimension(string search = null)
        {
            return await _dimensionService.GetAll(search);
        }

        [HttpGet("dimension/{id}")]
        public async Task<DimensionFormDto> GetDimensionById(int id)
        {
            return await _dimensionService.GetById(id);
        }

        [HttpPost("dimension")]
        public async Task<DimensionListDto> CreateDimension(DimensionFormDto catalog)
        {
            catalog.UsuarioId = "userId";
            return await _dimensionService.Create(catalog);
        }

        [HttpPut("dimension")]
        public async Task<DimensionListDto> UpdateDimension(DimensionFormDto catalog)
        {
            catalog.UsuarioId = "userId";
            return await _dimensionService.Update(catalog);
        }
    }
}