using Microsoft.AspNetCore.Mvc;
using Service.Identity.Dtos;
using Service.Identity.Repository.IRepository;
using System.Collections.Generic;
using System.Threading.Tasks;
namespace Service.Identity.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RolController
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

        [HttpPut("update")]
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
    }
}
