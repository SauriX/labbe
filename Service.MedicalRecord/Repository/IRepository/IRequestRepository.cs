using Service.MedicalRecord.Domain.Request;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Service.MedicalRecord.Repository.IRepository
{
    public interface IRequestRepository
    {
        Task<Request> FindAsync(Guid id);
        Task<Request> GetById(Guid id);
        Task<string> GetLastCode(Guid branchId, string date);
        Task<RequestStudy> GetStudyById(Guid requestId, int studyId);
        Task<List<RequestStudy>> GetStudyById(Guid requestId, IEnumerable<int> studiesIds);
        Task<List<RequestStudy>> GetStudiesByRequest(Guid requestId);
        Task Create(Request request);
        Task Update(Request request);
        Task UpdateStudy(RequestStudy study);
        Task BulkUpdateStudies(Guid requestId, List<RequestStudy> studies);
    }
}
