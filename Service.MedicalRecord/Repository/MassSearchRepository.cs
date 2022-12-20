using Microsoft.EntityFrameworkCore;
using Service.MedicalRecord.Context;
using Service.MedicalRecord.Dictionary;
using Service.MedicalRecord.Domain.Request;
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

        public async Task<List<Request>> GetAllCaptureResults(DeliverResultsFilterDto filter)
        {
            var requests = _context.CAT_Solicitud
                .Include(x => x.Expediente)
                .Include(x => x.Compañia)
                .Include(x => x.Sucursal)
                .Include(x => x.Estudios).ThenInclude(x => x.Estatus)
                .AsQueryable();

            if (!string.IsNullOrWhiteSpace(filter.Clave))
            {
                requests = requests.Where(x => x.Clave.ToLower().Contains(filter.Clave)
                || x.ClavePatologica.ToLower().Contains(filter.Clave)
                || (x.Expediente.NombrePaciente + " " + x.Expediente.PrimerApellido + " " + x.Expediente.SegundoApellido).ToLower().Contains(filter.Clave));
            }

            if (filter.TipoFecha != null && filter.TipoFecha == 1 && filter.FechaInicial != null && filter.FechaFinal != null)
            {
                requests = requests.Where(x => ((DateTime)filter.FechaInicial).Date <= x.FechaCreo.Date && ((DateTime)filter.FechaFinal).Date >= x.FechaCreo.Date);
            }
            if (filter.TipoFecha != null && filter.TipoFecha == 2 && filter.FechaInicial != null && filter.FechaFinal != null)
            {
                requests = requests.Where(x => x.Estudios.Any(x => ((DateTime)filter.FechaInicial).Date <= x.FechaEntrega && ((DateTime)filter.FechaFinal).Date >= x.FechaEntrega));
            }
            if (filter.Sucursales != null && filter.Sucursales.Any())
            {
                requests = requests.Where(x => filter.Sucursales.Contains(x.SucursalId));
            }
            if (filter.Ciudades != null && filter.Ciudades.Any())
            {
                requests = requests.Where(x => filter.Ciudades.Contains(x.Sucursal.CiudadId));
            }

            if (filter.Companias != null && filter.Companias.Any())
            {
                requests = requests.Where(x => x.CompañiaId != null && filter.Companias.Contains((Guid)x.CompañiaId));
            }

            if (filter.Medicos != null && filter.Medicos.Any())
            {
                requests = requests.Where(x => filter.Medicos.Contains((Guid)x.MedicoId));
            }

            if (filter.Procedencias != null && filter.Procedencias.Any())
            {
                requests = requests.Where(x => filter.Procedencias.Contains(x.Procedencia));
            }

            if (filter.TipoSolicitud != null && filter.TipoSolicitud.Any())
            {
                requests = requests.Where(x => filter.TipoSolicitud.Contains(x.Urgencia));
            }

            if (filter.Estatus != null && filter.Estatus.Any())
            {
                requests = requests.Where(x => x.Estudios.Any(y => filter.Estatus.Contains(y.EstatusId)));
            }

            if (filter.Departamentos != null && filter.Departamentos.Any())
            {
                requests = requests.Where(x => x.Estudios.Any(y => filter.Departamentos.Contains(y.DepartamentoId)));
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

        public async Task<List<Request>> GetByFilter(MassSearchFilterDto filter)
        {


            var requests = _context.CAT_Solicitud
                .Include(x => x.Expediente)
                .Include(x => x.Sucursal)
                .Include(x => x.Estudios).ThenInclude(x => x.Resultados)
                //.Where(x => x.EstatusId >= Status.RequestStudy.Solicitado)
                .AsQueryable();
            
            requests = requests.Where(x => x.Estudios.Any(y => y.EstatusId >= Status.RequestStudy.Capturado && 
                                                                y.EstatusId != Status.RequestStudy.Cancelado));

            if (filter.Sucursales != null && filter.Sucursales.Any())
            {
                requests = requests.Where(x => filter.Sucursales.Contains(x.SucursalId));
            }

            if (filter.Area > 0)
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


            return await requests.ToListAsync();
            
        }
    }
}
