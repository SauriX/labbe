using Microsoft.AspNetCore.Mvc;
using Service.Catalog.Application.IApplication;
using Service.Catalog.Dtos.Maquilador;
using Shared.Dictionary;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Identidad.Api.Controllers
{
    [Route("api/[Controller]")]
    [ApiController]
    public class MaquiladorController : ControllerBase
    {
        private readonly IMaquiladorApplication _Services;

        public MaquiladorController(IMaquiladorApplication services)
        {
            _Services = services;
        }

        [HttpGet("{Id}")]

        public async Task<MaquiladorFormDto> GetById(int Id)
        {
            return await _Services.GetById(Id);
        }

        [HttpGet("all/{search}")]
        public async Task<IEnumerable<MaquiladorListDto>> GetAll(string search = null)
        {
            return await _Services.GetAll(search);
        }
        [HttpPost]
        public async Task Create(MaquiladorFormDto Company)
        {

            await _Services.Create(Company);
        }
        [HttpPut]
        public async Task Update(MaquiladorFormDto Company)
        {
            await _Services.Update(Company);
        }

        [HttpPost("export/list/{search?}")]
        public async Task<IActionResult> ExportList(string search = null)
        {
            var file = await _Services.ExportListMaquilador(search);
            return File(file, MimeType.XLSX);
        }

        [HttpPost("export/form/{id}")]
        public async Task<IActionResult> ExportForm(int id)
        {
            var file = await _Services.ExportFormMaquilador(id);
            return File(file, MimeType.XLSX);
        }

    }
}
