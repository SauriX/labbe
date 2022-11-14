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
        Task<List<PriceQuote>> GetActive();
        Task<PriceQuote> GetById(Guid id);
        Task<string> GetLastCode(Guid branchId, string date);
        //Task<List<PriceQuoteStudy>> GetStudyById(Guid priceQuoteId, IEnumerable<int> studiesIds);
        //Task<List<PriceQuoteStudy>> GetStudiesByPriceQuote(Guid priceQuoteId);
        Task<List<PriceQuotePack>> GetPacksByPriceQuote(Guid priceQuoteId);
        Task Create(PriceQuote priceQuote);
        Task Update(PriceQuote priceQuote);
        Task BulkUpdatePacks(Guid priceQuoteId, List<PriceQuotePack> studies);
        Task BulkUpdateDeletePacks(Guid priceQuoteId, List<PriceQuotePack> studies);
        //Task BulkUpdateStudies(Guid priceQuoteId, List<PriceQuoteStudy> studies);
        //Task BulkUpdateDeleteStudies(Guid priceQuoteId, List<PriceQuoteStudy> studies);
    }
}
