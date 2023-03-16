using Service.MedicalRecord.Dtos.PendingRecive;
using Service.MedicalRecord.Dtos.RequestedStudy;
using Service.MedicalRecord.Dtos.RouteTracking;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Service.MedicalRecord.Application.IApplication
{
    public interface IRouteTrackingApplication
    {
        Task<List<RouteTrackingListDto>> GetAll(RouteTrackingSearchDto search);
        Task<(byte[] file, string fileName)> ExportForm(Guid id);
        Task<List<PendingReciveDto>> GetAllRecive(PendingSearchDto search);
        Task CreateTrackingOrder(RouteTrackingFormDto order);
        Task<List<TagTrackingOrderDto>> GetAllTags(string search);
        Task<List<TagTrackingOrderDto>> FindTags(string routeId);
        Task<byte[]> Print(PendingSearchDto search);
        Task<int> UpdateStatus(List<RequestedStudyUpdateDto> requestDto);
        Task<byte[]> ExportDeliver(Guid id);
    }
}
