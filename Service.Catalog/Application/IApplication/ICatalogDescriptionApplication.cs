using Service.Catalog.Domain.Catalog;
using Service.Catalog.Dtos.Catalog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Service.Catalog.Application.IApplication
{
    public interface ICatalogDescriptionApplication<T> where T : GenericCatalogDescription
    {
        Task<IEnumerable<CatalogDescriptionListDto>> GetAll(string search = null);
        Task<CatalogDescriptionFormDto> GetById(int id);
        Task<CatalogDescriptionListDto> Create(CatalogDescriptionFormDto Catalog);
        Task<CatalogDescriptionListDto> Update(CatalogDescriptionFormDto Catalog);
        Task<byte[]> ExportListIndication(string search = null);
        Task<byte[]> ExportFormIndication(int id);
    }
}
