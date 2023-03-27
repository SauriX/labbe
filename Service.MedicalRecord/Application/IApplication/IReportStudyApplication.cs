using Service.MedicalRecord.Dtos.General;
using Service.MedicalRecord.Dtos.Request;
using Service.MedicalRecord.Dtos.RportStudy;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Service.MedicalRecord.Application.IApplication
{
    public  interface IReportStudyApplication
    {
        Task<List<ReportRequestListDto>> GetByFilter(GeneralFilterDto filter);
        Task<byte[]> ExportRequest(GeneralFilterDto filter);
        Task<(byte[] file, string fileName)> ExportList(GeneralFilterDto search);
    }
}
