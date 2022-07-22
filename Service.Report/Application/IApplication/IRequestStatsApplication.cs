using Service.Report.Dtos;
using Service.Report.Dtos.Request;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Service.Report.Application.IApplication
{
    public interface IRequestStatsApplication
    {
        Task<IEnumerable<RequestStatsDto>> GetByFilter(ReportFilterDto search);
        Task<byte[]> DownloadReportPdf(ReportFilterDto search);
    }
}
