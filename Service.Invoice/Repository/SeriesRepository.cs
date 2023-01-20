using Microsoft.EntityFrameworkCore;
using Service.Billing.Context;
using Service.Billing.Domain.Series;
using Service.Billing.Repository.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Service.Catalog.Repository
{
    public class SeriesRepository : ISeriesRepository
    {
        private readonly ApplicationDbContext _context;

        public SeriesRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<Series>> GetByBranch(Guid branchId, byte type)
        {
            var series = await _context.CAT_Serie.Where(x => x.SucursalId == branchId && x.TipoSerie == type && x.Activo).ToListAsync();

            return series;
        }
    }
}
