using Service.MedicalRecord.Domain.Request;
using Service.MedicalRecord.Dtos.InvoiceCompany;
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
        Task<List<RequestPayment>> GetPayments(Guid requestId);
        Task<string> GetLastPaymentCode(string serie, string year);
        Task Create(Request request);
        Task CreatePayment(RequestPayment request);
        Task Update(Request request);
        Task Delete(Request request);
        Task UpdateImage(RequestImage requestImage);
        Task UpdateStudy(RequestStudy study);
        Task UpdatePayment(RequestPayment payment);
        Task BulkInsertUpdatePacks(Guid requestId, List<RequestPack> packs);
        Task BulkUpdateDeletePacks(Guid requestId, List<RequestPack> packs);
        Task BulkInsertUpdateStudies(Guid requestId, List<RequestStudy> studies);
        Task BulkUpdatePayments(Guid requestId, List<RequestPayment> payments);
        Task BulkUpdateDeleteStudies(Guid requestId, List<RequestStudy> studies);
        Task BulkUpdateWeeStudies(Guid requestId, List<RequestStudyWee> studies);
        Task DeleteImage(Guid requestId, string code);
        Task<List<Request>> InvoiceCompanyFilter(InvoiceCompanyFilterDto filter);
        

    }
}
