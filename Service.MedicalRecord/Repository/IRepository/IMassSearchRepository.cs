using Service.MedicalRecord.Dtos.MassSearch;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Service.MedicalRecord.Domain.Request;

namespace Service.MedicalRecord.Repository.IRepository
{
    public interface IMassSearchRepository
    {
        Task<List<Request>> GetByFilter(MassSearchFilterDto filter);
        Task<List<Request>> GetAllCaptureResults(DeliverResultsFilterDto filter);
    }
}
