using Microsoft.AspNetCore.Identity;
using Service.Identity.Context;
using Service.Identity.Domain.Users;
using Service.Identity.Repository.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Service.Identity.Repository
{
    public class UserRepository<T>:IUserRepository<T> where T : UsersModel
    {
        private readonly UserManager<UsersModel> _userManager;
        private readonly SignInManager<UsersModel> _signInManager;
        IndentityContext _context;

        public List<UsersModel> ApUsers { get; private set; }
        public UsersModel ApUser { get; private set; }

        public UserRepository(
                    UserManager<UsersModel> userManager,
                    SignInManager<UsersModel> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }
        public async Task NewUser(T user) {
            await _userManager.CreateAsync(user,user.Clave);

        }
        public async Task DeleteUser(T user)
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
        public async Task<bool> UpdateUser(string id) {
            ApUsers = _userManager.Users.ToList();
            ApUser = await _userManager.FindByIdAsync(id);
            if (ApUser != null)
            {
                IdentityResult result = await _userManager.UpdateAsync(ApUser);
                if (result.Succeeded) {
                    return true;
                }
            }
            return false;
        }
        public async Task LogIn() { }
    }
}
