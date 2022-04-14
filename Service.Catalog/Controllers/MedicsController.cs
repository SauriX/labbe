using Identidad.Api.Infraestructure.Services.IServices;
using Identidad.Api.ViewModels.Medicos;
using Identidad.Api.ViewModels.Menu;
using Microsoft.AspNetCore.Mvc;
using Service.Catalog.Domain.Medics;
using Service.Catalog.Dtos.Medicos;
using Shared.Dictionary;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Identidad.Api.Controllers
{
    [Route("api/[Controller]")]
    [ApiController]
    public class MedicsController : ControllerBase
    {
        private readonly IMedicsApplication _Services;

        public MedicsController(IMedicsApplication services)
        {
            _Services = services;
        }

        [HttpGet("{Id}")]

        public async Task<MedicsFormDto> GetById (int Id) 
        {
            return await _Services.GetById(Id);
        }

        [HttpGet("all/{search}")]
        public async Task<IEnumerable<MedicsListDto>> GetAll(string search = null)
        {
            return await _Services.GetAll(search);
        }
        [HttpPost]
        public async Task Create(MedicsFormDto Medics)
        {

            await _Services.Create(Medics);
        }
        [HttpPut]
        public async Task Update(MedicsFormDto medics)
        {
            await _Services.Update(medics);
        }

        [HttpPost("export/list/{search?}")]
        public async Task<IActionResult> ExportList(string search = null)
        {
            var file = await _Services.ExportList(search);
            return File(file, MimeType.XLSX);
        }

        [HttpPost("export/form/{id}")]
        public async Task<IActionResult> ExportForm(int id)
        {
            var file = await _Services.ExportForm(id);
            return File(file, MimeType.XLSX);
        }

    }
}
