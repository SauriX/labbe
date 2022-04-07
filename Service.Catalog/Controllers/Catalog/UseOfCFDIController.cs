using Microsoft.AspNetCore.Mvc;
using Service.Catalog.Dtos.Catalog;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Service.Catalog.Controllers.Catalog
{
    public partial class CatalogController : ControllerBase
    {
        [HttpGet("useOfCFDI/all/{search?}")]
        public async Task<IEnumerable<CatalogDescriptionListDto>> GetAllUseOfCFDI(string search = null)
        {
            return await _useOfCFDIService.GetAll(search);
        }

        [HttpGet("useOfCFDI/{id}")]
        public async Task<CatalogDescriptionFormDto> GetUseOfCFDIById(int id)
        {
            return await _useOfCFDIService.GetById(id);
        }

        [HttpPost("useOfCFDI")]
        public async Task<CatalogListDto> CreateUseOfCFDI(CatalogDescriptionFormDto catalog)
        {
            catalog.UsuarioId = "userId";
            return await _useOfCFDIService.Create(catalog);
        }

        [HttpPut("useOfCFDI")]
        public async Task<CatalogListDto> UpdateUseOfCFDI(CatalogDescriptionFormDto catalog)
        {
            catalog.UsuarioId = "userId";
            return await _useOfCFDIService.Update(catalog);
        }
    }
}