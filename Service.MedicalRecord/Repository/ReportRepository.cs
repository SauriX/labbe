using Service.MedicalRecord.Context;
using Service.MedicalRecord.Domain.Request;
using Service.MedicalRecord.Dtos.Reports;
using Service.MedicalRecord.Repository.IRepository;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using System;
using Microsoft.EntityFrameworkCore;
using Service.MedicalRecord.Domain.Quotation;

namespace Service.MedicalRecord.Repository
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

        public async Task<List<Request>> GetByFilter(ReportFilterDto search)
        {
            var report = _context.CAT_Solicitud
                .Include(x => x.Expediente)
                .Include(x => x.Medico)
                .Include(x => x.Estudios).ThenInclude(x => x.Estatus)
                .Include(x => x.Estudios).ThenInclude(x => x.Paquete)
                .Include(x => x.Estudios).ThenInclude(x => x.Maquila)
                .Include(x => x.Estatus)
                .Include(x => x.Compañia)
                .Include(x => x.Sucursal)
                .Include(x => x.Paquetes)
                .AsQueryable();

            var query = report.ToQueryString();
            
            if (search.Ciudad != null && search.Ciudad.Count > 0)
            {
                report = report.Where(x => search.Ciudad.Contains(x.Sucursal.Ciudad));
            }

            if (search.SucursalId != null && search.SucursalId.Count > 0)
            {
                report = report.Where(x => search.SucursalId.Contains(x.SucursalId));
                query = report.ToQueryString();
            }

            if (search.MedicoId != null && search.MedicoId.Count > 0)
            {
                report = report.Where(x => search.MedicoId.Contains((Guid)x.MedicoId!));
                query = report.ToQueryString();
            }

            if (search.CompañiaId != null && search.CompañiaId.Count > 0)
            {
                report = report.Where(x => search.CompañiaId.Contains((Guid)x.CompañiaId));
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
                    report = report.Where(x => x.Procedencia == 1);
                }

                else if (search.TipoCompañia.Contains(Todas))
                {
                    report = report.Where(x => x.Procedencia == 2);
                }

                query = report.ToQueryString();
            }

            if (search.TipoCompañia != null && search.TipoCompañia.Count == 2)
            {
                if (search.TipoCompañia.Contains(Convenio) && search.TipoCompañia.Contains(Todas))
                {
                    report = report.Where(x => x.Procedencia == 1 || x.Procedencia == 2);
                }
            }

            if (search.FechaInicial != DateTime.MinValue && search.FechaFinal != DateTime.MinValue)
            {
                report = report.
                    Where(x => x.FechaCreo.Date >= search.FechaInicial.Date && x.FechaCreo.Date <= search.FechaFinal.Date);
                query = report.ToQueryString();
            }

            if (search.Fecha != null)
            {
                report = report.
                    Where(x => x.FechaCreo.Date >= search.Fecha.First().Date && x.FechaCreo.Date <= search.Fecha.Last().Date);
                query = report.ToQueryString();
            }

            return await report.ToListAsync();
        }

        public async Task<List<RequestStudy>> GetByStudies(ReportFilterDto search)
        {
            var report = _context.Relacion_Solicitud_Estudio
                .Include(x => x.Maquila)
                .Include(x => x.Solicitud)
                .Include(x => x.Solicitud).ThenInclude(x => x.Expediente)
                .Include(x => x.Solicitud).ThenInclude(x => x.Medico)
                .Include(x => x.Solicitud).ThenInclude(x => x.Sucursal)
                .Include(x => x.Estatus)
                .Include(x => x.Paquete)
                .AsQueryable();

            if (search.Ciudad != null && search.Ciudad.Count > 0)
            {
                report = report.Where(x => search.Ciudad.Contains(x.Solicitud.Sucursal.Ciudad));
            }

            if (search.SucursalId != null && search.SucursalId.Count > 0)
            {
                report = report.Where(x => search.SucursalId.Contains(x.Solicitud.SucursalId));
            }

            if (search.MedicoId != null && search.MedicoId.Count > 0)
            {
                report = report.Where(x => search.MedicoId.Contains((Guid)x.Solicitud.MedicoId));
            }

            if (search.Fecha != null)
            {
                report = report.
                    Where(x => x.Solicitud.FechaCreo.Date >= search.Fecha.First().Date && x.Solicitud.FechaCreo.Date <= search.Fecha.Last().Date);
            }

            return await report.ToListAsync();
        }

        public async Task<List<Quotation>> GetByQuotation(ReportFilterDto search)
        {
            var report = _context.CAT_Cotizacion
                .Include(x => x.Expediente)
                .Include(x => x.Medico)
                .Include(x => x.Compañia)
                .Include(x => x.Estudios)
                .Include(x => x.Sucursal)
                .Include(x => x.Estudios).ThenInclude(x => x.Paquete)
                .Include(x => x.Paquetes)
                .AsQueryable();

            var query = report.ToQueryString();

            if (search.Ciudad != null && search.Ciudad.Count > 0)
            {
                report = report.Where(x => search.Ciudad.Contains(x.Sucursal.Ciudad));
                query = report.ToQueryString();
            }

            if (search.SucursalId != null && search.SucursalId.Count > 0)
            {
                report = report.Where(x => search.SucursalId.Contains(x.SucursalId));
                query = report.ToQueryString();
            }

            if (search.MedicoId != null && search.MedicoId.Count > 0)
            {
                report = report.Where(x => search.MedicoId.Contains((Guid)x.MedicoId!));
                query = report.ToQueryString();
            }

            if (search.CompañiaId != null && search.CompañiaId.Count > 0)
            {
                report = report.Where(x => search.CompañiaId.Contains((Guid)x.CompañiaId));
                query = report.ToQueryString();
            }

            if (search.Fecha != null)
            {
                report = report.
                    Where(x => x.FechaCreo.Date >= search.Fecha.First().Date && x.FechaCreo.Date <= search.Fecha.Last().Date);
                query = report.ToQueryString();
            }

            return await report.ToListAsync();
        }
    }
}
