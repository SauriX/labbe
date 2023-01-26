using Service.Billing.Domain.Series;
using Service.Billing.Dto.Series;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Service.Billing.Repository.IRepository
{
    public interface ISeriesRepository
    {
        Task<List<Series>> GetByFilter(SeriesFilterDto filter);
        Task<Series> GetById(int id);
        Task Create(Series serie);
        Task Update(Series serie);
        Task<List<Series>> GetByBranch(Guid branchId, byte type);
    }
}
