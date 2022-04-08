using Identidad.Api.Infraestructure.Repository.IRepository;
using Identidad.Api.Infraestructure.Services.IServices;
using Identidad.Api.mapper;
using Identidad.Api.ViewModels.Medicos;
using Identidad.Api.ViewModels.Menu;
using Service.Catalog.Domain.Medics;
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
        private readonly IMedicsRepository _repository;

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
        public async Task<MedicsFormDto> Create(MedicsFormDto medic)
        {
            if (medic.IdMedico != 0)
            {
                throw new CustomException(HttpStatusCode.Conflict, Responses.NotPossible);
            }

            var code = await GenerateCode(medic);

            medic.Clave = code;

            var newMedics = medic.ToModel();

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

        public async Task<string> GenerateCode(MedicsFormDto medics, string suffix = null)
        {
            var code = medics.Nombre[..3];
            code += medics.PrimerApellido[..1];
            code += medics.SegundoApellido[..1];
            code += suffix;

            var exists = await _repository.GetByCode(code);

            if (exists != null)
            {
                var dotIndex = code.IndexOf(".");
                return await GenerateCode(medics, dotIndex == -1 ? "." : code[code.IndexOf(".")..] + ".");
            }

            return code;
        }
    }
}