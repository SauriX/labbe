using Service.Report.Dtos;
using Service.Report.Dtos.BudgetStats;
using Service.Report.Dtos.MedicalRecord;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Service.Report.Client.IClient
{
    public interface IMedicalRecordClient
    {
        Task<MedicalRecordDto> GetMedicalRecord(List<Guid> records);
        Task<List<BudgetStatsDto>> GetQuotationByFilter(ReportFilterDto search);
        Task<BudgetDto> GetQuotationTableByFilter(ReportFilterDto search);
        Task<List<BudgetStatsChartDto>> GetQuotationChartByFilter(ReportFilterDto search);
    }
}
