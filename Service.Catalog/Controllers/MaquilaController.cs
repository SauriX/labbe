using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Service.Catalog.Application.IApplication;
using Service.Catalog.Dtos.Maquila;
using Shared.Dictionary;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Service.Catalog.Controllers
{
    [Route("api/[Controller]")]
    [ApiController]
    public class MaquilaController : ControllerBase
    {
        private readonly IMaquilaApplication _service;

        public MaquilaController(IMaquilaApplication service)
        {
            _service = service;
        }

        [HttpGet("all/{search}")]
        [Authorize(Policies.Access)]
        public async Task<IEnumerable<MaquilaListDto>> GetAll(string search)
        {
            return await _service.GetAll(search);
        }

        [HttpGet("active")]
        public async Task<IEnumerable<MaquilaListDto>> GetActive()
        {
            return await _service.GetActive();
        }

        [HttpGet("{id}")]
        [Authorize(Policies.Access)]
        public async Task<MaquilaFormDto> GetById(int id)
        {
            return await _service.GetById(id);
        }

        [HttpPost]
        [Authorize(Policies.Create)]
        public async Task<MaquilaListDto> Create(MaquilaFormDto maquila)
        {
            maquila.UsuarioId = (Guid)HttpContext.Items["userId"];
            return await _service.Create(maquila);
        }

        [HttpPut]
        [Authorize(Policies.Update)]
        public async Task<MaquilaListDto> Update(MaquilaFormDto maquila)
        {
            maquila.UsuarioId = (Guid)HttpContext.Items["userId"];
            return await _service.Update(maquila);
        }

        [HttpPost("export/list/{search}")]
        [Authorize(Policies.Download)]
        public async Task<IActionResult> ExportList(string search)
        {
            var (file, fileName) = await _service.ExportList(search);
            return File(file, MimeType.XLSX, fileName);
        }

        [HttpPost("export/form/{id}")]
        [Authorize(Policies.Download)]
        public async Task<IActionResult> ExportForm(int id)
        {
            var (file, fileName) = await _service.ExportForm(id);
            return File(file, MimeType.XLSX, fileName);
        }

    }
}
