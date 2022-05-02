using Service.Catalog.Dtos.Reagent;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Service.Catalog.Application.IApplication
{
    public interface IReagentApplication
    {
        Task<IEnumerable<ReagentListDto>> GetAll(string search);
        Task<ReagentFormDto> GetById(int id);
        Task Create(ReagentFormDto reagent);
        Task Update(ReagentFormDto reagent);
        Task<(byte[] file, string fileName)> ExportList(string search);
        Task<(byte[] file, string fileName)> ExportForm(int id);
    }
}
