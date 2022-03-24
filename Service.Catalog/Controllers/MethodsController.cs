using Microsoft.AspNetCore.Mvc;
using Service.Catalog.Dtos.Catalog;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Service.Catalog.Controllers
{
    public partial class CatalogController : ControllerBase
    {

        [HttpGet("methods/all/{search?}")]
        public async Task<IEnumerable<CatalogListDto>> GetAllMethods(string search = null)
        {
            return await _methodsService.GetAll(search);
        }

        [HttpGet("methods/{id}")]
        public async Task<CatalogFormDto> GetMethodsById(int id)
        {
            return await _methodsService.GetById(id);
        }

        [HttpPost("methods")]
        public async Task CreateMethods(CatalogFormDto catalog)
        {
            catalog.UsuarioId = "userId";
            await _methodsService.Create(catalog);
        }

        [HttpPut("methods")]
        public async Task UpdateMethods(CatalogFormDto catalog)
        {
            catalog.UsuarioId = "userId";
            await _methodsService.Update(catalog);
        }
    }
}