using Service.Identity.Dtos.Role;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Service.Identity.Application.IApplication
{
    public interface IRoleApplication
    {
        Task<IEnumerable<RoleListDto>> GetAll(string search);
        Task<IEnumerable<RoleListDto>> GetActive();
        Task<RoleFormDto> GetById(string id);
        Task<IEnumerable<RolePermissionDto>> GetPermission(string id = null);
        Task<RoleListDto> Create(RoleFormDto role);
        Task<RoleListDto> Update(RoleFormDto role);
        Task<(byte[] file, string fileName)> ExportForm(string id);
        Task<(byte[] file, string fileName)> ExportList(string search = null);
    }
}
