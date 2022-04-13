using Microsoft.EntityFrameworkCore;
using Service.Catalog.Context;
using Service.Catalog.Domain.Catalog;
using Service.Catalog.Repository.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Service.Catalog.Repository
{
    public class CatalogRepository<T> : ICatalogRepository<T> where T : GenericCatalog
    {
        private readonly ApplicationDbContext _context;
        private readonly DbSet<T> _entity;

        public CatalogRepository(ApplicationDbContext context)
        {
            _context = context;
            _entity = context.Set<T>();
        }

        public async Task<List<T>> GetAll(string search = null)
        {
            var catalogs = _entity.AsQueryable();

            search = search.Trim().ToLower();

            if (!string.IsNullOrWhiteSpace(search) && search != "all")
            {
                catalogs = catalogs.Where(x => x.Clave.ToLower().Contains(search) || x.Nombre.ToLower().Contains(search));
            }

            return await catalogs.ToListAsync();
        }

        public async Task<List<T>> GetActive()
        {
            var catalogs = _entity.Where(x => x.Activo);

            return await catalogs.ToListAsync();
        }

        public async Task<T> GetById(int id)
        {
            var catalog = await _entity.FindAsync(id);

            return catalog;
        }

        public async Task<bool> IsDuplicate(T catalog)
        {
            var isDuplicate = await _entity.AnyAsync(x => x.Id != catalog.Id && (x.Clave == catalog.Clave || x.Nombre == catalog.Nombre));

            return isDuplicate;
        }

        public async Task Crete(T catalog)
        {
            _entity.Add(catalog);

            await _context.SaveChangesAsync();
        }

        public async Task Update(T catalog)
        {
            _entity.Update(catalog);
            await _context.SaveChangesAsync();
        }
    }
}
