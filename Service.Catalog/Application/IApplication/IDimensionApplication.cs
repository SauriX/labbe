using Service.Catalog.Dtos.Catalog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Service.Catalog.Application.IApplication
{
    public interface IDimensionApplication
    {
        Task<IEnumerable<DimensionListDto>> GetAll(string search);
        Task<IEnumerable<DimensionListDto>> GetActive();
        Task<DimensionFormDto> GetById(int id);
        Task<DimensionListDto> Create(DimensionFormDto Catalog);
        Task<DimensionListDto> Update(DimensionFormDto Catalog);
        Task<byte[]> ExportList(string search);
        Task<(byte[] file, string code)> ExportForm(int id);
    }
}
