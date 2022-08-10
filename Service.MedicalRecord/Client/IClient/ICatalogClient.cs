using Service.MedicalRecord.Dtos.Request;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Service.MedicalRecord.Client.IClient
{
    public interface ICatalogClient
    {
        Task<string> GetCodeRange(Guid branchId);
        Task<List<RequestStudyParamsDto>> GetStudies(List<int> studies);
    }
}
