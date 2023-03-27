using Service.MedicalRecord.Dtos.General;
using Service.MedicalRecord.Dtos.MassSearch;
using Service.MedicalRecord.Dtos.RelaseResult;
using Service.MedicalRecord.Dtos.RequestedStudy;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Service.MedicalRecord.Application.IApplication
{
    public interface IRelaseResultApplication
    {

        Task<int> UpdateStatus(List<RequestedStudyUpdateDto> requestDto);
        Task<List<RelaceList>> GetAll(GeneralFilterDto search);
        Task<(byte[] file, string fileName)> ExportList(GeneralFilterDto search);
        Task<byte[]> SendResultFile(DeliverResultsStudiesDto estudios);
    }
}
