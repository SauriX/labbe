using Service.Catalog.Domain.Catalog;
using Service.Catalog.Dtos.Catalog;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Service.Catalog.Application.IApplication
{
    public interface ICatalogDescriptionApplication<T> where T : GenericCatalogDescription
    {
        Task<IEnumerable<CatalogDescriptionListDto>> GetAll(string search);
        Task<IEnumerable<CatalogDescriptionListDto>> GetActive();
        Task<CatalogDescriptionFormDto> GetById(int id);
        Task<CatalogDescriptionListDto> Create(CatalogDescriptionFormDto Catalog);
        Task<CatalogDescriptionListDto> Update(CatalogDescriptionFormDto Catalog);
        Task<byte[]> ExportList(string search, string catalogName);
        Task<(byte[] file, string code)> ExportForm(int id, string catalogName);
    }
}
