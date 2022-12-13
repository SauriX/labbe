using Service.MedicalRecord.Application.IApplication;
using Service.MedicalRecord.Dtos.Reports;
using Service.MedicalRecord.Repository.IRepository;
using System.Collections.Generic;
using System.Threading.Tasks;
using Service.MedicalRecord.Dtos.Reports.StudyStats;
using Service.MedicalRecord.Mapper;
using Service.MedicalRecord.Dtos.Reports.BudgetStats;

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
            var results = data.ToQuotationReportDto();

            return results;
        }

        public async Task<IEnumerable<ReportInfoDto>> GetRequestByFilter(ReportFilterDto filter)
        {
            var data = await _repository.GetByFilter(filter);
            var results = data.ToReportRequestDto();

            return results;
        }
        
        public async Task<IEnumerable<StudiesDto>> GetStudiesByFilter(ReportFilterDto filter)
        {
            var data = await _repository.GetByStudies(filter);
            var results = data.RequestStudies();

            return results;
        }
    }
}
