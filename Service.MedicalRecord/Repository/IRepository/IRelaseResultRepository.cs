using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Service.MedicalRecord.Domain.Request;
using Service.MedicalRecord.Dtos.General;
using Service.MedicalRecord.Dtos.RelaseResult;

namespace Service.MedicalRecord.Repository.IRepository
{
    public interface IRelaseResultRepository
    {
        Task<List<Request>> GetAll(GeneralFilterDto search);
        Task<Request> FindAsync(Guid id);
        Task<List<RequestStudy>> GetStudyById(Guid requestId, IEnumerable<int> studiesIds);
        Task BulkUpdateStudies(Guid requestId, List<RequestStudy> studies);
    }
}
