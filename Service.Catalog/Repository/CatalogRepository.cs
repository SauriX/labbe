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
            var reagents = _entity.AsQueryable();

            if (search != null)
            {
                reagents = reagents.Where(x => x.Clave == search || x.Nombre == search);
            }

            return await reagents.ToListAsync();
        }

        public async Task<T> GetById(int id)
        {
            var reagent = await _entity.FindAsync(id);

            return reagent;
        }

        public async Task Crete(T reagent)
        {
            _entity.Add(reagent);

            await _context.SaveChangesAsync();
        }

        public async Task Update(T reagent)
        {
            _entity.Update(reagent);
            await _context.SaveChangesAsync();
        }
    }
}
