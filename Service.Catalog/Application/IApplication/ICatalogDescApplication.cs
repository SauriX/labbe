using Service.Catalog.Domain.Catalog;
using Service.Catalog.Dtos.Catalog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Service.Catalog.Application.IApplication
{
    public interface ICatalogDescApplication<T> where T : GenericCatalogDescription
    {
        Task<IEnumerable<CatalogDescListDto>> GetAll(string search = null);
        Task<CatalogDescFormDto> GetById(int id);
        Task Create(CatalogDescFormDto Catalog);
        Task Update(CatalogDescFormDto Catalog);
    }
}
