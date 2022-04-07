using Service.Catalog.Domain.Catalog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Service.Catalog.Repository.IRepository
{
    public interface IDimensionRepository
    {
        Task<List<Dimension>> GetAll(string search = null);
        Task<Dimension> GetById(int id);
        Task Create(Dimension dimension);
        Task Update(Dimension dimension);
    }
}
