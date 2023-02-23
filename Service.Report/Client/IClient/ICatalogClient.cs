using System.Collections.Generic;
using System.Threading.Tasks;
using System;
using Service.Report.Domain.MedicalRecord;
using Service.Report.Dtos;
using Service.Report.Dtos.Indicators;
using Service.Report.Domain.Catalogs;

namespace Service.Report.Client.IClient
{
    public interface ICatalogClient
    {
        Task<List<ServicesCost>> GetBudgetsByBranch(ReportModalFilterDto search);
        Task<List<ServiceUpdateDto>> GetServiceCostByBranch(ReportModalFilterDto search);
        Task<BranchInfo> GetBranchByName(string name);
        Task CreateList(List<BudgetFormDto> bugets);
        Task UpdateService(UpdateServiceDto bugets);
    }
}
