using Service.Report.Dtos;
using Service.Report.Dtos.BondedRequest;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Service.Report.Application.IApplication
{
    public interface IMaquilaInternApplication
    {
        Task<IEnumerable<MaquilaRequestDto>> GetByStudies(ReportFilterDto search);
        Task<IEnumerable<MaquilaRequestChartDto>> GetChartByFilter(ReportFilterDto search);
        Task<byte[]> DownloadReportPdf(ReportFilterDto search);
    }
}
