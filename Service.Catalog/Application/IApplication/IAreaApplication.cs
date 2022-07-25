using Service.Catalog.Dtos.Catalog;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Service.Catalog.Application.IApplication
{
    public interface IAreaApplication
    {
        Task<IEnumerable<AreaListDto>> GetAll(string search);
        Task<IEnumerable<AreaListDto>> GetActive();
        Task<IEnumerable<AreaListDto>> GetAreaByDepartment(int departmentId);
        Task<AreaFormDto> GetById(int id);
        Task<AreaListDto> Create(AreaFormDto Catalog);
        Task<AreaListDto> Update(AreaFormDto Catalog);
        Task<byte[]> ExportList(string search);
        Task<(byte[] file, string code)> ExportForm(int id);
    }
}
