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
        [HttpGet("user/{id}")]
        public async Task<UsersModel> GetById(string id) {
            return await _service.GetById(id);
        }

        [HttpPost]
        public async Task<UsersModel> Create(UsersModel user)
        {
             return await _service.NewUser(user);
        }

        [HttpPut]
        public async Task<UsersModel> Update(UsersModel user)
        {
            return await _service.UpdateUser(user);
        }

        [HttpPut("assingrole")]
        public async Task<UsersModel> UpdateRol(string IdRol, string IdUSer) {
            return await _service.AssingRol(IdRol, IdUSer);
        }
    }
}
