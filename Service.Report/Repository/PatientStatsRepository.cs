using Microsoft.EntityFrameworkCore;
using Service.Catalog.Context;
using Service.Report.Domain.Request;
using Service.Report.Dtos.PatientStats;
using Service.Report.Repository.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Service.Report.Repository
{
    public class PatientStatsRepository : IPatientStatsRepository
    {
        private readonly ApplicationDbContext _context;

        public PatientStatsRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<Request>> GetByName()
        {
            var report = await _context.Request.Include(x => x.Expediente).ToListAsync();

            return report;
        }

        public async Task<List<Report.Domain.Request.Request>> GetFilter(PatientStatsSearchDto search)
        {
            var report = _context.Request.Include(x => x.Expediente).AsQueryable();

            if (search.SucursalId != Guid.Empty)
            {
                report = report.Where(x => x.SucursalId == search.SucursalId);
            }
            if(search.Fecha != null)
            {
                report = report.Where(x => x.Fecha.Date >= search.Fecha.First().Date &&
                x.Fecha.Date <= search.Fecha.Last().Date);
            }

            return await report.ToListAsync();
        }
    }
}
