using Service.Catalog.Domain.Catalog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Service.Catalog.Repository.IRepository
{
    public interface IDimensionRepository
    {
        Task<List<Dimension>> GetAll(string search);
        Task<List<Dimension>> GetActive();
        Task<Dimension> GetById(int id);
        Task<bool> IsDuplicate(Dimension area);
        Task Create(Dimension dimension);
        Task Update(Dimension dimension);
    }
}
