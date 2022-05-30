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

        public async Task<List<Area>> GetActive()
        {
            var areas = await _context.CAT_Area.Include(x => x.Departamento).Where(x => x.Activo).ToListAsync();

            return areas;
        }

        public async Task<List<Area>> GetAreaByDepartment(int departmentId)
        {
            var areas = await _context.CAT_Area.Include(x => x.Departamento).Where(x => x.DepartamentoId == departmentId && x.Activo).ToListAsync();

            return areas;
        }

        public async Task<Area> GetById(int id)
        {
            var area = await _context.CAT_Area.Include(x => x.Departamento).FirstOrDefaultAsync(x => x.Id == id);

            return area;
        }

        public async Task<IEnumerable<Area>> GetAreas(int id)
        {
            var catalog = await _context.CAT_Area.Where(x => x.DepartamentoId == id && x.Activo).ToListAsync();

            return catalog;
        }

        public async Task<bool> IsDuplicate(Area catalog)
        {
            var isDuplicate = await _context.CAT_Area.AnyAsync(x => x.Id != catalog.Id && (x.Clave == catalog.Clave || x.Nombre == catalog.Nombre));

            return isDuplicate;
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
