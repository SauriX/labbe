using Service.MedicalRecord.Application.IApplication;
using Service.MedicalRecord.Dtos.Reports;
using Service.MedicalRecord.Dtos.Reports.BudgetStats;
using Service.MedicalRecord.Repository.IRepository;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using Service.MedicalRecord.Mapper.Reports;

namespace Service.MedicalRecord.Application
{
    public class ReportApplication : IReportApplication
    {
        public readonly IReportRepository _repository;

        public ReportApplication(IReportRepository repository)
        {
            _repository = repository;
        }

        // Cotizaciones
        public async Task<IEnumerable<BudgetStatsDto>> GetQuotationByFilter(ReportFilterDto filter)
        {
            var data = await _repository.GetByQuotation(filter);
            var results = data.ToBudgetRequestDto();

            return results;
        }

        public async Task<BudgetDto> GetQuotationTableByFilter(ReportFilterDto filter)
        {
            var data = await _repository.GetByQuotation(filter);
            var results = data.ToBudgetDto();

            return results;
        }

        public async Task<IEnumerable<BudgetStatsChartDto>> GetQuotationChartByFilter(ReportFilterDto filter)
        {
            var data = await _repository.GetByQuotation(filter);
            var results = data.ToBudgetStatsChartDto();

            return results;
        }
    }
}
