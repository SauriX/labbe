using Microsoft.AspNetCore.Mvc;
using Service.Catalog.Dtos.Catalog;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Service.Catalog.Controllers
{
    public partial class CatalogController : ControllerBase
    {

        [HttpGet("specialty/all/{search?}")]
        public async Task<IEnumerable<CatalogListDto>> GetAllSpecialty(string search = null)
        {
            return await _specialtyService.GetAll(search);
        }

        [HttpGet("specialty/{id}")]
        public async Task<CatalogFormDto> GetSpecialtyById(int id)
        {
            return await _specialtyService.GetById(id);
        }

        [HttpPost("specialty")]
        public async Task CreateSpecialty(CatalogFormDto catalog)
        {
            catalog.UsuarioId = "userId";
            await _specialtyService.Create(catalog);
        }

        [HttpPut("specialty")]
        public async Task UpdateSpecialty(CatalogFormDto catalog)
        {
            catalog.UsuarioId = "userId";
            await _specialtyService.Update(catalog);
        }
    }
}