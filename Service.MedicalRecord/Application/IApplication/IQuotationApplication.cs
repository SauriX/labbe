using Service.MedicalRecord.Dtos.General;
using Service.MedicalRecord.Dtos.MedicalRecords;
using Service.MedicalRecord.Dtos.Quotation;
using Service.MedicalRecord.Dtos.WeeClinic;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Service.MedicalRecord.Application.IApplication
{
    public interface IQuotationApplication
    {
        Task<IEnumerable<QuotationInfoDto>> GetByFilter(GeneralFilterDto filter);
        Task<IEnumerable<QuotationInfoDto>> GetActive();
        Task<QuotationDto> GetById(Guid quotationId);
        Task<QuotationGeneralDto> GetGeneral(Guid quotationId);
        Task<QuotationStudyUpdateDto> GetStudies(Guid quotationId);
        Task SendTestEmail(QuotationSendDto quotationDto);
        Task SendTestWhatsapp(QuotationSendDto quotationDto);
        Task<string> Create(QuotationDto quotationDto);
        Task<string> ConvertToRequest(Guid quotationId, Guid userId, string userName);
        Task UpdateGeneral(QuotationGeneralDto quotationDto);
        Task AssignRecord(Guid quotationId, Guid? recordId, Guid userId);
        Task UpdateTotals(QuotationTotalDto quotationDto);
        Task<QuotationStudyUpdateDto> UpdateStudies(QuotationStudyUpdateDto quotationDto);
        Task CancelQuotation(Guid quotationId, Guid userId);
        Task DeleteQuotation(Guid quotationId);
        Task DeleteStudies(QuotationStudyUpdateDto quotationDto);
        Task<byte[]> PrintQuotation(Guid quotationId);
        Task<byte[]> ExportQuote(Guid id);
    }
}
