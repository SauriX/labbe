using Service.Report.Dtos;
using Service.Report.Dtos.BondedRequest;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Service.Report.Application.IApplication
{
    public interface IMaquilaExternApplication
    {
        Task<IEnumerable<MaquilaRequestDto>> GetByStudies(ReportFilterDto search);
        Task<IEnumerable<MaquilaRequestChartDto>> GetChartByFilter(ReportFilterDto search);
        Task<byte[]> DownloadReportPdf(ReportFilterDto search);
    }
}
