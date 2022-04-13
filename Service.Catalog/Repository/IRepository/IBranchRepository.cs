using Service.Catalog.Domain.Branch;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Service.Catalog.Repository.IRepository
{
    public interface IBranchRepository
    {
        Task<List<Branch>> GetAll(string search = null);
        Task<Branch> GetById(int id);
        Task Create(Branch reagent);
        Task Update(Branch reagent);
    }
}
