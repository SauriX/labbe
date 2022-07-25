using Service.Identity.Dtos.Profile;
using Service.Identity.Dtos.User;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Service.Identity.Application.IApplication
{
    public interface IUserApplication
    {
        Task<IEnumerable<UserListDto>> GetAll(string search);
        Task<UserFormDto> GetById(string id);
        string GeneratePassword();
        Task<IEnumerable<UserPermissionDto>> GetPermission();
        Task<string> GenerateCode(UserCodeDto data, string suffix = null);
        Task<UserListDto> Create(UserFormDto user);
        Task<UserListDto> Update(UserFormDto user);
        Task UpdatePassword(ChangePasswordFormDto data);
        Task<(byte[] file, string fileName)> ExportForm(string id);
        Task<(byte[] file, string fileName)> ExportList(string search = null);
    }
}


//Task<Profile> getProfile(string token);
//Task<List<MenuDto>> getUserMenus(string id);
//Task<byte[]> ExportForm(string id);
//Task<byte[]> ExportList(string search = null);
//Task<List<UserListDto>> GetAll(string search);
//Task<UserListDto> GetById(string id);
//Task<UserListDto> Create(UserFormDto user, string token);
//Task DeleteUser(User user);
//Task<UserListDto> UpdateUser(UserFormDto user, string token);
//Task<User> AssingRol(string rolId, string userId);
//Task<User> ChangePassword(ChangePasswordFormDto form);
//Task<string> generateClave(clave data);
//Task<string> generatePassword();