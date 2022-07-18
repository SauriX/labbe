using Service.Report.Dtos.MedicalStats;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Service.Report.Application.IApplication
{
    public interface IMedicalStatsApplication
    {
        Task<IEnumerable<MedicalStatsFiltroDto>> GetByDoctor();
        Task<IEnumerable<MedicalStatsFiltroDto>> GetFilter(MedicalStatsSearchDto search);
        Task<byte[]> GenerateReportPDF(MedicalStatsSearchDto search);
    }
}
