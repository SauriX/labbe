using Service.MedicalRecord.Domain.Request;
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
        Task<string> GetLastCode(Guid branchId, string date);
        Task<List<Domain.Request.RequestStudy>> FindAllEstudios(List<int> estudios, string request);
        Task<List<Domain.Request.RequestStudy>> FindStudiesRequest(string Solicitud);
        Task<TrackingOrder> FindOrderByRequestId(Guid Solicitud);
    }
}
