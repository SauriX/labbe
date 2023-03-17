using Service.MedicalRecord.Domain.RouteTracking;
using Service.MedicalRecord.Domain.TrackingOrder;
using Service.MedicalRecord.Dtos.PendingRecive;
using Service.MedicalRecord.Dtos.RouteTracking;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using  Service.MedicalRecord.Domain.Request;

namespace Service.MedicalRecord.Repository.IRepository
{
    public interface IRouteTrackingRepository
    {
        Task BulkUpdateStudies(Guid requestId, List<RequestStudy> studies);
        Task<List<RequestStudy>> GetStudyById(Guid requestId, IEnumerable<int> studiesIds);
        Task<Request> FindAsync(Guid id);
        Task<List<TrackingOrder>> GetAll(RouteTrackingSearchDto search);
        Task<List<RequestTag>> GetTagsByOrigin();
        Task<TrackingOrder> GetById(Guid Id);
        Task CreateOrder(TrackingOrder order);
        Task Update(RouteTracking route);
        Task Create(RouteTracking route);
        Task<List<TrackingOrder>> GetAllRecive(PendingSearchDto search);
        Task<RouteTracking> GetTracking(Guid Id);
        Task<IEnumerable<RequestTag>> GetAllTags(string search);
        Task<IEnumerable<RequestTag>> FindTags(string routeId);
        Task<IEnumerable<RequestStudy>> FindStudies(List<int> tagsId, Guid requestId);
        Task<string> GetLastCode(string date);
    }
}
