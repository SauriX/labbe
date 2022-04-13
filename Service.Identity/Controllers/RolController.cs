using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Service.Identity.Domain.Users;
using Service.Identity.Dtos;
using Service.Identity.Repository.IRepository;
using Shared.Dictionary;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Service.Identity.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RolController : ControllerBase
    {
        private readonly IRolRepository _service;
        public RolController(IRolRepository service)
        {
            _service = service;
        }
        [HttpPost]
        public async Task<bool> Create(RolForm rolForm, [FromHeader] string authorization)
        {
            return await _service.Create(rolForm, authorization);
        }

        [HttpPut]
        public async Task<bool> Update(RolForm rolForm, [FromHeader] string authorization) {
            return await _service.Update(rolForm, authorization);
        }
        [HttpGet("all/{search?}")]
        public async Task<List<RolInfo>> GetAll(string search) {
            return await _service.GetAll(search);
        }
        [HttpGet("rol/{id}")]
        public async Task<RolForm> GetById(string id) {
            return await _service.GetById(id);
        }
        [HttpGet("permisos")]
        public async Task<List<UserPermission>> GetPermission() {
            return await _service.GetPermission();
        }

        [HttpPost("export/list/{search?}")]
        public async Task<IActionResult> ExportList(string search = null)
        {
            var file = await _service.ExportList(search);
            return File(file, MimeType.XLSX);
        }
        [HttpPost("export/form/{id}")]
        public async Task<IActionResult> ExportForm(string id)
        {
            var file = await _service.ExportForm(id);
            return File(file, MimeType.XLSX);
        }
    }
}
