using Service.Identity.Domain.Role;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Service.Identity.Repository.IRepository
{
    public interface IRoleRepository
    {
        Task<List<Role>> GetAll(string search);
        Task<List<Role>> GetActive();
        Task<Role> GetById(Guid id);
        Task<List<RolePermission>> GetPermission(Guid? id = null);
        Task<bool> IsDuplicate(Role role);
        Task Create(Role role);
        Task Update(Role role);
    }
}
