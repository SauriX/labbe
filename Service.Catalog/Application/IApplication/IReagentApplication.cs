using Service.Catalog.Dtos.Reagent;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Service.Catalog.Application.IApplication
{
    public interface IReagentApplication
    {
        Task<IEnumerable<ReagentListDto>> GetAll(string search);
        Task<IEnumerable<ReagentListDto>> GetActive();
        Task<ReagentFormDto> GetById(string id);
        Task<ReagentListDto> Create(ReagentFormDto reagent);
        Task<ReagentListDto> Update(ReagentFormDto reagent);
        Task<(byte[] file, string fileName)> ExportList(string search);
        Task<(byte[] file, string fileName)> ExportForm(string id);
    }
}
