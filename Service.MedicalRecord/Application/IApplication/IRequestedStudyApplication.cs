using Service.MedicalRecord.Dtos.Request;
using Service.MedicalRecord.Dtos.RequestedStudy;
using Service.MedicalRecord.Dtos.Sampling;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Service.MedicalRecord.Application.IApplication
{
    public interface IRequestedStudyApplication
    {
        Task<int> UpdateStatus(RequestStudyUpdateDto requestDto);
        Task<List<SamplingListDto>> GetAll(RequestedStudySearchDto search);
        Task<(byte[] file, string fileName)> ExportList(RequestedStudySearchDto search = null);
    }
}
