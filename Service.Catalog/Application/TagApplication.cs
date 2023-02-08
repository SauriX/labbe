using Service.Catalog.Application.IApplication;
using Service.Catalog.Domain.Tapon;
using Service.Catalog.Repository.IRepository;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Service.Catalog.Application
{
    public class TagApplication : ITagApplication
    {
        public readonly ITagRepository _repository;
        public TagApplication(ITagRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<Tag>> GetAll()
        {
            var tapon = await _repository.GetAll();
            return tapon;
        }
    }
}
