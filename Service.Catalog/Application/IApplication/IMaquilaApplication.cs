using Service.Catalog.Dtos.Maquilador;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Service.Catalog.Application.IApplication
{
    public interface IMaquilaApplication
    {
        Task<IEnumerable<MaquilaListDto>> GetAll(string search);
        Task<IEnumerable<MaquilaListDto>> GetActive();
        Task<MaquilaFormDto> GetById(int Id);
        Task<MaquilaListDto> Create(MaquilaFormDto maquila);
        Task<MaquilaListDto> Update(MaquilaFormDto maquila);
        Task<(byte[] file, string fileName)> ExportList(string search);
        Task<(byte[] file, string fileName)> ExportForm(int id);
    }
}
