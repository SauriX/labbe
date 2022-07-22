using Service.MedicalRecord.Domain.Request;
using System;
using System.Threading.Tasks;

namespace Service.MedicalRecord.Repository.IRepository
{
    public interface IRequestRepository
    {
        Task<Request> GetById(Guid id);
        Task<string> GetLastCode(Guid branchId, string date);
        Task Create(Request request);
        Task Update(Request request);
    }
}
