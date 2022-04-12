using Service.Identity.Domain.UsersRol;
using Service.Identity.Dtos;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Service.Identity.Repository.IRepository
{
    public interface IRolRepository
    {
        Task<bool> Create(RolForm rol,string token);
        Task<bool> Update(RolForm rolForm, string token);
        Task<List<RolInfo>> GetAll(string search);
        Task<RolForm> GetById(string id);
        Task<List<UserPermission>> GetPermission();
        Task<byte[]> ExportForm(string id);
        Task<byte[]> ExportList(string search = null);
    }
}
