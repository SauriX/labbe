using Service.Identity.Domain.Menu;
using Service.Identity.Domain.User;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Service.Identity.Repository.IRepository
{
    public interface IProfileRepository
    {
        Task<User> GetById(Guid code);
        Task<User> GetByCode(string code);
        Task<List<Menu>> GetMenu(Guid userId);
        Task<UserPermission> GetScopes(Guid userId, string controller);
    }
}
