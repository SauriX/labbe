using Service.Report.Domain.PatientStats;
using Service.Report.Dtos.PatientStats;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Service.Report.Repository.IRepository
{
    public interface IPatientStatsRepository
    {
        Task<List<Report.Domain.PatientStats.PatientStats>> GetFilter(PatientStatsSearchDto search);
    }
}
