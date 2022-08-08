using Service.Report.Dtos;
using Service.Report.Dtos.MedicalBreakdownStats;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Service.Report.Application.IApplication
{
    public interface IMedicalBreakdownStatsApplication
    {
        Task<IEnumerable<MedicalBreakdownRequestDto>> GetByFilter(ReportFilterDto search);
        Task<MedicalBreakdownDto> GetTableByFilter(ReportFilterDto search);
        Task<IEnumerable<MedicalBreakdownRequestChartDto>> GetChartByFilter(ReportFilterDto search);
        Task<byte[]> DownloadReportPdf(ReportFilterDto search);
    }
}
