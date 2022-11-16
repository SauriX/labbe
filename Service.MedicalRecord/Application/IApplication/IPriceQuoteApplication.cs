using Service.MedicalRecord.Dtos.MedicalRecords;
using Service.MedicalRecord.Dtos.PriceQuote;
using Service.MedicalRecord.Dtos.Request;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Service.MedicalRecord.Application.IApplication
{
    public interface IPriceQuoteApplication
    {
        Task<List<PriceQuoteListDto>> GetByFilter(PriceQuoteFilterDto filter);
        Task<List<PriceQuoteListDto>> GetActive();
        Task<PriceQuoteFormDto> GetById(Guid id);
        Task<PriceQuoteListDto> Create(PriceQuoteFormDto priceQuote);
        Task<PriceQuoteListDto> Update(PriceQuoteFormDto expediente);
        Task<(byte[] file, string fileName)> ExportList(PriceQuoteFilterDto search);
        Task<(byte[] file, string fileName)> ExportForm(Guid id);
        Task<List<MedicalRecordsListDto>> GetMedicalRecord(PriceQuoteExpedienteSearch search);
        Task<byte[]> GetTicket();
        Task SendTestEmail(RequestSendDto requestDto);
        Task SendTestWhatsapp(RequestSendDto requestDto);
    }
}
