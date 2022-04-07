using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Service.Identity.Context;
using Service.Identity.Domain.Users;
using Service.Identity.Dtos;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using System.Text;
using System.Security.Claims;
using System;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using Service.Identity.Repository.IRepository;
using System.Linq;

namespace Service.Identity.Repository
{
    public class SessionRepository:ISessionRepository
    {
        private readonly UserManager<UsersModel> _userManager;
        private readonly SignInManager<UsersModel> _signInManager;
        private readonly IndentityContext _context;
        private readonly IConfiguration _configuration;

        public List<UsersModel> ApUsers { get; private set; }
        public UsersModel ApUser { get; private set; }

        public SessionRepository(
                 UserManager<UsersModel> userManager,
                 SignInManager<UsersModel> signInManager,
                 IndentityContext indentityContext,
                 IConfiguration configuration)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _context = indentityContext;
            _configuration = configuration;

        }
        public async Task<LoginResponse> Login(LoginDto user)
            {
            var response = new LoginResponse{};
            var result = await _signInManager.PasswordSignInAsync(user.UserName, user.Password, false, false);
            if (result.Succeeded)
            {
                ApUsers = _userManager.Users.ToList();
                ApUser = await _userManager.FindByNameAsync(user.UserName);
                if (ApUser.Activo)
                {
                    var secretKey = _configuration.GetValue<string>("SecretKey");
                    var key = Encoding.ASCII.GetBytes(secretKey);
                    var claims = new Claim[]
                    {
                    new Claim(ClaimTypes.Name, user.UserName),
                    new Claim(ClaimTypes.NameIdentifier, ApUser.Id.ToString())
                    };

                    var tokenDescriptor = new SecurityTokenDescriptor
                    {
                        Subject = new ClaimsIdentity(claims),
                        // Nuestro token va a durar un día
                        Expires = DateTime.UtcNow.AddDays(1),
                        // Credenciales para generar el token usando nuestro secretykey y el algoritmo hash 256
                        SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
                    };

                    var tokenHandler = new JwtSecurityTokenHandler();
                    var createdToken = tokenHandler.CreateToken(tokenDescriptor);




                    response.token = tokenHandler.WriteToken(createdToken);
                    response.changePassword = !ApUser.flagpassword;
                    response.id = ApUser.Id;
                  
                    return response;
                }
                response.code = 1;
                return response;
            }
            response.code = 2;
            return response;
        }
    }
}
