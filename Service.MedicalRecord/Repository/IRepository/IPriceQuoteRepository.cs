using Service.MedicalRecord.Domain.PriceQuote;
using Service.MedicalRecord.Dtos.PriceQuote;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Service.MedicalRecord.Repository.IRepository
{
    public interface IPriceQuoteRepository
    {
        Task<PriceQuote> FindAsync(Guid id);
        Task<List<PriceQuote>> GetByFilter(PriceQuoteFilterDto filter);
        Task<string> GetLastCode(Guid branchId, string date);
        Task<List<PriceQuote>> GetActive();
        Task<PriceQuote> GetById(Guid id);
        Task Create(PriceQuote priceQuote);
        Task Update(PriceQuote priceQuote);
    }
}
