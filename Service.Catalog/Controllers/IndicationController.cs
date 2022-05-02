using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using Shared.Dictionary;
using Service.Catalog.Application.IApplication;
using Service.Catalog.Domain.Indication;
using Service.Catalog.Dtos.Indication;
using Microsoft.AspNetCore.Authorization;

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
        [Authorize(Policies.Access)]
        public async Task<IEnumerable<IndicationListDto>> GetAll(string search = null)
        {
            return await _indicationService.GetAll(search);
        }

        [HttpGet("{id}")]
        [Authorize(Policies.Access)]
        public async Task<IndicationFormDto> GetById(int id)
        {
            return await _indicationService.GetById(id);
        }

        [HttpPost]
        [Authorize(Policies.Create)]
        public async Task Create(IndicationFormDto indicacion)
        {
            await _indicationService.Create(indicacion);
        }

        [HttpPut]
        [Authorize(Policies.Update)]
        public async Task Update(IndicationFormDto indication)
        {
            await _indicationService.Update(indication);
        }

        [HttpPost("export/list/{search?}")]
        [Authorize(Policies.Download)]
        public async Task<IActionResult> ExportListIndication(string search = null)
        {
            var file = await _indicationService.ExportListIndication(search);
            return File(file, MimeType.XLSX);
        }

        [HttpPost("export/form/{id}")]
        [Authorize(Policies.Download)]
        public async Task<IActionResult> ExportFormIndication(int id)
        {
            var file = await _indicationService.ExportFormIndication(id);
            return File(file, MimeType.XLSX);
        }
    }

}
