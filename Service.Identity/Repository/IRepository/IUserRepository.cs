using Service.Identity.Domain.Users;
using Service.Identity.Dtos;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Service.Identity.Repository.IRepository
{
    public interface IUserRepository
    {
        Task<List<UsersModel>> GetAll(string search);
        Task<UsersModel> GetById(string id);
        Task<UsersModel> NewUser(UsersModel user);
        Task DeleteUser(UsersModel user);
        Task<UsersModel> UpdateUser(UsersModel user);
        Task<UsersModel> AssingRol(string rolId, string userId);
        Task ChangePassword(string id, string pass);
    }
}
