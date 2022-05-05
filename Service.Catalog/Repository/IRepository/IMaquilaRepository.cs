using Service.Catalog.Domain.Maquilador;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Service.Catalog.Repository.IRepository
{
    public interface IMaquilaRepository
    {
        Task<List<Maquila>> GetAll(string search);
        Task<List<Maquila>> GetActive();
        Task<Maquila> GetById(int id);
        Task<bool> IsDuplicate(Maquila maquila);
        Task Create(Maquila maquila);
        Task Update(Maquila maquila);
    }
}
