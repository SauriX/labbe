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
    public class AreaRepository : IAreaRepository
    {
        private readonly ApplicationDbContext _context;

        public AreaRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<Area>> GetAll(string search)
        {
            var areas = _context.CAT_Area.Include(x => x.Departamento).AsQueryable();

            search = search.Trim().ToLower();

            if (!string.IsNullOrWhiteSpace(search) && search != "all")
            {
                areas = areas.Where(x => x.Clave.ToLower().Contains(search) || x.Nombre.ToLower().Contains(search));
            }

            return await areas.ToListAsync();
        }

        public async Task<Area> GetById(int id)
        {
            var area = await _context.CAT_Area.Include(x => x.Departamento).FirstOrDefaultAsync(x => x.Id == id);

            return area;
        }

        public async Task Create(Area area)
        {
            _context.CAT_Area.Add(area);

            await _context.SaveChangesAsync();
        }

        public async Task Update(Area area)
        {
            _context.CAT_Area.Update(area);

            await _context.SaveChangesAsync();
        }
    }
}
