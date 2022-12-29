using Service.MedicalRecord.Domain.Request;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Service.MedicalRecord.Repository.IRepository
{
    public interface IWorkListRepository
    {
        Task<List<Request>> GetWorkList(int areaId, List<Guid> branchesId, DateTime date, DateTime startTime, DateTime endTime);
        Task<List<Request>> GetMassiveWorkList(int areaId, List<Guid> branchesId, List<DateTime> date);
    }
}
