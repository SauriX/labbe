using Service.Catalog.Domain.Indication;
using Service.Catalog.Dtos.Catalog;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Service.Catalog.Repository.IRepository
{
    public interface IIndicationRepository
    {
        Task<Indication>  GetById(int Id);
        Task<bool> IsDuplicate(Indication indication);
        Task Create(Indication indication);
        Task Update(Indication indication);
        Task<List<Indication>> GetAll(string search = null);
        Task<bool> ValidateClave(string clave);

    }
}
