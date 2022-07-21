using Service.Catalog.Domain.Catalog;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Service.Catalog.Repository.IRepository
{
    public interface ICatalogRepository<T> where T : GenericCatalog
    {
        Task<List<T>> GetAll(string search);
        Task<List<T>> GetActive();
        Task<T> GetById(int id);
        Task<bool> IsDuplicate(T catalog);
        Task Create(T reagent);
        Task Update(T reagent);
    }
}
