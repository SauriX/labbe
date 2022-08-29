using Service.MedicalRecord.Domain.Request;
using Service.MedicalRecord.Dtos.RequestedStudy;
using Service.MedicalRecord.Dtos.Sampling;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Service.MedicalRecord.Repository.IRepository
{
    public interface IRequestedStudyRepository
    {
        Task<List<Request>> GetAll(RequestedStudySearchDto search);
        Task UpdateStatus(UpdateDto dates);
        Task<Request> FindAsync(Guid id);
        Task<List<RequestStudy>> GetStudyById(Guid requestId, IEnumerable<int> studiesIds);
        Task BulkUpdateStudies(Guid requestId, List<RequestStudy> studies);
    }
}
