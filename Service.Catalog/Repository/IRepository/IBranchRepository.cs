using Service.Catalog.Domain.Branch;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Service.Catalog.Repository.IRepository
{
    public interface IBranchRepository
    {
        Task<List<Branch>> GetAll(string search = null);
        Task<Branch> GetById(string id);
        Task<string> GetCodeRange(Guid id);
        Task<(bool, string)> IsDuplicate(Branch branch);
        Task Create(Branch reagent);
        Task Update(Branch reagent);
        Task<List<Branch>> GetBranchByCity();
        Task<bool> isMatrizActive(Branch branch);
    }
}
