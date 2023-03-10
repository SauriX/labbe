using EFCore.BulkExtensions;
using Microsoft.EntityFrameworkCore;
using Service.Catalog.Context;
using Service.Catalog.Domain.Route;
using Service.Catalog.Dtos.Route;
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
            var routes = _context.CAT_Rutas
                .Include(x => x.Estudios)
                .ThenInclude(x => x.Estudio).ThenInclude(x => x.Area).ThenInclude(x => x.Departamento)
                .Include(x => x.SucursalOrigen)
                .Include(x => x.SucursalDestino)
                .Include(x => x.Paqueteria)
                .Include(x => x.Maquilador)
                .AsQueryable();

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
            var routes = await _context.CAT_Rutas
                .Include(x => x.Estudios).ThenInclude(x => x.Estudio).ThenInclude(x => x.Area).ThenInclude(x => x.Departamento)
                .Include(x => x.SucursalOrigen)
                .Include(x => x.SucursalDestino)
                .Include(x => x.Paqueteria)
                .Include(x => x.Maquilador)
                .FirstOrDefaultAsync(x => x.Id == id || x.DestinoId == id);

            return routes;
        }

        public async Task<bool> IsDuplicate(Route routes)
        {
            var isDuplicate = await _context.CAT_Rutas.AnyAsync(x => x.Id != routes.Id && (x.Clave == routes.Clave || x.Nombre == routes.Nombre));

            return isDuplicate;
        }
        public async Task<bool> IsDestinoIgualAlOrigen(Route routes)
        {
            var isDuplicate = await _context.CAT_Rutas.AnyAsync(x => x.Id != routes.Id && (routes.DestinoId == routes.OrigenId));

            return isDuplicate;
        }

        public async Task<bool> IsDestinoVacio(Route routes)
        {
            var isDuplicate = await _context.CAT_Rutas.AnyAsync(x => x.Id != routes.Id && (routes.DestinoId == null && routes.MaquiladorId == null));

            return isDuplicate;
        }

        public async Task Create(Route routes)
        {
            _context.CAT_Rutas.Add(routes);

            await _context.SaveChangesAsync();
        }

        public async Task Update(Route routes)
        {
            //_context.CAT_Rutas.Update(routes);

            //await _context.SaveChangesAsync();
            using var transaction = _context.Database.BeginTransaction();

            try
            {
                var estudios = routes.Estudios.ToList();

                routes.Estudios = null;

                _context.CAT_Rutas.Update(routes);

                await _context.SaveChangesAsync();

                var config = new BulkConfig();
                config.SetSynchronizeFilter<Route_Study>(x => x.RouteId == routes.Id);

                await _context.BulkInsertOrUpdateOrDeleteAsync(estudios, config);

                transaction.Commit();
            }
            catch (System.Exception)
            {
                transaction.Rollback();
                throw;
            }
        }

        public async Task<List<Route>> FindRoute(Route route)
        {
            
            var routes = _context.CAT_Rutas
                 .Include(x => x.Estudios)
                 .ThenInclude(x => x.Estudio)
                 .ThenInclude(x => x.Area)
                 .ThenInclude(x => x.Departamento)
                .Include(x => x.SucursalOrigen)
                .Include(x => x.SucursalDestino)
                .Include(x => x.Paqueteria)
                .Include(x => x.Maquilador)
                .AsQueryable();
            if (route.DestinoId != null && route.DestinoId != Guid.Empty)
            {
                routes = routes.Where(x => x.DestinoId == route.DestinoId);
            }
            if (route.MaquiladorId != null && route.MaquiladorId > 0)
            {
                routes = routes.Where(x => x.MaquiladorId == route.MaquiladorId);
            }

            routes = routes.Where(x => x.HoraDeRecoleccion >= route.HoraDeRecoleccion
                                    && (x.OrigenId == route.OrigenId)
                                    && (x.Lunes && route.Lunes ? true :
                                    x.Martes && route.Martes ? true :
                                    x.Miercoles && route.Miercoles ? true :
                                    x.Jueves && route.Jueves ? true :
                                    x.Viernes && route.Viernes ? true :
                                    x.Sabado && route.Sabado ? true :
                                    x.Domingo && route.Domingo ? true : false)
);

            var routesList = await routes.ToListAsync();

            //return routesList.FirstOrDefault();
            return routesList;

        }
    }
}
