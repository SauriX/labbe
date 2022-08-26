using Service.MedicalRecord.Dtos.TrackingOrder;
using System;
using System.Threading.Tasks;

namespace Service.MedicalRecord.Application.IApplication
{
    public interface ITrackingOrderApplication
    {
        Task<TrackingOrderDto> GetTrackingOrder(TrackingOrderFormDto order); 
        Task<TrackingOrderDto> GetById(Guid Id);
        Task<TrackingOrderDto> Create(TrackingOrderFormDto order);
        Task<TrackingOrderDto> Update(TrackingOrderFormDto order);
    }
}
