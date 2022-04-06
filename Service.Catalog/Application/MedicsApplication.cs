using Identidad.Api.Infraestructure.Repository.IRepository;
using Identidad.Api.Infraestructure.Services.IServices;
using Identidad.Api.mapper;
using Identidad.Api.ViewModels.Medicos;
using Identidad.Api.ViewModels.Menu;
using Service.Catalog.Dtos.Medicos;
using Shared.Dictionary;
using Shared.Error;
using System.Collections.Generic;
using System.Net;
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
            if (Medicos == null)
            {
                throw new CustomException(HttpStatusCode.NotFound, Responses.NotFound);
            }
            return Medicos.ToMedicsFormDto();
        }
        public async Task<MedicsFormDto> Create(MedicsFormDto medics)
        {
            if (medics.IdMedico != 0)
            {
                throw new CustomException(HttpStatusCode.Conflict, Responses.NotPossible);
            }

            var newMedics = medics.ToModel();

            await _repository.Create(newMedics);
            return newMedics.ToMedicsFormDto();
        }

        public async Task<IEnumerable<MedicsListDto>> GetAll(string search = null)
        {
            var doctors = await _repository.GetAll(search);
            return doctors.ToMedicsListDto();
        }
        public async Task<MedicsFormDto> Update(MedicsFormDto medics)
        {
            var existing = await _repository.GetById(medics.IdMedico);

            if (existing == null)
            {
                throw new CustomException(HttpStatusCode.NotFound, Responses.NotFound);
            }

            var updatedAgent = medics.ToModel(existing);

            await _repository.Update(updatedAgent);
            return existing.ToMedicsFormDto();
        }
    }
}