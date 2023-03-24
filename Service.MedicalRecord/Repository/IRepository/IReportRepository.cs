using Service.MedicalRecord.Domain.Quotation;
using Service.MedicalRecord.Domain.Request;
using Service.MedicalRecord.Dtos.Reports;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Service.MedicalRecord.Repository.IRepository
{
    public interface IReportRepository
    {
        Task<List<Request>> GetByFilter(ReportFilterDto search);
        //Task<List<RequestPayment>> GetPaymentByFilter(ReportFilterDto search);
        Task<List<RequestStudy>> GetByStudies(ReportFilterDto search);
        Task<List<Quotation>> GetByQuotation(ReportFilterDto search);
        Task<List<RequestPayment>> GetByPayment(ReportFilterDto search);
    }
}
