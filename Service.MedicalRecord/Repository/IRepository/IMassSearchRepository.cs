using Service.MedicalRecord.Dtos.MassSearch;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Service.MedicalRecord.Domain.Request;
using Service.MedicalRecord.Dtos.General;

namespace Service.MedicalRecord.Repository.IRepository
{
    public interface IMassSearchRepository
    {
        Task<List<Request>> GetByFilter(GeneralFilterDto filter);
        Task<List<Request>> GetAllCaptureResults(GeneralFilterDto filter);
    }
}
