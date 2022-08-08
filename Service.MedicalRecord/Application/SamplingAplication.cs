using Service.MedicalRecord.Application.IApplication;
using Service.MedicalRecord.Dtos.Sampling;
using Service.MedicalRecord.Mapper;
using Service.MedicalRecord.Repository.IRepository;
using System.Collections.Generic;
using System.Threading.Tasks;
using static Service.MedicalRecord.Dictionary.Response;

namespace Service.MedicalRecord.Application
{
    public class SamplingAplication:ISamplingApplication
    {
        public readonly ISamplingRepository _repository;

        public SamplingAplication(ISamplingRepository repository)
        {

            _repository = repository;
        }

        public async  Task<List<SamplingListDto>> GetAll(SamplingSearchDto search) {
            var sampling = await _repository.GetAll(search);
            if (sampling != null)
            {
                return sampling.ToSamplingListDto();
            }
            else { return null; }
           
        }
        public async Task UpdateStatus(UpdateDto dates) {
            await _repository.UpdateStatus(dates);
        }
    }
}
