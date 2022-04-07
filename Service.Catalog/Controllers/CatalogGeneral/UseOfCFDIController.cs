using Microsoft.AspNetCore.Mvc;
using Service.Catalog.Dtos.Catalog;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Service.Catalog.Controllers
{
    public partial class CatalogDescController : ControllerBase
    {

        [HttpGet("useOfCFDI/all/{search?}")]
        public async Task<IEnumerable<CatalogDescListDto>> GetAllUseOfCFDI(string search = null)
        {
            return await _useOfCFDIService.GetAll(search);
        }

        [HttpGet("useOfCFDI/{id}")]
        public async Task<CatalogDescFormDto> GetUseOfCFDIById(int id)
        {
            return await _useOfCFDIService.GetById(id);
        }

        [HttpPost("useOfCFDI")]
        public async Task CreateUseOfCFDI(CatalogDescFormDto catalog)
        {
            catalog.UsuarioId = "userId";
            await _useOfCFDIService.Create(catalog);
        }

        [HttpPut("useOfCFDI")]
        public async Task UpdateUseOfCFDI(CatalogDescFormDto catalog)
        {
            catalog.UsuarioId = "userId";
            await _useOfCFDIService.Update(catalog);
        }
    }
}