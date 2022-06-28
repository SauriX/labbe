using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Service.Catalog.Application.IApplication;
using Service.Catalog.Dtos.Pack;
using Shared.Dictionary;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Service.Catalog.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PackController : ControllerBase
    {
        private readonly IPackApplication _Service;

        public PackController(IPackApplication service)
        {
            _Service = service;
        }

        [HttpGet("{id}")]
        [Authorize(Policies.Access)]
        public async Task<PackFormDto> GetById(int id)
        {
            return await _Service.GetById(id);
        }

        [HttpGet("all/{search?}")]
        [Authorize(Policies.Access)]
        public async Task<IEnumerable<PackListDto>> GetAll(string search = null)
        {
            return await _Service.GetAll(search);
        }

        [HttpGet("active")]
        [Authorize(Policies.Access)]
        public async Task<IEnumerable<PackListDto>> GetActive()
        {
            return await _Service.GetActive();
        }

        [HttpPost]
        [Authorize(Policies.Create)]
        public async Task<PackListDto> Create(PackFormDto pack)
        {
            pack.IdUsuario = (Guid)HttpContext.Items["userId"];
            return await _Service.Create(pack);
        }

        [HttpPut]
        [Authorize(Policies.Update)]
        public async Task<PackListDto> Update(PackFormDto pack)
        {
            pack.IdUsuario = (Guid)HttpContext.Items["userId"];
            return await _Service.Update(pack);
        }

        [HttpPost("export/list/{search?}")]
        [Authorize(Policies.Download)]
        public async Task<IActionResult> ExportList(string search = null)
        {
            var (file, fileName) = await _Service.ExportList(search);
            return File(file, MimeType.XLSX, fileName);
        }

        [HttpPost("export/form/{id}")]
        [Authorize(Policies.Download)]
        public async Task<IActionResult> ExportForm(int id)
        {
            var (file, fileName) = await _Service.ExportForm(id);
            return File(file, MimeType.XLSX, fileName);
        }
    }
}
