using Service.Report.Domain.MedicalRecord;
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
        Task<List<MedicalRecordDto>> GetMedicalRecord(List<Guid> records);
        Task<List<Quotation>> GetQuotationByFilter(ReportFilterDto search);
        Task<List<RequestInfo>> GetRequestByFilter(ReportFilterDto search);
        Task<List<RequestStudies>> GetStudiesByFilter(ReportFilterDto search);
        Task<List<RequestRegister>> GetRequestPaymentByFilter(ReportFilterDto filter);
    }
}
