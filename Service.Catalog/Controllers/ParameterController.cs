using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Service.Catalog.Application.IApplication;
using Service.Catalog.Dtos.Parameters;
using Shared.Dictionary;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Service.Catalog.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ParameterController : ControllerBase
    {
        private readonly IParameterApplication _service;

        public ParameterController(IParameterApplication service)
        {
            _service = service;
        }

        [HttpGet("all/{search}")]
        [Authorize(Policies.Access)]
        public async Task<IEnumerable<ParameterListDto>> GetAll(string search)
        {
            return await _service.GetAll(search);
        }

        [HttpGet("active")]
        public async Task<IEnumerable<ParameterListDto>> GetActive()
        {
            return await _service.GetActive();
        }

        [HttpGet("{id}")]
        [Authorize(Policies.Access)]
        public async Task<ParameterFormDto> GetById(string id)
        {
            return await _service.GetById(id);
        }

        [HttpGet("value/{id}")]
        [Authorize(Policies.Access)]
        public async Task<ParameterValueDto> GetValue(string id)
        {
            return await _service.GetValueById(id);
        }

        [HttpGet("all/values/{id}/{type}")]
        [Authorize(Policies.Access)]
        public async Task<IEnumerable<ParameterValueDto>> GetAllValues(string id)
        {
            return await _service.GetAllValues(id);
        }

        [HttpPost]
        [Authorize(Policies.Create)]
        public async Task<ParameterListDto> Create(ParameterFormDto parameter)
        {
            parameter.UsuarioId = (Guid)HttpContext.Items["userId"];
            return await _service.Create(parameter);
        }

        [HttpPost("value")]
        [Authorize(Policies.Create)]
        public async Task AddValue(ParameterValueDto value)
        {
            await _service.AddValue(value);
        }

        [HttpPut]
        [Authorize(Policies.Update)]
        public async Task<ParameterListDto> Update(ParameterFormDto parameter)
        {
            parameter.UsuarioId = (Guid)HttpContext.Items["userId"];
            return await _service.Update(parameter);
        }

        [HttpPut("value")]
        [Authorize(Policies.Update)]
        public async Task UpdateValue(ParameterValueDto value)
        {
            await _service.UpdateValue(value);
        }

        [HttpDelete("{id}")]
        [Authorize(Policies.Update)]
        public async Task DeleteValue(string id)
        {
            await _service.DeleteValue(id);
        }

        [HttpPost("export/list/{search}")]
        public async Task<IActionResult> ExportList(string search)
        {
            var (file, fileName) = await _service.ExportList(search);
            return File(file, MimeType.XLSX, fileName);
        }

        [HttpPost("export/form/{id}")]
        public async Task<IActionResult> ExportForm(string id)
        {
            var (file, fileName) = await _service.ExportForm(id);
            return File(file, MimeType.XLSX, fileName);
        }
    }
}
