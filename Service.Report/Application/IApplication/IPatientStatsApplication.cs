using Service.Report.Dtos.PatientStats;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Service.Report.Application.IApplication
{
    public interface IPatientStatsApplication
    {
        Task<IEnumerable<PatientStatsFiltroDto>> GetFilter(PatientStatsSearchDto search);
        Task<(byte[] file, string fileName)> ExportTableStats(PatientStatsSearchDto search);
        Task<(byte[] file, string fileName)> ExportChartStats(PatientStatsSearchDto search);
        Task<byte[]> GenerateReportPDF(PatientStatsSearchDto search);
    }
}
