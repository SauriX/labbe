using Service.MedicalRecord.Dtos.Reports;
using Service.MedicalRecord.Dtos.Reports.BudgetStats;
using Service.MedicalRecord.Dtos.Reports.StudyStats;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Service.MedicalRecord.Application.IApplication
{
    public interface IReportApplication
    {
        Task<IEnumerable<BudgetStatsDto>> GetQuotationByFilter(ReportFilterDto search);
        Task<IEnumerable<ReportInfoDto>> GetRequestByFilter(ReportFilterDto search);
        Task<IEnumerable<StudiesDto>> GetStudiesByFilter(ReportFilterDto search);
    }
}
