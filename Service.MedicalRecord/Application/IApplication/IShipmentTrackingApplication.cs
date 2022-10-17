using Service.MedicalRecord.Dtos.ShipmentTracking;
using Service.MedicalRecord.Dtos.TrackingOrder;
using System;
using System.Threading.Tasks;

namespace Service.MedicalRecord.Application.IApplication
{
    public interface IShipmentTrackingApplication
    {
        Task<ShipmentTrackingDto> getByid(Guid id);
        Task<TrackingOrderFormDto> getorder(Guid id);
        Task<ReciveShipmentTracking> getByidRecive(Guid id);
        Task UpdateTracking(ReciveShipmentTracking reciveShipment);
    }
}
