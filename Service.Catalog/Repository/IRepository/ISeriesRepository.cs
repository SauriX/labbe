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
        Task<List<Serie>> GetByIds(List<int> ids);
        Task<List<Serie>> GetAll(Guid branchId);
        Task<List<Serie>> IsSerieDuplicate(Guid branchId, List<int> ids);
        Task<bool> IsDuplicate(Serie serie);
        Task Create(Serie serie);
        Task Update(Serie serie);
        Task BulkUpdate(List<Serie> series);
        Task<List<Serie>> GetByBranchType(Guid branchId, byte type);
        Task<List<Serie>> GetByBranch(Guid branchId);
    }
}
