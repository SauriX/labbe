using Service.Catalog.Dtos.Maquilador;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Service.Catalog.Application.IApplication
{
    public interface IMaquiladorApplication
    {
        Task<MaquiladorFormDto> GetById(int Id);
        //Task<IEnumerable<MaquiladorListDto>> GetActive();
        Task<MaquiladorFormDto> Create(MaquiladorFormDto maqui);
        Task<MaquiladorFormDto> Update(MaquiladorFormDto maqui);
        Task<IEnumerable<MaquiladorListDto>> GetAll(string search = null);
        Task<byte[]> ExportListMaquilador(string search = null);
        Task<byte[]> ExportFormMaquilador(int id);
    }
}
