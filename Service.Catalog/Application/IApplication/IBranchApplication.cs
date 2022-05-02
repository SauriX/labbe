using Service.Catalog.Dtos.Branch;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Service.Catalog.Application.IApplication
{
    public interface IBranchApplication
    {
        Task<byte[]> ExportListBranch(string search = null);
        Task<byte[]> ExportFormBranch(string id);
        Task<IEnumerable<BranchInfoDto>> GetAll(string search = null);
        Task<BranchFormDto> GetById(string Id);
        Task<bool> Create(BranchFormDto branch);
        Task<bool> Update(BranchFormDto branch);
    }
}
