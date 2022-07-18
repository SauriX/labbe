using Microsoft.EntityFrameworkCore;
using Service.Catalog.Context;
using Service.Report.Domain.Request;
using Service.Report.Dtos.MedicalStats;
using Service.Report.Repository.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Service.Report.Repository
{
    public class MedicalStatsRepository : IMedicalStatsRepository
    {
        private readonly ApplicationDbContext _context;

        public MedicalStatsRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<Request>> GetByDoctor()
        {
            var report = await _context.Request.Include(x => x.Expediente).Include(x => x.Medico).ToListAsync();

            return report;
        }

        public async Task<List<Report.Domain.Request.Request>> GetFilter(MedicalStatsSearchDto search)
        {
            var report = _context.Request
                .Include(x => x.Expediente).Include(x=>x.Medico)
                .ToList();

            if (!string.IsNullOrEmpty(search.SucursalId) && Guid.TryParse(search.SucursalId, out Guid seguir))
            {
                report = report.Where(x => x.SucursalId == Guid.Parse(search.SucursalId)).ToList();
            }
            if (search.Fecha != null)
            {
                report = report.
                    Where(x => x.Fecha.Date >= search.Fecha.First().Date &&
                    x.Fecha.Date <= search.Fecha.Last().Date).ToList();
            }
            if (!string.IsNullOrEmpty(search.MedicoId) && Guid.TryParse(search.MedicoId, out Guid continua))
            {
                report = report.Where(x => x.MedicoId == Guid.Parse(search.MedicoId)).ToList();
            }
            return report.ToList();
        }
    }
}
