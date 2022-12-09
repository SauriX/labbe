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
        private const int Convenio = 1;
        private const int Todas = 2;

        private readonly ApplicationDbContext _context;

        public ReportRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<RequestStudy>> GetByStudies(ReportFilterDto search)
        {
            var report = _context.Relación_Solicitud_Estudio
                .Include(x => x.Maquila)
                .Include(x => x.Sucursal)
                .Include(x => x.Solicitud)
                .Include(x => x.Solicitud).ThenInclude(x => x.Expediente)
                .Include(x => x.Solicitud).ThenInclude(x => x.Medico)
                .Include(x => x.Estatus)
                .Include(x => x.Solicitud).ThenInclude(x => x.Sucursal)
                .Include(x => x.Paquete)
                .Include(x => x.Solicitud).ThenInclude(x => x.Paquetes)
                .AsQueryable();

            if (search.SucursalId != null && search.SucursalId.Count > 0)
            {
                report = report.Where(x => search.SucursalId.Contains(x.Solicitud.SucursalId));
            }

            if (search.MedicoId != null && search.MedicoId.Count > 0)
            {
                report = report.Where(x => search.MedicoId.Contains(x.Solicitud.MedicoId));
            }

            if (search.Fecha != null)
            {
                report = report.
                    Where(x => x.Solicitud.Fecha.Date >= search.Fecha.First().Date && x.Solicitud.Fecha.Date <= search.Fecha.Last().Date);
            }

            return await report.ToListAsync();
        }

        public async Task<List<Request>> GetByFilter(ReportFilterDto search)
        {
            var report = _context.CAT_Solicitud
                .Include(x => x.Expediente)
                .Include(x => x.Medico)
                .Include(x => x.Estudios).ThenInclude(x => x.Estatus)
                .Include(x => x.Empresa)
                .Include(x => x.Sucursal)
                .Include(x => x.Estudios).ThenInclude(x => x.Paquete)
                .Include(x => x.Paquetes)
                .Include(x => x.Estudios).ThenInclude(x => x.Maquila)
                .Include(x => x.Estudios).ThenInclude(x => x.Sucursal)
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

            if (search.TipoCompañia != null && search.TipoCompañia.Count == 1)
            {
                if (search.TipoCompañia.Contains(Convenio))
                {
                    report = report.Where(x => x.Empresa.Convenio == 1);
                }

                else if (search.TipoCompañia.Contains(Todas))
                {
                    report = report.Where(x => x.Empresa.Convenio == 2);
                }

                query = report.ToQueryString();
            }

            if (search.TipoCompañia != null && search.TipoCompañia.Count == 2)
            {
                if (search.TipoCompañia.Contains(Convenio) && search.TipoCompañia.Contains(Todas))
                {
                    report = report.Where(x => x.Empresa.Convenio == 1 || x.Empresa.Convenio == 2);
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

        public async Task<List<RequestPayment>> GetPaymentByFilter(ReportFilterDto search)
        {
            var report = _context.CAT_Corte_Caja
                .Include(x => x.Empresa).Include(x => x.Solicitud).ThenInclude(x => x.Expediente).Include(x => x.Solicitud).ThenInclude(x => x.Sucursal).Include(x => x.Solicitud).ThenInclude(x => x.Empresa)
                .AsQueryable();

            if (search.SucursalId != null && search.SucursalId.Count > 0)
            {
                report = report.Where(x => search.SucursalId.Contains(x.Solicitud.SucursalId));

            }

            if (search.TipoCompañia != null && search.TipoCompañia.Count == 1)
            {
                if (search.TipoCompañia.Contains(Convenio))
                {
                    report = report.Where(x => x.Solicitud.Empresa.Convenio == 1);
                }

                else if (search.TipoCompañia.Contains(Todas))
                {
                    report = report.Where(x => x.Solicitud.Empresa.Convenio == 2);
                }
            }

            if (search.TipoCompañia != null && search.TipoCompañia.Count == 2)
            {
                if (search.TipoCompañia.Contains(Convenio) && search.TipoCompañia.Contains(Todas))
                {
                    report = report.Where(x => x.Solicitud.Empresa.Convenio == 1 || x.Solicitud.Empresa.Convenio == 2);
                }
            }

            if (search.FechaIndividual != DateTime.MinValue)
            {
                report = report.Where(x => x.Fecha.Date == search.FechaIndividual.Date);
            }

            if (search.Hora != null)
            {
                report = report.
                    Where(x => x.Fecha.TimeOfDay >= search.Hora.First().TimeOfDay && x.Fecha.TimeOfDay <= search.Hora.Last().TimeOfDay);
            }

            return await report.ToListAsync();
        }
    }
}
