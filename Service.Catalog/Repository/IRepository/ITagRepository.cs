using Service.Catalog.Domain.Tapon;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Service.Catalog.Repository.IRepository
{
    public interface ITagRepository
    {
        Task<List<Tag>> GetAll();
    }
}
