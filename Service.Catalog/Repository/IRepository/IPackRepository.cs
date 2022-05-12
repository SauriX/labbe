using Service.Catalog.Domain.Packet;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Service.Catalog.Repository.IRepository
{
    public interface IPackRepository
    {
        Task<Packet> GetById(int Id);
        Task<List<Packet>> GetAll(string search);
        Task Create(Packet pack);
        Task Update(Packet pack);
        Task<bool> IsDuplicate(Packet pack);
    }
}
