using Service.MedicalRecord.Dtos.Request;
using Service.MedicalRecord.Dtos.RportStudy;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Service.MedicalRecord.Application.IApplication
{
    public  interface IReportStudyApplication
    {
        Task<List<ReportRequestListDto>> GetByFilter(RequestFilterDto filter);
        Task<byte[]> ExportRequest(RequestFilterDto filter);
        Task<(byte[] file, string fileName)> ExportList(RequestFilterDto search);
    }
}
