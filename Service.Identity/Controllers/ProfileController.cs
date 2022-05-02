using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Service.Identity.Application.IApplication;
using Service.Identity.Dtos;
using Service.Identity.Dtos.Menu;
using Service.Identity.Dtos.Profile;
using Service.Identity.Repository.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Service.Identity.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProfileController : ControllerBase
    {
        private readonly IProfileApplication _service;

        public ProfileController(IProfileApplication service)
        {
            _service = service;
        }

        [HttpGet("menu")]
        public async Task<IEnumerable<MenuDto>> GetMenu()
        {
            var userId = GetUserId();
            return await _service.GetMenu(userId);
        }

        [HttpGet("me")]
        public async Task<ProfileDto> GetProfile()
        {
            var userId = GetUserId();
            return await _service.GetProfile(userId);
        }

        [HttpPost("login")]
        [AllowAnonymous]
        public async Task<ProfileDto> Login(LoginDto credentials)
        {
            return await _service.Login(credentials);
        }

        private Guid GetUserId()
        {
            return Guid.Parse(User.Claims.SingleOrDefault(x => x.Type == ClaimTypes.NameIdentifier)?.Value);
        }
    }
}
