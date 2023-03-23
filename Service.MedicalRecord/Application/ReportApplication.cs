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

        public async Task<IEnumerable<BudgetStatsDto>> GetQuotationByFilter(ReportFilterDto filter)
        {
            var data = await _repository.GetByQuotation(filter);

            return data.ToQuotationReportDto();
        }

        public async Task<IEnumerable<ReportInfoDto>> GetRequestByFilter(ReportFilterDto filter)
        {
            var data = await _repository.GetByFilter(filter);

            return data.ToReportRequestDto();
        }
        
        public async Task<IEnumerable<StudiesDto>> GetStudiesByFilter(ReportFilterDto filter)
        {
            var data = await _repository.GetByStudies(filter);

            return data.RequestStudies();
        }

        public async Task<IEnumerable<RequestPaymentStatsDto>> GetPaymentsByFilter(ReportFilterDto filter, string user)
        {
            var data = await _repository.GetByPayment(filter);

            var payment = data.RequestPayment(user);

            return payment;
        }
    }
}
