using Service.Report.Dtos;
using Service.Report.Dtos.TypeRequest;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Service.Report.Application.IApplication
{
    public interface IDescountRequestApplication
    {
        Task<IEnumerable<TypeRequestDto>> GetByFilter(ReportFilterDto search);
        Task<TypeDto> GetTableByFilter(ReportFilterDto search);
        Task<IEnumerable<TypeRequestChartDto>> GetChartByFilter(ReportFilterDto search);
        Task<byte[]> DownloadReportPdf(ReportFilterDto search);
    }
}
