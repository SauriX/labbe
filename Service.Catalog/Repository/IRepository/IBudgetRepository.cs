using Service.Catalog.Domain.Catalog;
using Service.Catalog.Dtos.Catalog;
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
        Task<List<BudgetBranch>> GetBudgetsById(List<Guid> ids);
        Task<List<BudgetBranch>> GetBudgetByBranch(Guid branchId);
        Task<List<BudgetBranch>> GetServiceCostByFilter(BudgetFilterDto search);
        Task<bool> IsDuplicate(Budget budget);
        Task Create(Budget budget);
        Task CreateList(List<BudgetBranch> budgets);
        Task Update(Budget budget);
        Task UpdateService(List<BudgetBranch> budgets, BudgetFilterDto filter);
        Task<IEnumerable<Budget>> GetBudgets(List<int> ids);

    }
}
