using EFCore.BulkExtensions;
using Microsoft.EntityFrameworkCore;
using Service.MedicalRecord.Context;
using Service.MedicalRecord.Dictionary;
using Service.MedicalRecord.Domain.Request;
using Service.MedicalRecord.Dtos.RequestedStudy;
using Service.MedicalRecord.Dtos.Sampling;
using Service.MedicalRecord.Repository.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Service.MedicalRecord.Repository
{
    public class RequestedStudyRepository : IRequestedStudyRepository
    {
        private readonly ApplicationDbContext _context;

        public RequestedStudyRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Request> FindAsync(Guid id)
        {
            var request = await _context.CAT_Solicitud.FindAsync(id);

            return request;
        }

        public async Task<List<Request>> GetAll(RequestedStudySearchDto search)
        {
            var report = _context.CAT_Solicitud
                .Include(x => x.Expediente)
                .Include(x => x.Medico)
                .Include(x => x.Estudios)
                .Include(x => x.Sucursal)
                .Include(x => x.Compañia)
                .AsQueryable();

            if (!string.IsNullOrEmpty(search.Buscar))
            {
                report = report.Where(x => x.Clave.Contains(search.Buscar) 
                || x.Expediente.NombreCompleto.ToLower().Contains(search.Buscar.ToLower()));
            }
            if (search.SucursalId != null && search.SucursalId.Count > 0)
            {
                report = report.Where(x => search.SucursalId.Contains(x.SucursalId));
            }
            if (search.MedicoId != null && search.MedicoId.Count > 0)
            {
                report = report.Where(x => search.MedicoId.Equals(x.MedicoId));;
            }
            if (search.CompañiaId != null && search.CompañiaId.Count > 0)
            {
                report = report.Where(x => search.CompañiaId.Equals(x.CompañiaId));
            }
            if (search.Estatus != null && search.Estatus.Count > 0)
            {
                report = report.Where(x => x.Estudios.Any(y => search.Estatus.Contains(y.EstatusId)));
            }
            if (search.Fecha != null)
            {
                report = report.
                    Where(x => x.FechaCreo.Date >= search.Fecha.First().Date && x.FechaCreo.Date <= search.Fecha.Last().Date);
            }
            if (search.Procedencia != null && search.Procedencia.Count > 0)
            {
                report = report.Where(x => search.Procedencia.Contains(x.Procedencia));
            }
            if (search.TipoSolicitud != null && search.TipoSolicitud.Count > 0)
            {
                report = report.Where(x => search.TipoSolicitud.Contains(x.Urgencia));
            }
            if (search.Departamento != null && search.Departamento.Count > 0)
            {
                report = report.Where(x => x.Estudios.Any(y => search.Departamento.Contains(y.DepartamentoId)));
            }
            if (search.Area != null && search.Area.Count > 0)
            {
                report = report.Where(x => x.Estudios.Any(y => search.Area.Contains(y.AreaId)));
            }

            return await report.ToListAsync();
        }

        public async Task<List<RequestStudy>> GetStudyById(Guid requestId, IEnumerable<int> studiesIds)
        {
            var studies = await _context.Relacion_Solicitud_Estudio
                .Where(x => x.SolicitudId == requestId && studiesIds.Contains(x.EstudioId))
                .ToListAsync();

            return studies;
        }

        public async Task BulkUpdateStudies(Guid requestId, List<RequestStudy> studies)
        {
            var config = new BulkConfig();
            config.SetSynchronizeFilter<RequestStudy>(x => x.SolicitudId == requestId);

            await _context.BulkInsertOrUpdateAsync(studies, config);
        }

        public async Task UpdateStatus(UpdateDto status)
        {
            var data = status.Id.ToArray();

            foreach(var id in data)
            {
                var studio = await _context.Relacion_Solicitud_Estudio.FirstOrDefaultAsync(x => x.EstudioId == id);
                if (studio.EstatusId == 2)
                {
                    studio.EstatusId = Status.RequestStudy.Solicitado;
                }
                else
                {
                    if (studio.EstatusId == 3)
                    {
                        studio.EstatusId = Status.RequestStudy.TomaDeMuestra;
                    }
                }

                _context.Update(studio);
                await _context.SaveChangesAsync();
            }
        }
    }
}
