using Service.Report.Domain.Request;
using Service.Report.Dtos;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Service.Report.Repository.IRepository
{
    public interface IReportRepository
    {
        Task<List<Request>> GetByFilter(ReportFilterDto search);
        Task<List<RequestPayment>> GetPaymentByFilter(ReportFilterDto search);
        Task<List<RequestStudy>> GetByStudies(ReportFilterDto search);
    }
}
