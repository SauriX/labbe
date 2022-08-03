using Service.Report.Dtos;
using Service.Report.Dtos.CompanyStats;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Service.Report.Application.IApplication
{
    public interface ICompanyStatsApplication
    {
        Task<IEnumerable<CompanyStatsDto>> GetByFilter(ReportFilterDto search);
        Task<CompanyDto> GetTableByFilter(ReportFilterDto search);
        Task<IEnumerable<CompanyStatsChartDto>> GetChartByFilter(ReportFilterDto search);
        Task<byte[]> DownloadReportPdf(ReportFilterDto search);
    }
}