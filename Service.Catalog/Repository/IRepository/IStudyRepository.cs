using Service.Catalog.Domain;
using Service.Catalog.Domain.Study;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Service.Catalog.Repository.IRepository
{
    public interface IStudyRepository
    {
        Task<List<Study>> GetAll(string search);
        Task<Study> GetById(int id);
        Task Create(Study study);
        Task Update(Study study);
        Task<bool> ValidateClaveNamne(string clave, string nombre,int id, int orden);
    }
}
