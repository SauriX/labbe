using Microsoft.EntityFrameworkCore;
using Service.MedicalRecord.Context;
using Service.MedicalRecord.Domain.Request;
using Service.MedicalRecord.Dtos.RequestedStudy;
using Service.MedicalRecord.Repository.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EFCore.BulkExtensions;
using Service.MedicalRecord.Domain;
using Service.MedicalRecord.Dtos.ClinicResults;
using Service.MedicalRecord.Dictionary;

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
                .Include(x => x.Expediente)
                .Include(x => x.Estudios).ThenInclude(x => x.Estatus)
                .FirstOrDefault(x => x.Id == id);

            return report;
        }

        public async Task<ClinicResults> GetById(Guid id)
        {
            var request = await _context.Resultados_Clinicos
                .Include(x => x.Solicitud)
                .Include(x => x.Solicitud).ThenInclude(x => x.Expediente)
                .Include(x => x.SolicitudEstudio).ThenInclude(x => x.Estatus)
                .FirstOrDefaultAsync(x => x.Id == id);

            return request;
        }

        public async Task<List<ClinicResults>> GetByRequest(Guid requestId)
        {
            var request = await _context.Resultados_Clinicos
                .Include(x => x.Solicitud)
                .Include(x => x.Solicitud).ThenInclude(x => x.Expediente)
                .Include(x => x.SolicitudEstudio).ThenInclude(x => x.Estatus)
                .Where(x => x.SolicitudId == requestId).ToListAsync();

            return request;
        }

        public async Task<List<Request>> GetAll(ClinicResultSearchDto search)
        {
            var report = _context.CAT_Solicitud.Where(x => x.Medico != null)
                .Where(x => x.Estudios != null)
                .Where(x => x.Estudios.Any(y => y.EstatusId >= Status.RequestStudy.Solicitado && y.EstatusId < Status.RequestStudy.Cancelado))
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

        public async Task CreateLabResults(List<ClinicResults> newParameter)
        {
            await _context.BulkInsertAsync(newParameter);

        }
        public async Task UpdateLabResults(List<ClinicResults> newParameter)
        {
            var config = new BulkConfig()
            {
                PropertiesToInclude = new List<string>
                {
                    nameof(ClinicResults.Resultado),
                    nameof(ClinicResults.UltimoResultado),
                    nameof(ClinicResults.Orden),
                    nameof(ClinicResults.ValorInicial),
                    nameof(ClinicResults.ValorFinal),
                    nameof(ClinicResults.CriticoMinimo),
                    nameof(ClinicResults.CriticoMaximo),
                    nameof(ClinicResults.FCSI),
                    nameof(ClinicResults.TipoValorId),
                    nameof(ClinicResults.ObservacionesId),
                }
            };
            await _context.BulkUpdateAsync(newParameter, config);
        }


        public async Task CreateResultPathological(ClinicalResultsPathological result)
        {
            _context.Cat_Captura_ResultadosPatologicos.Add(result);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateResultPathologicalStudy(ClinicalResultsPathological result)
        {
            _context.Cat_Captura_ResultadosPatologicos.Update(result);

            await _context.SaveChangesAsync();

        }

        public async Task<ClinicalResultsPathological> GetResultPathologicalById(int id)
        {
            var resultExisting = await _context.Cat_Captura_ResultadosPatologicos
                .Where(x => x.RequestStudyId == id)
                .Include(x => x.Medico)
                .Include(x => x.SolicitudEstudio)
                .ThenInclude(x => x.EstudioWeeClinic)
                .Include(x => x.Solicitud).ThenInclude(y => y.Expediente)
                .FirstOrDefaultAsync();
            return resultExisting;
        }

        public async Task<List<ClinicalResultsPathological>> GetListResultPathologicalById(List<int> ids)
        {
            var resultExisting = await _context.Cat_Captura_ResultadosPatologicos
                .Where(x => ids.Contains(x.RequestStudyId))
                .Include(x => x.Medico)
                .Include(x => x.SolicitudEstudio)
                .ThenInclude(x => x.EstudioWeeClinic)
                .Include(x => x.Solicitud).ThenInclude(y => y.Expediente)
                .ToListAsync();
            return resultExisting;
        }

        public async Task<List<ClinicResults>> GetLabResultsById(int id)
        {
            var resultExisting = await _context.Resultados_Clinicos
                .Where(x => x.SolicitudEstudioId == id)
                .Include(x => x.SolicitudEstudio)
                .ThenInclude(x => x.EstudioWeeClinic)
                .Include(x => x.Solicitud).ThenInclude(y => y.Expediente)
                .Include(x => x.Solicitud).ThenInclude(y => y.Medico)
                .Include(x => x.Solicitud).ThenInclude(y => y.Estudios)
                .Include(x => x.Solicitud).ThenInclude(y => y.Compañia)
                .ToListAsync();
            return resultExisting;
        }

        public async Task<List<ClinicResults>> GetResultsById(Guid id)
        {
            var resultExisting = await _context.Resultados_Clinicos
                .Where(x => x.SolicitudId == id)
                .Include(x => x.SolicitudEstudio)
                .Include(x => x.Solicitud).ThenInclude(y => y.Expediente)
                .Include(x => x.Solicitud).ThenInclude(y => y.Medico)
                .Include(x => x.Solicitud).ThenInclude(y => y.Estudios)
                .Include(x => x.Solicitud).ThenInclude(y => y.Compañia)
                .ToListAsync();
            return resultExisting;
        }

        public async Task<List<Request>> GetSecondLastRequest(Guid recordId)
        {
            var resultExisting = await _context.CAT_Solicitud
                .Where(x => x.ExpedienteId == recordId)
                .Include(x => x.Estudios).ThenInclude(x => x.Resultados)
                .OrderByDescending(x => x.FechaCreo)
                .ToListAsync();
            return resultExisting;
        }

        public async Task UpdateStatusStudy(RequestStudy study)
        {
            /*_context.Relacion_Solicitud_Estudio.Update(study);

            await _context.SaveChangesAsync();*/

            var entry = _context.Entry(study);

            entry.State = EntityState.Modified;

            await _context.SaveChangesAsync();

            entry.State = EntityState.Detached;
        }
        public async Task<RequestStudy> GetStudyById(int RequestStudyId)
        {
            var studies = await _context.Relacion_Solicitud_Estudio
                .Where(x => x.Id == RequestStudyId)
                .FirstOrDefaultAsync();
            return studies;
        }

        public async Task<RequestStudy> GetRequestStudyById(int RequestStudyId)
        {
            var resuqestStudy = await _context.Relacion_Solicitud_Estudio
                .Include(x => x.Estatus)
                .Where(x => x.Id == RequestStudyId)
                .Where(x => x.EstatusId != 9)
                .FirstOrDefaultAsync();
            return resuqestStudy;

        }

        public async Task<Request> GetRequestById(Guid id)
        {
            var request = await _context.CAT_Solicitud
                .Include(x => x.Expediente)
                .Include(x => x.Sucursal)
                .Include(x => x.Compañia)
                .Include(x => x.Medico)
                .Include(x => x.Estudios.Where(x => x.PaqueteId == null))
                .ThenInclude(x => x.EstudioWeeClinic)
                .Include(x => x.Paquetes).ThenInclude(x => x.Estudios)
                .FirstOrDefaultAsync(x => x.Id == id);

            return request;
        }
        public async Task UpdateMedioSolicitado(RequestStudy study)
        {
            var entry = _context.Entry(study);

            entry.State = EntityState.Modified;

            await _context.SaveChangesAsync();

            entry.State = EntityState.Detached;
        }
        public async Task<string> GetMedioSolicitado(int RequestStudyId)
        {
            return await _context.Relacion_Solicitud_Estudio
                .Where(x => x.Id == RequestStudyId)
                .Select(x => x.MedioSolicitado)
                .FirstOrDefaultAsync();

        }
        

       
    }
}
