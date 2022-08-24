using Service.MedicalRecord.Application.IApplication;
using Service.MedicalRecord.Dtos.RequestedStudy;
using Service.MedicalRecord.Dtos.Sampling;
using Service.MedicalRecord.Mapper;
using Service.MedicalRecord.Repository.IRepository;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Service.MedicalRecord.Application
{
    public class RequestedStudyApplication : IRequestedStudyApplication
    {
        public readonly IRequestedStudyRepository _repository;
        public RequestedStudyApplication(IRequestedStudyRepository repository)
        {
            _repository = repository;
        }

        public async Task<List<SamplingListDto>> GetAll(RequestedStudySearchDto search)
        {
            var requestedStudy = await _repository.GetAll(search);
            if(requestedStudy != null)
            {
                return requestedStudy.ToRequestedStudyDto();
            }
            else
            {
                return null;
            }
        }

        public async Task UpdateStatus(UpdateDto dates)
        {
            await _repository.UpdateStatus(dates);
        }
    }
}
