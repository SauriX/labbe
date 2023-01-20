using Service.Billing.Domain.Series;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Service.Billing.Repository.IRepository
{
    public interface ISeriesRepository
    {
        Task<List<Series>> GetByBranch(Guid branchId, byte type);
    }
}
