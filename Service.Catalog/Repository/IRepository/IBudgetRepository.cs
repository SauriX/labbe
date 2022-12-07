using Service.Catalog.Domain.Catalog;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Service.Catalog.Repository.IRepository
{
    public interface IBudgetRepository
    {
        Task<List<Budget>> GetAll(string search);
        Task<List<Budget>> GetActive();
        Task<Budget> GetById(int id);
        Task<List<Budget>> GetBudgetByBranch(Guid branchId);
        Task<bool> IsDuplicate(Budget budget);
        Task Create(Budget budget);
        Task Update(Budget budget);
        Task<IEnumerable<Budget>> GetBudgets(Guid id);

    }
}
