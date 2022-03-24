using Service.Catalog.Domain.Catalog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Service.Catalog.Repository.IRepository
{
    public interface ICatalogDescRepository<T> where T : GenericCatalogDescription
    {
        Task<List<T>> GetAll(string search = null);
        Task<T> GetById(int id);
        Task Crete(T reagent);
        Task Update(T reagent);
    }
}
