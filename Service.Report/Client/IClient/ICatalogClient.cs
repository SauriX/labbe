using System.Collections.Generic;
using System.Threading.Tasks;
using System;
using Service.Report.Domain.MedicalRecord;

namespace Service.Report.Client.IClient
{
    public interface ICatalogClient
    {
        Task<List<ServicesCost>> GetBudgetsByBranch(List<Guid> branchIds);
    }
}
