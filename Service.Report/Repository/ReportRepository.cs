using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Service.Report.Context;
using Service.Report.Domain.Request;
using Service.Report.Dtos;
using Service.Report.Repository.IRepository;

namespace Service.Report.Repository
{
    public class ReportRepository : IReportRepository    {
        private readonly ApplicationDbContext _context;

        public ReportRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<List<Request>> GetAll()
        {
            var report = await _context.Request.Include(x => x.Expediente).Include(x => x.Medico).ToListAsync();

            return report;
        }

        public async Task<List<Request>> GetFilter(ReportFiltroDto search)
        {
            var report = _context.Request
                .Include(x => x.Expediente).Include(x => x.Medico)
                .AsQueryable();

            var query = report.ToQueryString();

            if (search.SucursalId != null && search.SucursalId.Count > 0)
            {
                report = report.Where(x => search.SucursalId.Contains(x.SucursalId.ToString()));
                query = report.ToQueryString();
            }
            if (search.Fecha != null)
            {
                report = report.
                    Where(x => x.Fecha.Date >= search.Fecha.First().Date &&
                    x.Fecha.Date <= search.Fecha.Last().Date);
                query = report.ToQueryString();
            }
            if (!string.IsNullOrEmpty(search.MedicoId) && Guid.TryParse(search.MedicoId, out Guid continua))
            {
                report = report.Where(x => x.MedicoId == Guid.Parse(search.MedicoId));
                query = report.ToQueryString();
            }
            return await report.ToListAsync();
        }
    }
}
