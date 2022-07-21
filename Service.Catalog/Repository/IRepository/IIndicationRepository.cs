using Service.Catalog.Domain.Indication;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Service.Catalog.Repository.IRepository
{
    public interface IIndicationRepository
    {
        Task<List<Indication>> GetAll(string search);
        Task<Indication> GetById(int Id);
        Task<bool> IsDuplicate(Indication indication);
        Task Create(Indication indication);
        Task Update(Indication indication);
    }
}
