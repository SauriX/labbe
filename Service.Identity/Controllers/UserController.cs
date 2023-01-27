using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Service.Identity.Application.IApplication;
using Service.Identity.Dtos.Profile;
using Service.Identity.Dtos.User;
using Shared.Dictionary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Service.Identity.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserApplication _service;

        public UserController(IUserApplication service)
        {
            _service = service;
        }

        [HttpGet("all/{search?}")]
        [Authorize(Policies.Access)]
        public async Task<IEnumerable<UserListDto>> GetAll(string search = null)
        {
            return await _service.GetAll(search);
        }

        [HttpGet("{id}")]
        [Authorize(Policies.Access)]
        public async Task<UserFormDto> GetById(string id)
        {
            return await _service.GetById(id);
        }

        [HttpGet("passwordGenerator")]
        [Authorize(Policies.Access)]
        public string GeneratePassword()
        {
            return _service.GeneratePassword();
        }

        [HttpGet("permission")]
        [Authorize(Policies.Access)]
        public async Task<IEnumerable<UserPermissionDto>> GetPermission()
        {
            return await _service.GetPermission();
        }

        [HttpPost("code")]
        [Authorize(Policies.Access)]
        public async Task<string> GenerateCode(UserCodeDto data)
        {
            return await _service.GenerateCode(data);
        }
        [HttpPost("updateBranch/{id}")]
        [Authorize(Policies.Access)]
        public async Task<UserListDto> UpdateBranch(Guid id)
        {
            var userId = (Guid)HttpContext.Items["userId"];
            return await _service.UpdateBranch(id, userId);
        }

        [HttpPost]
        [Authorize(Policies.Create)]
        public async Task<UserListDto> Create(UserFormDto user)
        {
            user.UsuarioId = (Guid)HttpContext.Items["userId"];
            return await _service.Create(user);
        }

        [HttpPut]
        [Authorize(Policies.Update)]
        public async Task<UserListDto> Update(UserFormDto user)
        {
            user.UsuarioId = (Guid)HttpContext.Items["userId"];
            return await _service.Update(user);
        }

        [HttpPut("password")]
        //[Authorize(Policies.Update)]
        public async Task UpdatePassword(ChangePasswordFormDto form)
        {
            var userId = Guid.Parse(User.Claims.SingleOrDefault(x => x.Type == ClaimTypes.NameIdentifier)?.Value);

            form.UsuarioId = userId;
            if (string.IsNullOrWhiteSpace(form.Id))
            {
                form.Id = userId.ToString();
            }
            await _service.UpdatePassword(form);
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

        [HttpPut("images")]
        [Authorize(Policies.Update)]
        public async Task<string> SaveImage([FromForm] RequestImageDto requestDto)
        {
            requestDto.UsuarioId = (Guid)HttpContext.Items["userId"];

            return await _service.SaveImage(requestDto);
        }
    }
}
