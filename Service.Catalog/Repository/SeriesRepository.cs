using EFCore.BulkExtensions;
using Microsoft.EntityFrameworkCore;
using Service.Catalog.Context;
using Service.Catalog.Domain.Branch;
using Service.Catalog.Domain.Series;
using Service.Catalog.Dto.Series;
using Service.Catalog.Repository.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Service.Catalog.Repository
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

        public async Task<List<Serie>> GetByFilter(SeriesFilterDto filter)
        {
            var series = _context.CAT_Serie.Include(x => x.Sucursal).AsQueryable();

            if (!string.IsNullOrEmpty(filter.Buscar))
            {
                series = series.Where(x => x.Clave == filter.Buscar);
            }

            if(filter.Ciudad != null && filter.Ciudad.Count > 0)
            {
                series = series.Where(x => filter.Ciudad.Contains(x.Ciudad));
            }

            if(filter.Sucursales != null && filter.Sucursales.Count > 0)
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

        public async Task<List<Serie>> GetByBranchType(Guid branchId, byte type)
        {
            var series = await _context.CAT_Serie.Include(x => x.Sucursal).Where(x => x.SucursalId == branchId && x.TipoSerie == type && x.Activo && x.Relacion).ToListAsync();

            return series;
        }
        
        public async Task<List<Serie>> GetByBranch(Guid branchId)
        {
            var series = await _context.CAT_Serie.Include(x => x.Sucursal).Where(x => x.SucursalId == branchId && x.Activo).ToListAsync();

            return series;
        }

        public async Task<Serie> GetById(int id, byte tipo)
        {
            var serie = await _context.CAT_Serie.Include(x => x.Sucursal).FirstOrDefaultAsync(x => x.Id == id && x.TipoSerie == tipo);

            return serie;
        }

        public async Task<List<Serie>> GetByIds(List<int> ids)
        {
            var series = await _context.CAT_Serie
                .Include(x => x.Sucursal)
                .Where(x => ids.Contains(x.Id))
                .ToListAsync();

            return series;
        }

        public async Task<List<Serie>> GetAll(Guid branchId)
        {
            var series = await _context.CAT_Serie.Include(x => x.Sucursal).Where(x => x.SucursalId == branchId).ToListAsync();

            return series;
        }

        public async Task<List<Serie>> IsSerieDuplicate(Guid branchId, List<int> ids)
        {
            var isSerieDuplicate = await _context.CAT_Serie
                .Include(x => x.Sucursal)
                .Where(x => x.SucursalId != branchId && ids.Contains(x.Id) && x.Relacion)
                .ToListAsync();

            return isSerieDuplicate;
        }

        public async Task<bool> IsDuplicate(Serie serie)
        {
            var isDuplicate = await _context.CAT_Serie.Include(x => x.Sucursal).AnyAsync(x => x.Id != serie.Id && (x.Clave == serie.Clave || x.Nombre == serie.Nombre));

            return isDuplicate;
        }

        public async Task Create(Serie serie)
        {
            _context.Add(serie);

            await _context.SaveChangesAsync();
        }

        public async Task Update(Serie serie)
        {
            _context.Update(serie);

            await _context.SaveChangesAsync();
        }

        public async Task BulkUpdate(List<Serie> series)
        {
            var config = new BulkConfig()
            {
                PropertiesToInclude = new List<string>
                {
                    nameof(Serie.SucursalId),
                    nameof(Serie.FechaModifico),
                    nameof(Serie.UsuarioModificoId)
                }
            };

            await _context.BulkUpdateAsync(series, config);
        }
    }
}
