using Microsoft.EntityFrameworkCore;
using Service.MedicalRecord.Context;
using Service.MedicalRecord.Dictionary;
using Service.MedicalRecord.Domain.Request;
using Service.MedicalRecord.Dtos.General;
using Service.MedicalRecord.Dtos.MassSearch;
using Service.MedicalRecord.Repository.IRepository;
using System;
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

        public async Task<List<Request>> GetAllCaptureResults(GeneralFilterDto filter)
        {
            var requests = _context.CAT_Solicitud
                .Include(x => x.Expediente)
                .Include(x => x.Compañia)
                .Include(x => x.Sucursal)
                .Include(x => x.Estudios).ThenInclude(x => x.Estatus)
                .AsQueryable();

            if ((string.IsNullOrWhiteSpace(filter.Buscar)) && (filter.SucursalId == null || !filter.SucursalId.Any()))
            {
                requests = requests.Where(x => filter.SucursalesId.Contains(x.SucursalId));
            }

            if (string.IsNullOrWhiteSpace(filter.Buscar) && filter.TipoFecha != null && filter.TipoFecha == 1 && filter.Fecha != null)
            {
                requests = requests.Where(x => filter.Fecha.First().Date <= x.FechaCreo.Date && filter.Fecha.Last().Date >= x.FechaCreo.Date);
            }
            if (string.IsNullOrWhiteSpace(filter.Buscar) && filter.TipoFecha != null && filter.TipoFecha == 2 && filter.Fecha != null)
            {
                requests = requests.Where(x => x.Estudios.Any(x => filter.Fecha.First().Date <= x.FechaEntrega.Date && filter.Fecha.Last().Date >= x.FechaEntrega.Date));
            }

            if (!string.IsNullOrWhiteSpace(filter.Buscar))
            {
                requests = requests.Where(x => x.Clave.ToLower().Contains(filter.Buscar)
                || x.ClavePatologica.ToLower().Contains(filter.Buscar)
                || (x.Expediente.NombrePaciente + " " + x.Expediente.PrimerApellido + " " + x.Expediente.SegundoApellido).ToLower().Contains(filter.Buscar));
            }

            if (filter.SucursalId != null && filter.SucursalId.Any())
            {
                requests = requests.Where(x => filter.SucursalId.Contains(x.SucursalId));
            }
            if (filter.Ciudad != null && filter.Ciudad.Any())
            {
                requests = requests.Where(x => filter.Ciudad.Contains(x.Sucursal.Ciudad));
            }

            if (filter.CompañiaId != null && filter.CompañiaId.Any())
            {
                requests = requests.Where(x => x.CompañiaId != null && filter.CompañiaId.Contains((Guid)x.CompañiaId));
            }

            if (filter.MedicoId != null && filter.MedicoId.Any())
            {
                requests = requests.Where(x => filter.MedicoId.Contains((Guid)x.MedicoId));
            }

            if (filter.Procedencia != null && filter.Procedencia.Any())
            {
                requests = requests.Where(x => filter.Procedencia.Contains(x.Procedencia));
            }

            if (filter.TipoSolicitud != null && filter.TipoSolicitud.Any())
            {
                requests = requests.Where(x => filter.TipoSolicitud.Contains(x.Urgencia));
            }

            if (filter.Estatus != null && filter.Estatus.Any())
            {
                requests = requests.Where(x => x.Estudios.Any(y => filter.Estatus.Contains(y.EstatusId)));
            }

            if (filter.Departamento != null && filter.Departamento.Any())
            {
                requests = requests.Where(x => x.Estudios.Any(y => filter.Departamento.Contains(y.DepartamentoId)));
            }
            if (filter.Area != null && filter.Area.Count > 0)
            {
                requests = requests.Where(x => x.Estudios.Any(y => filter.Area.Contains(y.AreaId)));
            }
            if (filter.MediosEntrega != null)
            {
                if (filter.MediosEntrega.Contains("Whatsapp"))
                {

                    requests = requests.Where(x => !string.IsNullOrEmpty(x.EnvioWhatsApp));
                }
                if (filter.MediosEntrega.Contains("Correo"))
                {

                    requests = requests.Where(x => !string.IsNullOrEmpty(x.EnvioCorreo));
                }
            }

            return await requests.ToListAsync();
        }

        public async Task<List<Request>> GetByFilter(GeneralFilterDto filter)
        {


            var requests = _context.CAT_Solicitud
                .Include(x => x.Expediente)
                .Include(x => x.Sucursal)
                .Include(x => x.Estudios).ThenInclude(x => x.Resultados)
                .AsQueryable();

            requests = requests.Where(x => x.Estudios.Any(y => y.EstatusId >= Status.RequestStudy.Capturado &&
                                                                y.EstatusId != Status.RequestStudy.Cancelado));

            if ((string.IsNullOrWhiteSpace(filter.Buscar)) && (filter.SucursalId == null || filter.SucursalId.Count() <= 0))
            {
                requests = requests.Where(x => filter.SucursalesId.Contains(x.SucursalId));
            }

            if (filter.SucursalId != null && filter.SucursalId.Any())
            {
                requests = requests.Where(x => filter.SucursalId.Contains(x.SucursalId));
            }

            if (filter.Area != null && filter.Area[0] > 0)
            {
                requests = requests.Where(x => x.Estudios.Any(y => y.AreaId == filter.Area[0]));
            }
            if (filter.Estudio != null && filter.Estudio.Count > 0)
            {
                requests = requests.Where(x => x.Estudios.Any(y => filter.Estudio.Contains(y.EstudioId)));

            }
            if (!string.IsNullOrEmpty(filter.Buscar))
            {
                requests = requests.Where(x => x.Clave.Contains(filter.Buscar)
                || (x.Expediente.NombrePaciente + " " + x.Expediente.PrimerApellido + " " + x.Expediente.SegundoApellido).ToLower().Contains(filter.Buscar.ToLower()));
            }
            if (filter.Fecha != null)
            {
                requests = requests.
                    Where(x => x.FechaCreo.Date >= filter.Fecha.First().Date && x.FechaCreo.Date <= filter.Fecha.Last().Date);
            }


            return await requests.ToListAsync();

        }
    }
}
