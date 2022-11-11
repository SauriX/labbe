using Service.MedicalRecord.Domain.PriceQuote;
using Service.MedicalRecord.Dtos.PriceQuote;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Service.MedicalRecord.Repository.IRepository
{
    public interface IPriceQuoteRepository
    {
        Task<List<PriceQuote>> GetByFilter(PriceQuoteFilterDto filter);
        Task<PriceQuote> GetById(Guid id);
        public Task Create(PriceQuote expediente);
        Task<List<PriceQuote>> GetActive();
        Task Update(PriceQuote expediente);
        Task<List<MedicalRecord.Domain.MedicalRecord.MedicalRecord>> GetMedicalRecord(PriceQuoteExpedienteSearch search);
        Task<string> GetLastCode(string date);
    }
}
