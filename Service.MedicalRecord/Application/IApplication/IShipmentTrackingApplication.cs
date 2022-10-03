using Service.MedicalRecord.Dtos.ShipmentTracking;
using Service.MedicalRecord.Dtos.TrackingOrder;
using System;
using System.Threading.Tasks;

namespace Service.MedicalRecord.Application.IApplication
{
    public interface IShipmentTrackingApplication
    {
        Task<ShipmentTrackingDto> getByid(Guid id);
        Task<TrackingOrderDto> getorder(Guid id);
    }
}
