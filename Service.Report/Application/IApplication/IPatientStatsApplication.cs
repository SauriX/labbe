using Service.Report.Dtos.PatientStats;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Service.Report.Application.IApplication
{
    public interface IPatientStatsApplication
    {
        Task<IEnumerable<PatientStatsFiltroDto>> GetBranchByCount();
        Task<IEnumerable<PatientStatsFiltroDto>> GetFilter(PatientStatsSearchDto search);
        Task<(byte[] file, string fileName)> ExportTableStats(string search = null);
        Task<(byte[] file, string fileName)> ExportChartStats(string search = null);
        Task<byte[]> GenerateReportPDF();
    }
}
