using Service.Report.Dtos;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Service.Report.Application.IApplication
{
    public interface IBondedRequestApplication
    {
        Task<IEnumerable<BondedRequestDto>> GetByFilter(ReportFilterDto search);
        //Task<IEnumerable<StudyStatsChartDto>> GetChartByFilter(ReportFilterDto search);
        Task<byte[]> DownloadReportPdf(ReportFilterDto search);
    }
}
