using Service.MedicalRecord.Domain.RouteTracking;
using Service.MedicalRecord.Domain.TrackingOrder;
using System;
using System.Threading.Tasks;

namespace Service.MedicalRecord.Repository.IRepository
{
    public interface IShipmentTrackingRepository
    {
        Task<RouteTracking> GetRouteTracking(Guid id);
        Task<TrackingOrder> getTrackingOrder(Guid id);
        Task updateTrackingOrder(TrackingOrder trackingOrder);
    }
}
