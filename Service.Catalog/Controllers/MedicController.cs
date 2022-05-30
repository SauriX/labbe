using Identidad.Api.Infraestructure.Services.IServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Service.Catalog.Dtos.Medicos;
using Shared.Dictionary;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Identidad.Api.Controllers
{
    [Route("api/[Controller]")]
    [ApiController]
    public class MedicController : ControllerBase
    {
        private readonly IMedicsApplication _Services;

        public MedicController(IMedicsApplication services)
        {
            _Services = services;
        }

        [HttpGet("all/{search}")]
        [Authorize(Policies.Access)]
        public async Task<IEnumerable<MedicsListDto>> GetAll(string search)
        {
            return await _Services.GetAll(search);
        }

        [HttpGet("{Id}")]
        [Authorize(Policies.Access)]
        public async Task<MedicsFormDto> GetById(Guid Id)
        {
            return await _Services.GetById(Id);
        }

        [HttpPost]
        [Authorize(Policies.Create)]
        public async Task Create(MedicsFormDto Medics)
        {
            await _Services.Create(Medics);
        }

        [HttpPut]
        [Authorize(Policies.Update)]
        public async Task Update(MedicsFormDto medics)
        {
            await _Services.Update(medics);
        }

        [HttpPost("export/list/{search?}")]
        [Authorize(Policies.Download)]
        public async Task<IActionResult> ExportList(string search = null)
        {
            var file = await _Services.ExportList(search);
            return File(file, MimeType.XLSX);
        }

        [HttpPost("export/form/{id}")]
        [Authorize(Policies.Download)]
        public async Task<IActionResult> ExportForm(Guid id)
        {
            var file = await _Services.ExportForm(id);
            return File(file, MimeType.XLSX);
        }

    }
}
