using Service.Report.Dtos;
using Service.Report.Dtos.StudyStats;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Service.Report.Application.IApplication
{
    public interface IStudyStatsApplication
    {
        Task<IEnumerable<StudyStatsDto>> GetByFilter(ReportFilterDto search);
        Task<IEnumerable<StudyStatsChartDto>> GetCharByFilter(ReportFilterDto search);
        Task<byte[]> DownloadReportPdf(ReportFilterDto search);
    }
}
