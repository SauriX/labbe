using Service.Report.Domain.Request;
using Service.Report.Dtos.PatientStats;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Service.Report.Repository.IRepository
{
    public interface IPatientStatsRepository
    {
        Task<List<Report.Domain.Request.Request>> GetFilter(PatientStatsSearchDto search);
    }
}
