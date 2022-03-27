using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Service.Identity.Domain.Users;
using Service.Identity.Dtos;
using Service.Identity.Repository.IRepository;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Service.Identity.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IUserRepository _service;

        public AccountController(IUserRepository service)
        {
            _service = service;
        }


        [HttpGet("all/{search?}")]
        public async Task<IEnumerable<UserList>> GetAll(string search = null)
        {
            return (IEnumerable<UserList>)await _service.GetAll(search);
        }

        [HttpPost]
        public async Task Create(UsersModel user)
        {
            await _service.NewUser(user);
        }

        [HttpPut]
        public async Task Update(UsersModel user)
        {
            await _service.UpdateUser(user);
        }
    }
}
