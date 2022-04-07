using Service.Catalog.Dtos.Catalog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Service.Catalog.Application.IApplication
{
    public interface IAreaApplication
    {
        Task<IEnumerable<AreaListDto>> GetAll(string search = null);
        Task<AreaFormDto> GetById(int id);
        Task<AreaListDto> Create(AreaFormDto Catalog);
        Task<AreaListDto> Update(AreaFormDto Catalog);
    }
}
