﻿using Service.MedicalRecord.Context;
using System.Threading.Tasks;
using Service.MedicalRecord.Domain.Request;
using System;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using Service.MedicalRecord.Dictionary;
using Service.MedicalRecord.Dtos.RelaseResult;
using EFCore.BulkExtensions;
using Service.MedicalRecord.Repository.IRepository;
using Service.MedicalRecord.Dtos.General;

namespace Service.MedicalRecord.Repository
{
    public class RelaseResultRepository: IRelaseResultRepository
    {
        private readonly ApplicationDbContext _context;
        

        public RelaseResultRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<Request> FindAsync(Guid id)
        {
            var report = _context.CAT_Solicitud
                   .Include(x => x.Estudios);
            var request =  _context.CAT_Solicitud.Include(x => x.Estudios).Include(x=>x.Estatus).FirstOrDefault(x=>x.Id==id);

            return request;
        }

        public async Task<List<Request>> GetAll(GeneralFilterDto search)
        {
            var report = _context.CAT_Solicitud.Where(x => x.Estudios.Any(y => y.EstatusId == Status.RequestStudy.Liberado || y.EstatusId == Status.RequestStudy.Validado || y.EstatusId == Status.RequestStudy.Enviado))
                .Include(x => x.Expediente)
                .Include(x => x.Medico)
                .Include(x => x.Estudios).ThenInclude(x=>x.Estatus)
                .Include(x => x.Sucursal)
                .Include(x => x.Compañia)
                .AsQueryable();

            if ((string.IsNullOrWhiteSpace(search.Buscar) && (search.SucursalId == null || !search.SucursalId.Any())) && search.SucursalesId.Any())
            {
                report = report.Where(x => search.SucursalesId.Contains(x.SucursalId));
            }

            if (!string.IsNullOrEmpty(search.Buscar))
            {
                report = report.Where(x => x.Clave.Contains(search.Buscar)
                || (x.Expediente.NombrePaciente + " " + x.Expediente.PrimerApellido + " " + x.Expediente.SegundoApellido).ToLower().Contains(search.Buscar.ToLower()));
            }
            if (search.Ciudad != null && search.Ciudad.Count > 0)
            {
                report = report.Where(x => search.Ciudad.Contains(x.Sucursal.Ciudad));
            }
            if (search.SucursalId != null && search.SucursalId.Count > 0)
            {
                report = report.Where(x => search.SucursalId.Contains(x.SucursalId));
            }
            if (search.MedicoId != null && search.MedicoId.Count > 0)
            {
                report = report.Where(x => search.MedicoId.Contains(x.MedicoId));
            }
            if (search.CompañiaId != null && search.CompañiaId.Count > 0)
            {
                report = report.Where(x => search.CompañiaId.Contains(x.CompañiaId));

            }
            if (search.Fecha != null)
            {
                report = report.
                    Where(x => x.FechaCreo.Date >= search.Fecha.First().Date && x.FechaCreo.Date <= search.Fecha.Last().Date);
            }
            if (search.Estudio != null && search.Estudio.Count > 0)
            {
                report = report.Where(x => x.Estudios.Any(y => search.Estudio.Contains(y.EstudioId)));
            }
            if (search.Estatus != null && search.Estatus.Count > 0)
            {
                report = report.Where(x => x.Estudios.Any(y => search.Estatus.Contains(y.EstatusId)));
                List<Request> report2 = new List<Request>();
                foreach (var item in report)
                {
                    var estudios = item.Estudios;

                    estudios = estudios.Where(x => search.Estatus.Contains(x.EstatusId)).ToList();

                    item.Estudios = estudios;
                    report2.Add(item);
                }

                report = report2.AsQueryable();
            }


            if (search.TipoSolicitud != null && search.TipoSolicitud.Count > 0)
            {
                report = report.Where(x => search.TipoSolicitud.Contains(x.Urgencia));
            }

            if (search.Departamento != null && search.Departamento.Count > 0)
            {
                report = report.Where(x => x.Estudios.Any(y => search.Departamento.Contains(y.DepartamentoId)));
            }

            if (search.Area != null && search.Area[0] > 0)
            {
                report = report.Where(x => x.Estudios.Any(y => search.Area[0] == y.AreaId));
            }

            return report.ToList();
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

            await _context.BulkUpdateAsync(studies, config);
        }
    }
}
