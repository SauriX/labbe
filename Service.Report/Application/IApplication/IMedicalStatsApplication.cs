using Service.Report.Dtos;
using Service.Report.Dtos.MedicalStats;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Service.Report.Application.IApplication
{
    public interface IMedicalStatsApplication
    {
        Task<IEnumerable<MedicalStatsDto>> GetByFilter(ReportFilterDto search);
        Task<byte[]> DownloadReportPdf(ReportFilterDto search);
    }
}
