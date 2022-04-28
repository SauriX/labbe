using Service.Catalog.Domain.Maquilador;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Service.Catalog.Repository.IRepository
{
    public interface IMaquiladorRepository
    {
        Task<Maquilador> GetById(int Id);
        //Task<List<Maquilador>> GetActive();
        Task Create(Maquilador maqui);
        Task Update(Maquilador maqui);
        Task<List<Maquilador>> GetAll(string search = null);
        Task<bool> ValidateClaveName(string clave, string nombre);
    }
}
