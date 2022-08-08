using Service.Report.Dtos;
using Service.Report.Dtos.CanceledRequest;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Service.Report.Application.IApplication
{
    public interface ICanceledRequestApplication
    {
        Task<IEnumerable<CanceledRequestDto>> GetByFilter(ReportFilterDto search);
        Task<CanceledDto> GetTableByFilter(ReportFilterDto search);
        Task<IEnumerable<CanceledRequestChartDto>> GetChartByFilter(ReportFilterDto search);
        Task<byte[]> DownloadReportPdf(ReportFilterDto search);
    }
}
