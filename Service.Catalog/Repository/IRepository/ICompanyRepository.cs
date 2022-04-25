using Service.Catalog.Domain.Company;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Service.Catalog.Repository.IRepository
{
    public interface ICompanyRepository
    {
        Task<Company> GetById(int Id);
        Task<List<Company>> GetActive();
        Task Create(Company company);
        Task Update(Company company);
        Task<List<Company>> GetAll(string search = null);
        Task<string> GeneratePassword();

    }
}
