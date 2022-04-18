using Service.Identity.Domain.Users;
using Service.Identity.Dtos;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Service.Identity.Repository.IRepository
{
    public interface IUserRepository
    {
        Task<Profile> getProfile(string token);
        Task<List<Menu>> getUserMenus(string id);
        Task<byte[]> ExportForm(string id);
        Task<byte[]> ExportList(string search = null);
        Task<List<UserList>> GetAll(string search);
        Task<UserList> GetById(string id);
        Task<UserList> NewUser(RegisterUserDTO user,string token);
        Task DeleteUser(UsersModel user);
        Task<UserList> UpdateUser(RegisterUserDTO user, string token);
        Task<UsersModel> AssingRol(string rolId, string userId);
        Task<UsersModel> ChangePassword(ChangePasswordForm form);
        Task<string> generateClave(clave data);
        Task<string> generatePassword();
    }
}
