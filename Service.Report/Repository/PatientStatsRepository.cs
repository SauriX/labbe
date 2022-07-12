using Microsoft.EntityFrameworkCore;
using Service.Catalog.Context;
using Service.Report.Domain.PatientStats;
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

        public async Task<List<PatientStats>> GetRequestByCount()
        {
            var report = await _context.PatientStats.ToListAsync();

            return report;
        }

        public async Task<List<Report.Domain.PatientStats.PatientStats>> GetFilter(PatientStatsSearchDto search)
        {
            var report = _context.PatientStats.ToList();

            if (!String.IsNullOrEmpty(search.SucursalId))
            {
                report = report.Where(x => x.SucursalId == Guid.Parse(search.SucursalId)).ToList();
            }
            if(search.Fecha != null)
            {
                report = report.Where(x => x.Fecha.Date >= search.Fecha.First().Date).ToList();
            }

            return report.ToList();
        }
    }
}
