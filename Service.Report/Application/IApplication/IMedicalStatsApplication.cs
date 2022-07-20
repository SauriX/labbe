using Service.Report.Dtos;
using Service.Report.Dtos.MedicalStats;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Service.Report.Application.IApplication
{
    public interface IMedicalStatsApplication
    {
        Task<IEnumerable<MedicalStatsDto>> GetByDoctor();
        Task<IEnumerable<MedicalStatsDto>> GetFilter(ReportFiltroDto search);
        Task<byte[]> GenerateReportPDF(ReportFiltroDto search);
    }
}
