using Service.MedicalRecord.Domain.TrackingOrder;
using System;
using System.Threading.Tasks;

namespace Service.MedicalRecord.Repository.IRepository
{
    public interface ITrackingOrderRepository
    {
        Task<TrackingOrder> GetById(Guid id);
        Task Create(TrackingOrder order);
        Task Update(TrackingOrder order);
    }
}
