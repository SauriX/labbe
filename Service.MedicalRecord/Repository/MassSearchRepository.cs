using Microsoft.EntityFrameworkCore;
using Service.MedicalRecord.Context;
using Service.MedicalRecord.Dictionary;
using Service.MedicalRecord.Domain.Request;
using Service.MedicalRecord.Dtos.MassSearch;
using Service.MedicalRecord.Repository.IRepository;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading.Tasks;

namespace Service.MedicalRecord.Repository
{
    public class MassSearchRepository : IMassSearchRepository
    {
        private readonly ApplicationDbContext _context;

        public MassSearchRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public Task<List<Request>> GetByFilter(MassSearchFilterDto filter)
        {
            var requests = _context.CAT_Solicitud
                .Include(x => x.Expediente)
                .Include(x => x.Sucursal)
                .Include(x => x.Estudios).ThenInclude(x => x.Resultados)
                //.Where(x => x.EstatusId >= Status.RequestStudy.Solicitado)
                .AsQueryable();

            requests = requests.Where(x => x.Estudios.Any(y => y.EstatusId >= Status.RequestStudy.Solicitado));

            if (filter.Sucursales != null && filter.Sucursales.Any())
            {
                requests = requests.Where(x => filter.Sucursales.Contains(x.SucursalId));
            }

            if (filter.Area != null && filter.Area > 0)
            {
                requests = requests.Where(x => x.Estudios.Any(y => y.AreaId == filter.Area));
            }
            if (filter.Estudios != null && filter.Estudios.Count > 0)
            {
                requests = requests.Where(x => x.Estudios.Any(y => filter.Estudios.Contains(y.EstudioId)));
            }
            if (!string.IsNullOrEmpty(filter.Busqueda))
            {
                requests = requests.Where(x => x.Clave.Contains(filter.Busqueda)
                || (x.Expediente.NombrePaciente + " " + x.Expediente.PrimerApellido + " " + x.Expediente.SegundoApellido).ToLower().Contains(filter.Busqueda.ToLower()));
            }
            if (filter.Fechas != null)
            {
                requests = requests.
                    Where(x => x.FechaCreo.Date >= filter.Fechas.First().Date && x.FechaCreo.Date <= filter.Fechas.Last().Date);
            }


            return requests.ToListAsync();
            
        }
    }
}
