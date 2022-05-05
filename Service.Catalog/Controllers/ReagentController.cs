using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Service.Catalog.Application.IApplication;
using Service.Catalog.Dtos.Reagent;
using Shared.Dictionary;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Service.Catalog.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReagentController : ControllerBase
    {
        private readonly IReagentApplication _service;

        public ReagentController(IReagentApplication service)
        {
            _service = service;
        }

        [HttpGet("all/{search}")]
        [Authorize(Policies.Access)]
        public async Task<IEnumerable<ReagentListDto>> GetAll(string search)
        {
            return await _service.GetAll(search);
        }

        [HttpGet("active")]
        public async Task<IEnumerable<ReagentListDto>> GetActive()
        {
            return await _service.GetActive();
        }

        [HttpGet("{id}")]
        [Authorize(Policies.Access)]
        public async Task<ReagentFormDto> GetById(string id)
        {
            return await _service.GetById(id);
        }

        [HttpPost]
        [Authorize(Policies.Create)]
        public async Task<ReagentListDto> Create(ReagentFormDto reagent)
        {
            reagent.UsuarioId = (Guid)HttpContext.Items["userId"];
            return await _service.Create(reagent);
        }

        [HttpPut]
        [Authorize(Policies.Update)]
        public async Task<ReagentListDto> Update(ReagentFormDto reagent)
        {
            reagent.UsuarioId = (Guid)HttpContext.Items["userId"];
            return await _service.Update(reagent);
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
        public async Task<IActionResult> ExportForm(string id)
        {
            var (file, fileName) = await _service.ExportForm(id);
            return File(file, MimeType.XLSX, fileName);
        }
    }
}
