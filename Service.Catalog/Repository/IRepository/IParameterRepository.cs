using Service.Catalog.Domain.Parameter;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Service.Catalog.Repository.IRepository
{
    public interface IParameterRepository
    {
        Task<List<Parameters>> GetAll(string search);
        Task<Parameters> GetById(string id);
        Task Create(Parameters parameter);
        Task Update(Parameters parameter);
    }
}
