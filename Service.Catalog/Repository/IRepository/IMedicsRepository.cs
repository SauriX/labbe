using Identidad.Api.ViewModels.Medicos;
using Identidad.Api.ViewModels.Menu;
using Service.Catalog.Domain.Medics;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Identidad.Api.Infraestructure.Repository.IRepository
{

    public interface IMedicsRepository
    {
        Task<Medics> GetById(int Id);
        Task Create(Medics doctors);
        Task Update(Medics Doctors);
        Task<List<Medics>> GetAll(string search = null);
        Task<Medics> GetByCode(string code);

    }

}
