using Microsoft.AspNetCore.Mvc;
using Service.Catalog.Dtos.Catalog;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Service.Catalog.Controllers.Catalog
{
    public partial class CatalogController : ControllerBase
    {
        [HttpGet("sampleType/all/{search?}")]
        public async Task<IEnumerable<CatalogListDto>> GetAllSampleType(string search = null)
        {
            return await _sampleTypeService.GetAll(search);
        }

        [HttpGet("sampleType/{id}")]
        public async Task<CatalogFormDto> GetSampleTypeById(int id)
        {
            return await _sampleTypeService.GetById(id);
        }

        [HttpPost("sampleType")]
        public async Task<CatalogListDto> CreateSampleType(CatalogFormDto catalog)
        {
            catalog.UsuarioId = "userId";
            return await _sampleTypeService.Create(catalog);
        }

        [HttpPut("sampleType")]
        public async Task<CatalogListDto> UpdateSampleType(CatalogFormDto catalog)
        {
            catalog.UsuarioId = "userId";
            return await _sampleTypeService.Update(catalog);
        }
    }
}