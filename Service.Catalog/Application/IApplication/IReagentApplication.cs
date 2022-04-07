using Service.Catalog.Dtos.Reagent;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Service.Catalog.Application.IApplication
{
    public interface IReagentApplication
    {
        Task<IEnumerable<ReagentListDto>> GetAll(string search = null);
        Task<ReagentFormDto> GetById(int id);
        Task Create(ReagentFormDto reagent);
        Task Update(ReagentFormDto reagent);
        Task<byte[]> ExportList(string search = null);
        Task<byte[]> ExportForm(int id);
    }
}
