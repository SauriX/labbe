using Service.Report.Dtos;
using Service.Report.Dtos.ContactStats;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Service.Report.Application.IApplication
{
    public interface IContactStatsApplication
    {
        Task<IEnumerable<ContactStatsDto>> GetByContact();
        Task<IEnumerable<ContactStatsChartDto>> GetForChart();
        Task<IEnumerable<ContactStatsDto>> GetFilter(ReportFiltroDto search);
        Task<IEnumerable<ContactStatsChartDto>> GetFilterChart(ReportFiltroDto search);
        Task<byte[]> GenerateReportPDF(ReportFiltroDto search);
    }
}
