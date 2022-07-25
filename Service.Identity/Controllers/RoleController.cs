using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Service.Identity.Application.IApplication;
using Service.Identity.Dtos.Role;
using Shared.Dictionary;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Service.Identity.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoleController : ControllerBase
    {
        private readonly IRoleApplication _service;

        public RoleController(IRoleApplication service)
        {
            _service = service;
        }

        [HttpGet("all/{search?}")]
        [Authorize(Policies.Access)]
        public async Task<IEnumerable<RoleListDto>> GetAll(string search)
        {
            return await _service.GetAll(search);
        }

        [HttpGet("active")]
        [Authorize(Policies.Access)]
        public async Task<IEnumerable<RoleListDto>> GetActive()
        {
            return await _service.GetActive();
        }

        [HttpGet("{id}")]
        [Authorize(Policies.Access)]
        public async Task<RoleFormDto> GetById(string id)
        {
            return await _service.GetById(id);
        }

        [HttpGet("permission")]
        [Authorize(Policies.Access)]
        public async Task<IEnumerable<RolePermissionDto>> GetPermission()
        {
            return await _service.GetPermission();
        }

        [HttpGet("permission/{id}")]
        [Authorize(Policies.Access)]
        public async Task<IEnumerable<RolePermissionDto>> GetPermission(string id)
        {
            return await _service.GetPermission(id);
        }

        [HttpPost]
        [Authorize(Policies.Create)]
        public async Task<RoleListDto> Create(RoleFormDto role)
        {
            role.UsuarioId = (Guid)HttpContext.Items["userId"];
            return await _service.Create(role);
        }

        [HttpPut]
        [Authorize(Policies.Update)]
        public async Task<RoleListDto> Update(RoleFormDto role)
        {
            role.UsuarioId = (Guid)HttpContext.Items["userId"];
            return await _service.Update(role);
        }

        [HttpPost("export/list/{search?}")]
        [Authorize(Policies.Download)]
        public async Task<IActionResult> ExportList(string search = null)
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
