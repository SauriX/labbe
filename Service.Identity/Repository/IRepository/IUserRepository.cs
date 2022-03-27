using Service.Identity.Domain.Users;
using Service.Identity.Dtos;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Service.Identity.Repository.IRepository
{
    public interface IUserRepository
    {
        Task<List<UsersModel>> GetAll(string search);
        Task<string> NewUser(UsersModel user);
        Task DeleteUser(UsersModel user);
        Task<bool> UpdateUser(UsersModel user);

    }
}
