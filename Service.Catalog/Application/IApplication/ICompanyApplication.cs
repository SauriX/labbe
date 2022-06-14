using Service.Catalog.Dtos.Company;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Service.Catalog.Application.IApplication
{
    public interface ICompanyApplication
    {
        Task<CompanyFormDto> GetById(Guid Id);
        Task<IEnumerable<CompanyListDto>> GetActive();
        Task<CompanyFormDto> Create(CompanyFormDto company);
        Task<CompanyFormDto> Update(CompanyFormDto company);
        Task<IEnumerable<CompanyListDto>> GetAll(string search = null);
        Task<(byte[] file, string fileName)> ExportList(string search);
        Task<(byte[] file, string fileName)> ExportForm(Guid id);
        string GeneratePassword();
    }
}
