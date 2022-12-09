using Service.MedicalRecord.Dtos.Reports;
using Service.MedicalRecord.Dtos.Reports.BudgetStats;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Service.MedicalRecord.Application.IApplication
{
    public interface IReportApplication
    {
        // Cotizaciones
        Task<IEnumerable<BudgetStatsDto>> GetQuotationByFilter(ReportFilterDto search);
        Task<BudgetDto> GetQuotationTableByFilter(ReportFilterDto filter);
        Task<IEnumerable<BudgetStatsChartDto>> GetQuotationChartByFilter(ReportFilterDto filter);
    }
}
