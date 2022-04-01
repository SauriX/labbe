using Microsoft.AspNetCore.Mvc;
using Service.Identity.Dtos;
using Service.Identity.Repository.IRepository;
using System.Threading.Tasks;

namespace Service.Identity.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SessionController
    {
        private readonly ISessionRepository _service;

        public SessionController(ISessionRepository service)
        {
            _service = service;
        }
        [HttpPost("login")]
        public async Task <LoginResponse> Create(LoginDto user)
        {
            return await _service.Login(user);
        }
    }
}
