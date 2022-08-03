using Service.Catalog.Domain.Price;
using Service.Catalog.Domain.Promotion;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Service.Catalog.Repository.IRepository
{
    public interface IPromotionRepository
    {
        Task<List<Promotion>> GetAll(string search);
        Task<Promotion> GetById(int id);
        Task<List<Promotion>> GetActive();
        Task<PromotionStudy> GetStudyPromo(Guid priceListId, Guid branchId, int studyId);
        Task<PromotionPack> GetPackPromo(Guid priceListId, Guid branchId, int packId);
        Task Create(Promotion promotion);
        Task Update(Promotion promotion);
        Task<bool> IsDuplicate(Promotion promotion);
        Task<(bool existe, string nombre)> PackIsOnInvalidPromotion(int PackId);
        Task<(bool existe, string nombre)> PackIsOnPromotrtion(int id);
        Task<List<PriceList_Packet>> packsIsPriceList(Guid id);
    }
}
