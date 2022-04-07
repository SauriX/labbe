using Microsoft.AspNetCore.Mvc;
using Service.Catalog.Dtos.Catalog;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Service.Catalog.Controllers.Catalog
{
    public partial class CatalogController : ControllerBase
    {
        [HttpGet("indicator/all/{search?}")]
        public async Task<IEnumerable<CatalogDescriptionListDto>> GetAllIndicator(string search = null)
        {
            return await _indicatorService.GetAll(search);
        }

        [HttpGet("indicator/{id}")]
        public async Task<CatalogDescriptionFormDto> GetindIcatorById(int id)
        {
            return await _indicatorService.GetById(id);
        }

        [HttpPost("indicator")]
        public async Task<CatalogListDto> CreateIndicator(CatalogDescriptionFormDto catalog)
        {
            catalog.UsuarioId = "userId";
            return await _indicatorService.Create(catalog);
        }

        [HttpPut("indicator")]
        public async Task<CatalogListDto> UpdateIndicator(CatalogDescriptionFormDto catalog)
        {
            catalog.UsuarioId = "userId";
            return await _indicatorService.Update(catalog);
        }
    }
}