using Service.MedicalRecord.Domain.TrackingOrder;
using Service.MedicalRecord.Dtos.TrackingOrder;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Service.MedicalRecord.Repository.IRepository
{
    public interface ITrackingOrderRepository
    {
        Task<TrackingOrder> GetById(Guid id);
        Task Create(TrackingOrder order);
        Task Update(TrackingOrder order);
        Task<List<Domain.Request.RequestStudy>> FindEstudios(List<int> estudios);
    }
}
