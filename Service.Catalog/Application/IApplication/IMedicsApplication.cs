using Identidad.Api.ViewModels.Medicos;
using System.Collections.Generic;
using System.Threading.Tasks;
using Identidad.Api.ViewModels.Menu;
using Service.Catalog.Dtos.Medicos;

namespace Identidad.Api.Infraestructure.Services.IServices
{
    public interface IMedicsApplication
    {
        Task<MedicsFormDto> GetById(int Id);
        Task<MedicsFormDto> Create(MedicsFormDto Medics);
        Task<MedicsFormDto> Update(MedicsFormDto medics);
        Task<IEnumerable<MedicsListDto>> GetAll(string search = null);

    }
}
