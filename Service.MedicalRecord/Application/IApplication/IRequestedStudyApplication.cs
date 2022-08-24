using Service.MedicalRecord.Dtos.RequestedStudy;
using Service.MedicalRecord.Dtos.Sampling;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Service.MedicalRecord.Application.IApplication
{
    public interface IRequestedStudyApplication
    {
        Task UpdateStatus(UpdateDto dates);
        Task<List<SamplingListDto>> GetAll(RequestedStudySearchDto search);
    }
}
