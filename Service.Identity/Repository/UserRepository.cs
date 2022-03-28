using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Service.Identity.Context;
using Service.Identity.Domain.Users;
using Service.Identity.Dtos;
using Service.Identity.Repository.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Service.Identity.Repository
{
    public class UserRepository:IUserRepository
    {
        private readonly UserManager<UsersModel> _userManager;
        private readonly SignInManager<UsersModel> _signInManager;
        private readonly IndentityContext _context;

        public List<UsersModel> ApUsers { get; private set; }
        public UsersModel ApUser { get; private set; }

        public UserRepository(
                    UserManager<UsersModel> userManager,
                    SignInManager<UsersModel> signInManager,
                    IndentityContext indentityContext)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _context = indentityContext;
        }

        public async Task<List<UsersModel>> GetAll(string search)
        {
            var users = _context.Users.AsQueryable();

            search = search.Trim().ToLower();

            if (!string.IsNullOrWhiteSpace(search) && search != "all")
            {
                users = users.Where(x => x.Clave.ToLower().Contains(search) || x.Nombre.ToLower().Contains(search));
            }

            return await users.ToListAsync();
        }
        public async Task<UsersModel> NewUser(UsersModel user) {

            string password = GenerarPassword(8);
            await _userManager.CreateAsync(user,password);
            ApUsers = _userManager.Users.ToList();
            ApUser = await _userManager.Users.LastAsync();
            if (ApUser != null)
            {
                IdentityResult result = await _userManager.UpdateAsync(ApUser);
                if (result.Succeeded)
                {
                    ApUser.Contraseña = password;
                    return ApUser;
                }
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
        public async Task ChangePassword(string id,string pass) {
            var user = await _userManager.FindByIdAsync(id);

            var token = await _userManager.GeneratePasswordResetTokenAsync(user);

            var result = await _userManager.ResetPasswordAsync(user, token, pass);
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
    }
}
