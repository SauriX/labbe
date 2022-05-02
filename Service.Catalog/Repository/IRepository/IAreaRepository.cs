using Service.Catalog.Domain.Catalog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Service.Catalog.Repository.IRepository
{
    public interface IAreaRepository
    {
        Task<List<Area>> GetAll(string search);
        Task<List<Area>> GetActive();
        Task<Area> GetById(int id);
        Task<bool> IsDuplicate(Area area);
        Task Create(Area area);
        Task Update(Area area);
    }
}
