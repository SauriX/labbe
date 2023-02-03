using Service.Catalog.Domain.Branch;
using Service.Catalog.Domain.Series;
using Service.Catalog.Dto.Series;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Service.Catalog.Repository.IRepository
{
    public interface ISeriesRepository
    {
        Task<List<Serie>> GetByFilter(SeriesFilterDto filter);
        Task<Serie> GetById(int id, byte tipo);
        Task<bool> IsDuplicate(Serie serie);
        Task Create(Serie serie);
        Task Update(Serie serie);
        Task<List<Serie>> GetByBranch(Guid branchId, byte type);
    }
}
