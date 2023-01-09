using Service.Report.Domain.Indicators;
using Service.Report.Domain.Request;
using Service.Report.Dtos;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Service.Report.Repository.IRepository
{
    public interface IReportRepository
    {
        Task<List<Request>> GetByFilter(ReportFilterDto search);
        Task<List<RequestPayment>> GetPaymentByFilter(ReportFilterDto search);
        Task<List<Indicators>> GetBudgetByDate(DateTime startDate, DateTime endDate);
        Task<List<RequestStudy>> GetByStudies(ReportFilterDto search);
        Task CreateIndicators(Indicators indicator);
        Task UpdateIndicators(Indicators indicator);
        Task<bool> IsDuplicate(Indicators indicator);
        Task<Indicators> GetIndicatorById(Guid branchId, DateTime date);
    }
}
