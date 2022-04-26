using Microsoft.AspNetCore.Mvc;
using Service.Catalog.Dtos.Catalog;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Service.Catalog.Controllers.Catalog
{
    public partial class CatalogController : ControllerBase
    {
        [HttpGet("provenance/all/{search?}")]
        public async Task<IEnumerable<CatalogListDto>> GetAllProvenance(string search = null)
        {
            return await _provenanceService.GetAll(search);

        }

        [HttpGet("peovenance/active")]
        public async Task<IEnumerable<CatalogListDto>> GetActiveRpovenance(int id)
        {
            return await _provenanceService.GetActive();
        }

        [HttpGet("provenance/{id}")]
        public async Task<CatalogFormDto> GetByIdPRovenance(int id)
        {
            return await _provenanceService.GetById(id);
        }

        [HttpPost("peovenance")]
        public async Task<CatalogListDto> CreateProvenance(CatalogFormDto catalog)
        {
            catalog.UsuarioId = "userId";
            return await _provenanceService.Create(catalog);
        }

        [HttpPut("provenance")]
        public async Task<CatalogListDto> UpdateProvenance(CatalogFormDto catalog)
        {
            catalog.UsuarioId = "userId";
            return await _provenanceService.Update(catalog);
        }
    }
}
