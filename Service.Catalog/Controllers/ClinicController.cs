using Microsoft.AspNetCore.Mvc;
using Service.Catalog.Dtos.Catalog;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Service.Catalog.Controllers
{
    public partial class CatalogController : ControllerBase
    {

        [HttpGet("clinic/all/{search?}")]
        public async Task<IEnumerable<CatalogListDto>> GetAllClinic(string search = null)
        {
            return await _clinicService.GetAll(search);
        }

        [HttpGet("clinic/{id}")]
        public async Task<CatalogFormDto> GetClinicById(int id)
        {
            return await _clinicService.GetById(id);
        }

        [HttpPost("clinic")]
        public async Task CreateClinic(CatalogFormDto catalog)
        {
            catalog.UsuarioId = "userId";
            await _clinicService.Create(catalog);
        }

        [HttpPut("clinic")]
        public async Task UpdateClinic(CatalogFormDto catalog)
        {
            catalog.UsuarioId = "userId";
            await _clinicService.Update(catalog);
        }
    }
}
