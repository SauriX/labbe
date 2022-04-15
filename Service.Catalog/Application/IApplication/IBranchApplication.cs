using Service.Catalog.Dtos.Branch;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Service.Catalog.Application.IApplication
{
    public interface IBranchApplication
    {
        Task<byte[]> ExportListBranch(string search = null);
        Task<byte[]> ExportFormBranch(string id);
        Task<IEnumerable<BranchInfo>> GetAll(string search = null);
        Task<BranchForm> GetById(string Id);
        Task<bool> Create(BranchForm branch);
        Task<bool> Update(BranchForm branch);
    }
}
