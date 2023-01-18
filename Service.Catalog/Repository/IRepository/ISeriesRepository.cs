using Service.Catalog.Domain.Series;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Service.Catalog.Repository.IRepository
{
    public interface ISeriesRepository
    {
        Task<List<Series>> GetByBranch(Guid branchId, byte type);
    }
}
