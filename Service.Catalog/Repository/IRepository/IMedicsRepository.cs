using Identidad.Api.ViewModels.Menu;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Identidad.Api.Infraestructure.Repository.IRepository
{

    public interface IMedicsRepository
    {
        Task<Medics> GetById(int Id);
        Task Create(Medics Doctors);
        Task Update(Medics Doctors);
        Task<List<Medics>> GetAll(string search = null);

    }

}
