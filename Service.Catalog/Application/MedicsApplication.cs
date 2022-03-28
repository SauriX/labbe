using Identidad.Api.Infraestructure.Repository.IRepository;
using Identidad.Api.Infraestructure.Services.IServices;
using Identidad.Api.mapper;
using Identidad.Api.ViewModels.Medicos;
using Identidad.Api.ViewModels.Menu;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Identidad.Api.Infraestructure.Services
{
    public class MedicsApplication : IMedicsApplication
    {
        private readonly IMedicsRepository _repository ;

        public MedicsApplication(IMedicsRepository repository)
        {
            _repository = repository;
        }

        public async Task<MedicsFormDto> GetById(int Id)
        {
            var Medicos = await _repository.GetById(Id);
            return Medicos.ToMedicsFormDto();
        }
        public Task<MedicsFormDto> Create(Medics CatalogoMedicos)
        {
            throw new System.NotImplementedException();
        }

        public async Task<IEnumerable<MedicsFormDto>> GetAll(string search = null)
        {
            var doctors = await _repository.GetAll(search);
            return doctors.ToMedicsFormDto();
        }
        public Task<MedicsFormDto> Update(Medics CatalogoMedicos)
        {
            throw new System.NotImplementedException();
        }
    }
}
