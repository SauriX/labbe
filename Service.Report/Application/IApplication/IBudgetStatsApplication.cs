using Service.Report.Dtos.TypeRequest;
using Service.Report.Dtos;
using System.Collections.Generic;
using System.Threading.Tasks;
using Service.Report.Dtos.BudgetStats;

namespace Service.Report.Application.IApplication
{
    public interface IBudgetStatsApplication
    {
        Task<List<BudgetStatsDto>> GetByFilter(ReportFilterDto search);
        Task<BudgetDto> GetTableByFilter(ReportFilterDto search);
        Task<List<BudgetStatsChartDto>> GetChartByFilter(ReportFilterDto search);
        Task<byte[]> DownloadReportPdf(ReportFilterDto search);
    }
}
