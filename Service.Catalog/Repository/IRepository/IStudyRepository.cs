using Service.Catalog.Domain.Study;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Service.Catalog.Repository.IRepository
{
    public interface IStudyRepository
    {
        Task<List<Study>> GetAll(string search);
        Task<List<Study>> GetActive();
        Task<Study> GetById(int id);
        Task<int> GetIdByCode(string code);
        Task<List<Study>> GetByIds(List<int> id);
        Task Create(Study study);
        Task Update(Study study);
        Task<bool> ValidateClaveNamne(string clave, string nombre, int id, int orden);
    }
}
