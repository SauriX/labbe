using Service.MedicalRecord.Domain.RouteTracking;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Service.MedicalRecord.Repository.IRepository
{
    public interface IRouteTrackingRepository
    {
        Task<List<RouteTracking>> GetAll();
        Task<RouteTracking> getById(Guid Id);
        Task Update(RouteTracking route);
        Task Create(RouteTracking route);
    }
}
