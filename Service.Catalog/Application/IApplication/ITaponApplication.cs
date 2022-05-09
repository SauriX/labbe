using Service.Catalog.Domain.Tapon;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Service.Catalog.Application.IApplication
{
    public interface ITaponApplication
    {
        Task<IEnumerable<Tapon>> GetAll();
    }
}
