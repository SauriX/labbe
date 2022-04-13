using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using Shared.Dictionary;
using Service.Catalog.Application.IApplication;
using Service.Catalog.Domain.Indication;
using Service.Catalog.Dtos.Indication;

namespace Identidad.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class IndicationController : ControllerBase
    {

        private readonly IIndicationApplication _indicationService;
        

        public IndicationController(IIndicationApplication indicationService)
        {
            _indicationService = indicationService;
        }
        [HttpGet("all/{search?}")]
        public async Task<IEnumerable<IndicationListDto>> GetAll(string search = null)
        {
            return await _indicationService.GetAll(search);
        }

        [HttpGet("{id}")]
        public async Task<IndicationFormDto> GetById(int id)
        {
            return await _indicationService.GetById(id);
        }

        [HttpPost]
        public async Task Create(IndicationFormDto indicacion)
        {
             await _indicationService.Create(indicacion);
        }

        [HttpPut]
        public async Task Update(IndicationFormDto indication)
        {
            await _indicationService.Update(indication);
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
