using Service.Catalog.Application.IApplication;
using Service.Catalog.Domain.Tapon;
using Service.Catalog.Repository.IRepository;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Service.Catalog.Application
{
    public class TaponApplication : ITaponApplication
    {
        public readonly ITaponRepository _repository;
        public TaponApplication(ITaponRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<Tapon>> GetAll()
        {
            var tapon = await _repository.GetAll();
            return tapon;
        }
    }
}
