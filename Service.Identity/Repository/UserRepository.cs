using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Service.Identity.Context;
using Service.Identity.Domain.Users;
using Service.Identity.Dtos;
using Service.Identity.Repository.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Service.Identity.Mapper;
using System.Text;
using System.Security.Claims;
using Microsoft.IdentityModel.Logging;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Http;
using System.Security.Principal;

namespace Service.Identity.Repository
{
    public class UserRepository:IUserRepository
    {
        private readonly UserManager<UsersModel> _userManager;
        private readonly SignInManager<UsersModel> _signInManager;
        private readonly IndentityContext _context;
        private readonly IConfiguration _configuration;
        public List<UsersModel> ApUsers { get; private set; }
        public UsersModel ApUser { get; private set; }

        public UserRepository(
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

        public async Task<List<UserList>> GetAll(string search)
        {
            var users = _context.Users.AsQueryable();



            if (!string.IsNullOrWhiteSpace(search) && search != "all")
            {
                search = search.Trim().ToLower();
                users = users.Where(x => x.Clave.ToLower().Contains(search) || x.Nombre.ToLower().Contains(search));
            }
            else {
                users = users.Where(x => x.Activo);
            }
             var listuser =Mapper.UserMapper.ToUserListDto(users);
            return (List<UserList>)listuser;
        }
        public async Task<UsersModel> NewUser(UsersModel user) {
            StringBuilder clave = new StringBuilder();
            clave.Append(user.Nombre.Substring(0, 1));
            clave.Append(user.PrimerApellido);
            string password = GenerarPassword(8);
            user.Id = Guid.NewGuid();
            user.IdUsuario = user.Id;
            user.Clave = clave.ToString();
            IdentityResult results= await _userManager.CreateAsync(user,password);
            ApUsers = _userManager.Users.ToList();
            ApUser = ApUsers.Last();
            if (ApUser != null)
            {
                ApUser.Contraseña = password;
                    return ApUser;
                
            }
            return null;
        }
        public async Task DeleteUser(UsersModel user)
        {
            var logins = await _userManager.GetLoginsAsync(user);
            var rolesForUser = await _userManager.GetRolesAsync(user);
            using (var transaction = _context.Database.BeginTransaction())
            {
                IdentityResult result = IdentityResult.Success;
                foreach (var login in logins)
                {
                    result = await _userManager.RemoveLoginAsync(user, login.LoginProvider, login.ProviderKey);
                    if (result != IdentityResult.Success)
                        break;
                }
                if (result == IdentityResult.Success)
                {
                    foreach (var item in rolesForUser)
                    {
                        result = await _userManager.RemoveFromRoleAsync(user, item);
                        if (result != IdentityResult.Success)
                            break;
                    }
                }
                if (result == IdentityResult.Success)
                {
                    result = await _userManager.DeleteAsync(user);
                    if (result == IdentityResult.Success)
                        transaction.Commit();
                }
            }
        }
        public async Task<UsersModel> GetById(string id) {
            ApUsers = _userManager.Users.ToList();
            ApUser = await _userManager.FindByIdAsync(id);
            if (ApUser != null)
            {
                IdentityResult result = await _userManager.UpdateAsync(ApUser);
                if (result.Succeeded)
                {
                    return ApUser;
                }
            }
            return null;
        }
        public async Task<UsersModel> UpdateUser(UsersModel user) {
            string id = user.IdUsuario.ToString();
            ApUsers = _userManager.Users.ToList();
            ApUser = await _userManager.FindByIdAsync(id);
            if (ApUser != null)
            {
                ApUser = user;
                IdentityResult result = await _userManager.UpdateAsync(ApUser);
                if (result.Succeeded) {
                    return ApUser;
                }
            }
            return null;
        }
        public async Task<UsersModel> AssingRol(string rolId,string userId) {
            ApUsers = _userManager.Users.ToList();
            ApUser = await _userManager.FindByIdAsync(userId);
            if (ApUser != null)
            {
                ApUser.IdRol = Guid.Parse(rolId);
                IdentityResult result = await _userManager.UpdateAsync(ApUser);
                if (result.Succeeded)
                {
                    return ApUser;
                }
            }
            return null;
        }
        public async Task<UsersModel> ChangePassword(ChangePasswordForm form) {
            string jwt= form.token; 
            JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();
            var securityToken = (JwtSecurityToken)tokenHandler.ReadToken(jwt);
            var claimValue = securityToken.Claims.FirstOrDefault(c => c.Type == "nameid")?.Value;


            var id = claimValue;
            if (form.Password.Contains(form.ConfirmPassword))
            {
                if (PasswordValidator(form.Password))
                {
                    var user = await _userManager.FindByIdAsync(id);

                    var token = await _userManager.GeneratePasswordResetTokenAsync(user);

                    var result = await _userManager.ResetPasswordAsync(user, token, form.Password);
                    ApUsers = _userManager.Users.ToList();
                    ApUser = await _userManager.FindByIdAsync(id);
                    if (ApUser != null)
                    {
                        ApUser.flagpassword = true;
                        IdentityResult identityResult = await _userManager.UpdateAsync(ApUser);
                        if (identityResult.Succeeded)
                        {
                            ApUsers = _userManager.Users.ToList();
                            ApUser = await _userManager.FindByIdAsync(id);
                            if (ApUser != null)
                            {
                                if (result.Succeeded)
                                {
                                    return ApUser;
                                }
                            }
                            
                        }
                    }
                    return null;
                }
                return null;
            }
            return null;
        }

        public static string GenerarPassword(int longitud)
        {
            string contraseña = string.Empty;
            string[] letras = { "a", "b", "c", "d", "e", "f", "g", "h", "i", "j", "k", "l", "m", "n", "ñ", "o", "p", "q", "r", "s", "t", "u", "v", "w", "x", "y", "z",
                                "A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M", "N", "O", "P", "Q", "R", "S", "T", "U", "V", "W", "X", "Y", "Z"};
            Random EleccionAleatoria = new Random();

            for (int i = 0; i < longitud; i++)
            {
                int LetraAleatoria = EleccionAleatoria.Next(0, 100);
                int NumeroAleatorio = EleccionAleatoria.Next(0, 9);

                if (LetraAleatoria < letras.Length)
                {
                    contraseña += letras[LetraAleatoria];
                }
                else
                {
                    contraseña += NumeroAleatorio.ToString();
                }
            }
            return contraseña;
        }

        public static bool PasswordValidator(string pass) {
            Match matchLongitud = Regex.Match(pass, @"^\w{8}\b");
           // Match matchNumeros = Regex.Match(pass, @"\d");
           // Match matchMayusculas = Regex.Match(pass, @"[A-Z]");
           // Match matchAdmin = Regex.Match(pass, @"admin");
           // Match matchContraseña = Regex.Match(pass, @"contraseña");
          // String[] palabrasProhibidas = { "123", "12345", "56789", "123456789", "321", "54321", "987654321", "56789", "qwerty", "asdf", "zxcv", "poiuy", "lkjhg", " mnbv" };
            bool errorFlag = false;
            int errorCode = 0;
            /*if (!matchNumeros.Success)
            {

                errorCode = 1;
                errorFlag = true;
            }*/
            if (errorFlag || !matchLongitud.Success)
            {

                errorCode = 2;
                errorFlag = true;
            }

            switch (errorCode)
            {
                case 1:
                    return false;
                    break;
                case 2:
                    return false;
                    break;
                default:
                    return true;
                    break;
            }
        }
    }
}
