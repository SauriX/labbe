using Microsoft.AspNetCore.Mvc;
using Service.Catalog.Dtos.Catalog;
using System.Collections.Generic;
using System.Threading.Tasks;
using Shared.Dictionary;
using Service.Catalog.Application.IApplication;
using Service.Catalog.Domain.Indication;

namespace Service.Catalog.Controllers.Catalog
{
    [Route("api/[controller]")]
    [ApiController]
    public partial class IndicationController : ControllerBase

    {
        private readonly ICatalogDescriptionApplication<Indication> _indicationService;
        

        public IndicationController(ICatalogDescriptionApplication<Indication> indicationService)
        {
            _indicationService = indicationService;
        }
        [HttpGet("all/{search?}")]
        public async Task<IEnumerable<CatalogDescriptionListDto>> GetAllIndication(string search = null)
        {
            return await _indicationService.GetAll(search);
        }

        [HttpGet("{id}")]
        public async Task<CatalogDescriptionFormDto> GetByIdIndication(int id)
        {
            return await _indicationService.GetById(id);
        }

        [HttpPost]
        public async Task<CatalogListDto> CreateIndication(CatalogDescriptionFormDto catalog)
        {
            catalog.UsuarioId = "userId";
            return await _indicationService.Create(catalog);
        }

        [HttpPut]
        public async Task<CatalogListDto> UpdateIndication(CatalogDescriptionFormDto catalog)
        {
            catalog.UsuarioId = "userId";
            return await _indicationService.Update(catalog);
        }

        [HttpPost("export/list/{search?}")]
        public async Task<IActionResult> ExportListIndication(string search = null)
        {
            var file = await _indicationService.ExportListIndication(search);
            return File(file, MimeType.XLSX);
        }

        [HttpPost("export/form/{id}")]
        public async Task<IActionResult> ExportFormIndication(int id)
        {
            var file = await _indicationService.ExportFormIndication(id);
            return File(file, MimeType.XLSX);
        }
    }

}
