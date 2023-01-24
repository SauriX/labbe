using Service.Billing.Dtos.Series;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Service.Billing.Application.IApplication
{
    public interface ISeriesApplication
    {
        Task<IEnumerable<SeriesListDto>> GetByBranch(Guid branchId, byte type);
    }
}
