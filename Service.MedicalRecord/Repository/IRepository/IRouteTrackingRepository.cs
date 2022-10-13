using Service.MedicalRecord.Domain.RouteTracking;
using Service.MedicalRecord.Domain.TrackingOrder;
using Service.MedicalRecord.Dtos.PendingRecive;
using Service.MedicalRecord.Dtos.RouteTracking;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Service.MedicalRecord.Repository.IRepository
{
    public interface IRouteTrackingRepository
    {
        Task<List<TrackingOrder>> GetAll(RouteTrackingSearchDto search);
        Task<TrackingOrder> getById(Guid Id);
        Task Update(RouteTracking route);
        Task Create(RouteTracking route);
        Task<List<TrackingOrder>> GetAllRecive(PendingSearchDto search);
        Task<RouteTracking> GetTracking(Guid Id);
    }
}
