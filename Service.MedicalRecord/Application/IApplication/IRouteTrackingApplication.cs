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
    }
}
