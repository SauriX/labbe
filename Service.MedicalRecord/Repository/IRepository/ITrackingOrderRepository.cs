using Service.MedicalRecord.Domain.TrackingOrder;
using Service.MedicalRecord.Dtos.TrackingOrder;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Service.MedicalRecord.Repository.IRepository
{
    public interface ITrackingOrderRepository
    {
        Task Create(TrackingOrder order);
        Task<TrackingOrder> FindAsync(Guid orderId);
        Task Update(TrackingOrder order);
        Task<bool> ConfirmarRecoleccion(Guid seguimientoId);
        Task<bool> CancelarRecoleccion(Guid seguimientoId);
        Task<List<Domain.Request.RequestStudy>> FindEstudios(List<int> estudios);
    }
}
