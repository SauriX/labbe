using Service.Report.Dtos;
using Service.Report.Dtos.ContactStats;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Service.Report.Application.IApplication
{
    public interface IContactStatsApplication
    {
        Task<IEnumerable<ContactStatsDto>> GetByFilter(ReportFilterDto search);
        Task<IEnumerable<ContactStatsChartDto>> GetCharByFilter(ReportFilterDto search);
        Task<byte[]> DownloadReportPdf(ReportFilterDto search);
    }
}
