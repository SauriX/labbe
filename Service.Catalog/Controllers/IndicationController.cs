using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using Shared.Dictionary;
using Service.Catalog.Application.IApplication;
using Service.Catalog.Dtos.Indication;
using Microsoft.AspNetCore.Authorization;
using System;

namespace Service.Catalog.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class IndicationController : ControllerBase
    {
        private readonly IIndicationApplication _service;

        public IndicationController(IIndicationApplication service)
        {
            _service = service;
        }

        [HttpGet("all/{search}")]
        [Authorize(Policies.Access)]
        public async Task<IEnumerable<IndicationListDto>> GetAll(string search)
        {
            return await _service.GetAll(search);
        }

        [HttpGet("{id}")]
        [Authorize(Policies.Access)]
        public async Task<IndicationFormDto> GetById(int id)
        {
            return await _service.GetById(id);
        }

        [HttpPost]
        [Authorize(Policies.Create)]
        public async Task<IndicationListDto> Create(IndicationFormDto indicacion)
        {
            indicacion.UsuarioId = (Guid)HttpContext.Items["userId"];
            return await _service.Create(indicacion);
        }

        [HttpPut]
        [Authorize(Policies.Update)]
        public async Task<IndicationListDto> Update(IndicationFormDto indication)
        {
            indication.UsuarioId = (Guid)HttpContext.Items["userId"];
            return await _service.Update(indication);
        }

        [HttpPost("export/list/{search}")]
        [Authorize(Policies.Download)]
        public async Task<IActionResult> ExportListIndication(string search)
        {
            var (file, fileName) = await _service.ExportList(search);
            return File(file, MimeType.XLSX, fileName);
        }

        [HttpPost("export/form/{id}")]
        [Authorize(Policies.Download)]
        public async Task<IActionResult> ExportFormIndication(int id)
        {
            var (file, fileName) = await _service.ExportForm(id);
            return File(file, MimeType.XLSX, fileName);
        }
    }
}
