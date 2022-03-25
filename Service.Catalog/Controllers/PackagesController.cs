using Microsoft.AspNetCore.Mvc;
using Service.Catalog.Dtos.Catalog;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Service.Catalog.Controllers
{
    public partial class CatalogController : ControllerBase
    {

        [HttpGet("packages/all/{search?}")]
        public async Task<IEnumerable<CatalogListDto>> GetAllPackages(string search = null)
        {
            return await _packagesService.GetAll(search);
        }

        [HttpGet("packages/{id}")]
        public async Task<CatalogFormDto> GetPackagesById(int id)
        {
            return await _packagesService.GetById(id);
        }

        [HttpPost("packages")]
        public async Task CreatePackages(CatalogFormDto catalog)
        {
            catalog.UsuarioId = "userId";
            await _packagesService.Create(catalog);
        }

        [HttpPut("packages")]
        public async Task UpdatePackages(CatalogFormDto catalog)
        {
            catalog.UsuarioId = "userId";
            await _packagesService.Update(catalog);
        }
    }
}