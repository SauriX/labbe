using Microsoft.EntityFrameworkCore;
using Service.Catalog.Context;
using Service.Catalog.Domain.Route;
using Service.Catalog.Repository.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Service.Catalog.Repository
{
    public class RouteRepository : IRouteRepository
    {
        private readonly ApplicationDbContext _context;

        public RouteRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<Route>> GetAll(string search)
        {
            var routes = _context.CAT_Rutas.AsQueryable();

            search = search.Trim().ToLower();

            if (!string.IsNullOrWhiteSpace(search) && search != "all")
            {
                routes = routes.Where(x => x.Clave.ToLower().Contains(search) || x.Nombre.ToLower().Contains(search));
            }

            return await routes.ToListAsync();
        }

        public async Task<List<Route>> GetActive()
        {
            var routes = await _context.CAT_Rutas.Where(x => x.Activo).ToListAsync();

            return routes;
        }

        public async Task<Route> GetById(Guid id)
        {
            var routes = await _context.CAT_Rutas.FindAsync(id);

            return routes;
        }

        public async Task<bool> IsDuplicate(Route routes)
        {
            var isDuplicate = await _context.CAT_Rutas.AnyAsync(x => x.Id != routes.Id && (x.Clave == routes.Clave || x.Nombre == routes.Nombre));

            return isDuplicate;
        }

        public async Task Create(Route routes)
        {
            _context.CAT_Rutas.Add(routes);

            await _context.SaveChangesAsync();
        }

        public async Task Update(Route routes)
        {
            _context.CAT_Rutas.Update(routes);

            await _context.SaveChangesAsync();
        }
    }
}
