using Service.MedicalRecord.Dtos.MedicalRecords;
using Service.MedicalRecord.Dtos.PriceQuote;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Service.MedicalRecord.Application.IApplication
{
    public interface IPriceQuoteApplication
    {
        Task<List<PriceQuoteListDto>> GetNow(PriceQuoteSearchDto search);
        Task<List<PriceQuoteListDto>> GetActive();
        Task<PriceQuoteFormDto> GetById(Guid id);
        Task<PriceQuoteListDto> Create(PriceQuoteFormDto priceQuote);
        Task<PriceQuoteListDto> Update(PriceQuoteFormDto expediente);
        Task<(byte[] file, string fileName)> ExportList(PriceQuoteSearchDto search);
        Task<(byte[] file, string fileName)> ExportForm(Guid id);
        Task<List<MedicalRecordsListDto>> GetMedicalRecord(PriceQuoteExpedienteSearch search);
    }
}
