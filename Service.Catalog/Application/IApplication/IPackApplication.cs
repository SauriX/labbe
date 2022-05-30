using Service.Catalog.Dtos.Pack;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Service.Catalog.Application.IApplication
{
    public interface IPackApplication
    {
        Task<IEnumerable<PackListDto>> GetAll(string search);
        Task<PackFormDto> GetById(int id);
        Task<PackListDto> Create(PackFormDto pack);
        Task<PackListDto> Update(PackFormDto pack);
        Task<(byte[] file, string fileName)> ExportList(string search);
        Task<(byte[] file, string fileName)> ExportForm(int id);
    }
}
