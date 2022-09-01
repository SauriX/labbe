using Service.MedicalRecord.Dtos.Sampling;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Service.MedicalRecord.Application.IApplication
{
    public interface ISamplingApplication
    {
        Task UpdateStatus(UpdateDto dates);
        Task<List<SamplingListDto>> GetAll(rRequestedStudySearchDto search);
    }
}
