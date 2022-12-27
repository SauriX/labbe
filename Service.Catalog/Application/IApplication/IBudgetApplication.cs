using Service.Catalog.Dtos.Catalog;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Service.Catalog.Application.IApplication
{
    public interface IBudgetApplication
    {
        Task<IEnumerable<BudgetListDto>> GetAll(string search);
        Task<IEnumerable<BudgetListDto>> GetActive();
        Task<IEnumerable<BudgetListDto>> GetBudgetByBranch(Guid branchId);
        Task<IEnumerable<BudgetListDto>> GetBudgetsByBranch(List<Guid> branchId);
        Task<BudgetFormDto> GetById(int id);
        Task<BudgetListDto> Create(BudgetFormDto Catalog);
        Task<BudgetListDto> Update(BudgetFormDto Catalog);
        Task<byte[]> ExportList(string search);
        Task<(byte[] file, string code)> ExportForm(int id);
    }
}