﻿using Microsoft.EntityFrameworkCore;
using Service.Report.Context;
using Service.Report.Domain.Request;
using Service.Report.Dtos;
using Service.Report.Repository.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Service.Report.Repository
{
    public class ReportRepository : IReportRepository
    {
        private const int Correo = 1;
        private const int Telefono = 2;
        private const int Urgencia = 1;
        private const int UrgenciaConCargo = 2;

        private readonly ApplicationDbContext _context;

        public ReportRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<Request>> GetByFilter(ReportFilterDto search)
        {
            var report = _context.Request
                .Include(x => x.Expediente).Include(x => x.Medico).Include(x => x.Estudios).ThenInclude(x => x.Estatus).Include(x => x.Empresa)
                .AsQueryable();

            var query = report.ToQueryString();

            if (search.SucursalId != null && search.SucursalId.Count > 0)
            {
                report = report.Where(x => search.SucursalId.Contains(x.SucursalId));
                query = report.ToQueryString();
            }

            if (search.MedicoId != null && search.MedicoId.Count > 0)
            {
                report = report.Where(x => search.MedicoId.Contains(x.MedicoId));
                query = report.ToQueryString();
            }

            if (search.CompañiaId != null && search.CompañiaId.Count > 0)
            {
                report = report.Where(x => search.CompañiaId.Contains(x.EmpresaId));
                query = report.ToQueryString();
            }

            if (search.MetodoEnvio != null && search.MetodoEnvio.Count > 0)
            {
                if (search.MetodoEnvio.Contains(Correo))
                {
                    report = report.Where(x => !string.IsNullOrWhiteSpace(x.Expediente.Correo));
                }

                if (search.MetodoEnvio.Contains(Telefono))
                {
                    report = report.Where(x => !string.IsNullOrWhiteSpace(x.Expediente.Celular));
                }

                query = report.ToQueryString();
            }

            if (search.Urgencia != null && search.Urgencia.Count == 1)
            {
                if (search.Urgencia.Contains(Urgencia))
                {
                    report = report.Where(x => x.Urgencia == 2);
                }

                else if (search.Urgencia.Contains(UrgenciaConCargo))
                {
                    report = report.Where(x => x.Urgencia == 3);
                }

                query = report.ToQueryString();
            }

            if (search.Urgencia != null && search.Urgencia.Count == 2)
            {
                if (search.Urgencia.Contains(Urgencia) && search.Urgencia.Contains(UrgenciaConCargo))
                {
                    report = report.Where(x => x.Urgencia == 2 || x.Urgencia == 3);
                }
            }

            if (search.Fecha != null)
            {
                report = report.
                    Where(x => x.Fecha.Date >= search.Fecha.First().Date && x.Fecha.Date <= search.Fecha.Last().Date);
                query = report.ToQueryString();
            }

            return await report.ToListAsync();
        }
    }
}
