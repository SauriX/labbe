using Service.MedicalRecord.Domain;
using Service.MedicalRecord.Domain.Request;
using Service.MedicalRecord.Dtos.ClinicResults;
using Service.MedicalRecord.Dtos.Request;
using Service.MedicalRecord.Dtos.RequestedStudy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Service.MedicalRecord.Repository.IRepository
{
    public interface IClinicResultsRepository
    {
        Task CreateResultPathological(ClinicalResultsPathological result);
        Task UpdateResultPathologicalStudy(ClinicalResultsPathological result);
        Task UpdateStatusStudy(RequestStudy study);
        Task<RequestStudy> GetStudyById(int RequestStudyId);
        Task<ClinicalResultsPathological> GetResultPathologicalById(int id);
        Task<List<ClinicalResultsPathological>> GetListResultPathologicalById(List<int> ids);
        Task<List<ClinicResults>> GetLabResultsById(int id);
        Task<List<ClinicResults>> GetResultsById(Guid id);
        Task<List<Request>> GetSecondLastRequest(Guid recordId);
        Task<RequestStudy> GetRequestStudyById(int RequestStudyId);
        Task<List<Request>> GetAll(ClinicResultSearchDto search);
        Task<ClinicResults> GetById(Guid id);
        Task<List<ClinicResults>> GetByRequest(Guid requestId);
        Task<Request> FindAsync(Guid id);
        Task<List<RequestStudy>> GetStudyById(Guid requestId, IEnumerable<int> studiesIds);
        Task BulkUpdateStudies(Guid requestId, List<RequestStudy> studies);
        Task CreateLabResults(List<ClinicResults> newParameter);
        Task UpdateLabResults(List<ClinicResults> newParameter);
        Task<Request> GetRequestById(Guid id);
        Task UpdateMedioSolicitado(RequestStudy study);
        Task<string> GetMedioSolicitado(int RequestStudyId);

    }
}
