using Microsoft.EntityFrameworkCore;
using Service.MedicalRecord.Context;
using Service.MedicalRecord.Domain.RouteTracking;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Service.MedicalRecord.Repository
{
    public class RouteTrackingRepository
    {
        private readonly ApplicationDbContext _context;

        public RouteTrackingRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<RouteTracking>> GetAll() {
            var routeTrackingList = _context.Cat_PendientesDeEnviar.Include(x => x.Solicitud.Estudios).AsQueryable();

            return await routeTrackingList.ToListAsync();
        }
        public async Task<RouteTracking> getById(Guid Id) {
            var route =await  _context.Cat_PendientesDeEnviar.Include(x => x.Solicitud.Estudios).AsQueryable().FirstOrDefaultAsync(x=>x.Id==Id);
            return route;
        }
        public async Task Update(RouteTracking route) {
             _context.Update(route);
            await _context.SaveChangesAsync();
        }

        public async Task Create(RouteTracking route) {
            await _context.AddAsync(route);
            await _context.SaveChangesAsync();
        }
    }
}
