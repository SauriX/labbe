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
            return await _service.GetAll(search);
        }
        [HttpGet("user/{id}")]
        public async Task<UserList> GetById(string id) {
            return await _service.GetById(id);
        }

        [HttpPost]
        public async Task<UserList> Create(RegisterUserDTO user, [FromHeader] string authorization)
        {
             return await _service.NewUser(user,authorization);
        }

        [HttpPost("user/clave")]
        public async Task<string> generateClave(clave data) {
            return await _service.generateClave(data);
        }
        [HttpGet("user/paswwordgenerator")]
        public async Task<string> generatePassword() {
            return await _service.generatePassword();
        }
        [HttpPut]
        public async Task<UserList> Update(RegisterUserDTO user, [FromHeader] string authorization)
        {
            return await _service.UpdateUser(user,authorization);
        }

        [HttpPut("assingrole")]
        public async Task<UsersModel> UpdateRol(string IdRol, string IdUSer) {
            return await _service.AssingRol(IdRol, IdUSer);
        }

        [HttpPut("updatepassword")]
        public async Task<UsersModel> UpdatePassword(ChangePasswordForm form)
        {
            if (form.Password.Length >= 8)
            {
                return await _service.ChangePassword(form);
            }
            else {
                return null;
            }
        }
    }
}
