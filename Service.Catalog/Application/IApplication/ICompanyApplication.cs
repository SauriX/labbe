using Service.Catalog.Dtos.Company;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Service.Catalog.Application.IApplication
{
    public interface ICompanyApplication
    {
        Task<CompanyFormDto> GetById(int Id);
        Task<CompanyFormDto> Create(CompanyFormDto company);
        Task<CompanyFormDto> Update(CompanyFormDto company);
        Task<IEnumerable<CompanyListDto>> GetAll(string search = null);
        Task<byte[]> ExportListCompany(string search = null);
        Task<byte[]> ExportFormCompany(int id);
    }
}
