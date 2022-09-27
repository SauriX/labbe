using Microsoft.EntityFrameworkCore;
using Service.MedicalRecord.Context;
using Service.MedicalRecord.Domain.Request;
using Service.MedicalRecord.Dtos.RequestedStudy;
using Service.MedicalRecord.Repository.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Service.MedicalRecord.Dictionary;
using EFCore.BulkExtensions;

namespace Service.MedicalRecord.Repository
{
    public class ClinicResultsRepository : IClinicResultsRepository
    {
        private readonly ApplicationDbContext _context;

        public ClinicResultsRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Request> FindAsync(Guid id)
        {
            var report = _context.CAT_Solicitud
                   .Include(x => x.Estudios);
            var request = await _context.CAT_Solicitud.FindAsync(id);

            return request;
        }

        public async Task<List<Request>> GetAll(RequestedStudySearchDto search)
        {
            var report = _context.CAT_Solicitud
                .Include(x => x.Expediente)
                .Include(x => x.Medico)
                .Include(x => x.Estudios).ThenInclude(x => x.Estatus)
                .Include(x => x.Sucursal)
                .Include(x => x.Compañia)
                .AsQueryable();

            if (!string.IsNullOrEmpty(search.Buscar))
            {
                report = report.Where(x => x.Clave.Contains(search.Buscar)
                || (x.Expediente.NombrePaciente + " " + x.Expediente.PrimerApellido + " " + x.Expediente.SegundoApellido).ToLower().Contains(search.Buscar.ToLower()));
            }
            if (search.SucursalId != null && search.SucursalId.Count > 0)
            {
                report = report.Where(x => search.SucursalId.Contains(x.SucursalId));
            }
            if (search.MedicoId != null && search.MedicoId.Count > 0)
            {
                report = report.Where(x => search.MedicoId.Contains(x.MedicoId.ToString()));
            }
            if (search.CompañiaId != null && search.CompañiaId.Count > 0)
            {
                report = report.Where(x => search.CompañiaId.Contains(x.CompañiaId.ToString()));
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
            if (search.Estudio != null && search.Estudio.Count > 0)
            {
                report = report.Where(x => x.Estudios.Any(y => search.Estudio.Contains(y.EstudioId)));
            }


            return await report.ToListAsync();
        }

        public async Task<Request> GetById(Guid id)
        {
            var request = await _context.CAT_Solicitud
                .Include(x => x.Expediente)
                .Include(x => x.Medico)
                .Include(x => x.Estudios).ThenInclude(x => x.Estatus)
                .Include(x => x.Sucursal)
                .Include(x => x.Compañia)
               .FirstOrDefaultAsync(x => x.Id == id);

            return request;
        }

        public async Task BulkUpdateStudies(Guid requestId, List<RequestStudy> studies)
        {
            var config = new BulkConfig();
            config.SetSynchronizeFilter<RequestStudy>(x => x.SolicitudId == requestId);

            await _context.BulkUpdateAsync(studies, config);
        }

        public async Task<List<RequestStudy>> GetStudyById(Guid requestId, IEnumerable<int> studiesIds)
        {
            var studies = await _context.Relacion_Solicitud_Estudio
                .Where(x => x.SolicitudId == requestId && studiesIds.Contains(x.EstudioId))
                .ToListAsync();

            return studies;
        }

        public async Task<RequestImage> GetImage(Guid requestId, string code)
        {
            var image = await _context.Relacion_Solicitud_Imagen.FirstOrDefaultAsync(x => x.SolicitudId == requestId && x.Clave == code);

            return image;
        }

        public async Task<List<RequestImage>> GetImages(Guid requestId)
        {
            var images = await _context.Relacion_Solicitud_Imagen.Where(x => x.SolicitudId == requestId).ToListAsync();

            return images;
        }

        public Task UpdateImage(RequestImage requestImage)
        {
            throw new NotImplementedException();
        }

        public Task DeleteImage(Guid requestId, string code)
        {
            throw new NotImplementedException();
        }

        public Task Create(object newParameter)
        {
            throw new NotImplementedException();
        }

        public Task CreateResultPathological(Domain.ClinicResults.ClinicalResultsPathological result)
        {
            throw new NotImplementedException();
        }
    }
}
