using Service.MedicalRecord.Domain.Request;
using Service.MedicalRecord.Dtos.Request;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Service.MedicalRecord.Repository.IRepository
{
    public interface IRequestRepository
    {
        Task<Request> FindAsync(Guid id);
        Task<List<Request>> GetByFilter(RequestFilterDto filter);
        Task<Request> GetById(Guid id);
        Task<string> GetLastCode(Guid branchId, string date);
        Task<string> GetLastPathologicalCode(Guid branchId, string date, string type);
        Task<RequestStudy> GetStudyById(Guid requestId, int studyId);
        Task<List<RequestStudy>> GetStudyById(Guid requestId, IEnumerable<int> studiesIds);
        Task<List<RequestStudy>> GetAllStudies(Guid requestId);
        Task<List<RequestStudy>> GetStudiesByRequest(Guid requestId);
        Task<List<RequestPack>> GetPacksByRequest(Guid requestId);
        Task<RequestImage> GetImage(Guid requestId, string code);
        Task<List<RequestImage>> GetImages(Guid requestId);
        Task Create(Request request);
        Task Update(Request request);
        Task UpdateImage(RequestImage requestImage);
        Task UpdateStudy(RequestStudy study);
        Task BulkUpdatePacks(Guid requestId, List<RequestPack> studies);
        Task BulkUpdateDeletePacks(Guid requestId, List<RequestPack> studies);
        Task BulkUpdateStudies(Guid requestId, List<RequestStudy> studies);
        Task BulkUpdateDeleteStudies(Guid requestId, List<RequestStudy> studies);
        Task DeleteImage(Guid requestId, string code);
    }
}
