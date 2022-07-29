using Service.Report.Dtos;
using Service.Report.Dtos.StudyStats;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Service.Report.Application.IApplication
{
    public interface IUrgentStatsApplication
    {
        Task<IEnumerable<StudyStatsDto>> GetByFilter(ReportFilterDto search);
        Task<IEnumerable<StudyStatsChartDto>> GetChartByFilter(ReportFilterDto search);
        Task<byte[]> DownloadReportPdf(ReportFilterDto search);
    }
}
