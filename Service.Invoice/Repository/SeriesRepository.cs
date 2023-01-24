using Microsoft.EntityFrameworkCore;
using Service.Billing.Context;
using Service.Billing.Domain.Series;
using Service.Billing.Dto.Series;
using Service.Billing.Repository.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Service.Billing.Repository
{
    public class SeriesRepository : ISeriesRepository
    {
        private readonly ApplicationDbContext _context;
        private const int Factura = 1;
        private const int Recibo = 2;

        public SeriesRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<Series>> GetByFilter(SeriesFilterDto filter)
        {
            var series = _context.CAT_Serie.AsQueryable();

            if (!string.IsNullOrEmpty(filter.Buscar))
            {
                series = series.Where(x => x.Clave == filter.Buscar);
            }

            if(filter.Ciudad.Count > 0 || filter.Ciudad != null)
            {
                series = series.Where(x => filter.Ciudad.Contains(x.Ciudad));
            }

            if(filter.Sucursales.Count > 0 || filter.Sucursales != null)
            {
                series = series.Where(x => filter.Sucursales.Contains(x.SucursalId));
            }

            if (filter.Año != DateTime.MinValue)
            {
                series = series.Where(x => x.FechaCreo.Year == filter.Año.Year);
            }

            if (filter.TipoSeries != null && filter.TipoSeries.Count == 1)
            {
                if (filter.TipoSeries.Contains(Factura))
                {
                    series = series.Where(x => x.TipoSerie == 1);
                }

                else if (filter.TipoSeries.Contains(Recibo))
                {
                    series = series.Where(x => x.TipoSerie == 2);
                }

            }

            if (filter.TipoSeries != null && filter.TipoSeries.Count == 2)
            {
                if (filter.TipoSeries.Contains(Factura) && filter.TipoSeries.Contains(Recibo))
                {
                    series = series.Where(x => x.TipoSerie == 1 || x.TipoSerie == 2);
                }
            }

            return await series.ToListAsync();
        }

        public async Task<List<Series>> GetByBranch(Guid branchId, byte type)
        {
            var series = await _context.CAT_Serie.Where(x => x.SucursalId == branchId && x.TipoSerie == type && x.Activo).ToListAsync();

            return series;
        }
    }
}
