using Service.MedicalRecord.Domain.Request;
using Service.MedicalRecord.Dtos.Sampling;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Service.MedicalRecord.Repository.IRepository
{
    public interface ISamplingRepository
    {
        Task<List<Request>> GetAll(SamplingSearchDto search);
        Task UpdateStatus(UpdateDto dates);
    }
}
