using Identidad.Api.ViewModels.Medicos;
using System.Collections.Generic;
using System.Threading.Tasks;
using Identidad.Api.ViewModels.Menu;

namespace Identidad.Api.Infraestructure.Services.IServices
{
    public interface IMedicsApplication
    {
        Task<MedicsFormDto> GetById(int Id);
        Task<MedicsFormDto> Create(Medics CatalogoMedicos);
        Task<MedicsFormDto> Update(Medics CatalogoMedicos);
        Task<IEnumerable<MedicsFormDto>> GetAll(string search = null);

    }
}
