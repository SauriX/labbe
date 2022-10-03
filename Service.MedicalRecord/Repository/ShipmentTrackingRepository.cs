using Microsoft.EntityFrameworkCore;
using Service.MedicalRecord.Context;
using Service.MedicalRecord.Domain.RouteTracking;
using Service.MedicalRecord.Domain.TrackingOrder;
using Service.MedicalRecord.Repository.IRepository;
using System;
using System.Threading.Tasks;

namespace Service.MedicalRecord.Repository
{
    public class ShipmentTrackingRepository: IShipmentTrackingRepository
    {
        private readonly ApplicationDbContext _context;

        public ShipmentTrackingRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<TrackingOrder> getTrackingOrder( Guid id) {
            var TrackingOrder = await _context.CAT_Seguimiento_Ruta.Include(x => x.Estudios).ThenInclude(x => x.Solicitud.Compañia).FirstOrDefaultAsync(x=>x.Id==id);
            return TrackingOrder;
        }

        public async Task<RouteTracking> GetRouteTracking(Guid id) {
            var TrackingOrder = await _context.Cat_PendientesDeEnviar.Include(x => x.Solicitud.Compañia).Include(x => x.Solicitud.Expediente).FirstOrDefaultAsync(x => x.SegumientoId == id);
            return TrackingOrder;
        }
    }
}
