using Service.Identity.Domain.User;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Service.Identity.Repository.IRepository
{
    public interface IUserRepository
    {
        Task<List<User>> GetAll(string search);
        Task<User> GetById(Guid id);
        Task<List<UserPermission>> GetPermission(Guid? id = null);
        Task<User> GetByCode(string code);
        Task<bool> IsDuplicate(User role);
        Task Create(User user);
        Task Update(User user, bool updatePermission = true);
        Task UpdateBranch(User user, bool updatePermission = true);
        Task<RequestImage> GetImage(Guid requestId, string code);
        Task UpdateImage(RequestImage requestImage);
    }
}
