using Service.MedicalRecord.Domain.TrackingOrder;
using Service.MedicalRecord.Dtos.TrackingOrder;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Service.MedicalRecord.Application.IApplication
{
    public interface ITrackingOrderApplication
    {
        Task<TrackingOrderDto> Create(TrackingOrderFormDto order);
        Task Update(TrackingOrderFormDto order);
        Task<TrackingOrderCurrentDto> GetOrderById(Guid orderId);
        Task<IEnumerable<EstudiosListDto>> FindEstudios(List<int> estudios);
        Task<bool> ConfirmarRecoleccion(Guid seguimientoId);
        Task<bool> CancelarRecoleccion(Guid seguimientoId);
        Task<(byte[] file, string fileName)> ExportForm(TrackingOrderFormDto order);
    }
}
