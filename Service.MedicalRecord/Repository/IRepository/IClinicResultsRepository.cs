using Service.MedicalRecord.Domain.Request;
using Service.MedicalRecord.Dtos.RequestedStudy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Service.MedicalRecord.Repository.IRepository
{
    public interface IClinicResultsRepository
    {
        Task<List<Request>> GetAll(RequestedStudySearchDto search);
        Task<Request> GetById(Guid id);
        Task<Request> FindAsync(Guid id);
        Task<List<RequestStudy>> GetStudyById(Guid requestId, IEnumerable<int> studiesIds);
        Task BulkUpdateStudies(Guid requestId, List<RequestStudy> studies);
        Task<RequestImage> GetImage(Guid requestId, string code);
        Task<List<RequestImage>> GetImages(Guid requestId);
        Task UpdateImage(RequestImage requestImage);
        Task DeleteImage(Guid requestId, string code);
        Task Create(object newParameter);
    }
}
