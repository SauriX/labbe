﻿using EFCore.BulkExtensions;
using Microsoft.EntityFrameworkCore;
using Service.MedicalRecord.Context;
using Service.MedicalRecord.Dictionary;
using Service.MedicalRecord.Domain.Request;
using Service.MedicalRecord.Dtos.General;
using Service.MedicalRecord.Dtos.RequestedStudy;
using Service.MedicalRecord.Repository.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Service.MedicalRecord.Repository
{
    public class SamplingRepository : ISamplingRepository
    {
        private readonly ApplicationDbContext _context;

        public SamplingRepository(ApplicationDbContext context)
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

        public async Task<List<Request>> GetAll(GeneralFilterDto search)
        {
            var report = _context.CAT_Solicitud.Where(x => x.Estudios.Count > 0)
                .OrderBy(x => x.FechaCreo)
                .Include(x => x.Expediente)
                .Include(x => x.Medico)
                .Include(x => x.Estudios).ThenInclude(x => x.Estatus)
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
            if (search.Estatus != null && search.Estatus.Count > 0)
            {
                report = report.Where(x => x.Estudios.Any(y => search.Estatus.Contains(y.EstatusId)));
            }
            if (search.Fecha != null && string.IsNullOrWhiteSpace(search.Buscar))
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
                .Where(x => x.SolicitudId == requestId && studiesIds.Contains(x.Id))
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
