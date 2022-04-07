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
    public class DimensionRepository : IDimensionRepository
    {
        private readonly ApplicationDbContext _context;

        public DimensionRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<Dimension>> GetAll(string search)
        {
            var dimensions = _context.CAT_Dimension.AsQueryable();

            search = search.Trim().ToLower();

            if (!string.IsNullOrWhiteSpace(search) && search != "all")
            {
                dimensions = dimensions.Where(x => x.Clave.ToLower().Contains(search));
            }

            return await dimensions.ToListAsync();
        }

        public async Task<Dimension> GetById(int id)
        {
            var dimension = await _context.CAT_Dimension.FindAsync(id);

            return dimension;
        }

        public async Task Create(Dimension dimension)
        {
            _context.CAT_Dimension.Add(dimension);

            await _context.SaveChangesAsync();
        }

        public async Task Update(Dimension dimension)
        {
            _context.CAT_Dimension.Update(dimension);

            await _context.SaveChangesAsync();
        }
    }
}
