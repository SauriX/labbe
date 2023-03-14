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
        Task<List<Promotion>> GetActive();
        Task<Promotion> GetById(int id);
        Task<Promotion> GetStudiesAndPacks(int promotionId);
        Task<List<PromotionStudy>> GetStudyPromos(Guid priceListId, Guid branchId, Guid? doctorId, int studyId);
        Task<List<PromotionPack>> GetPackPromos(Guid priceListId, Guid branchId, Guid? doctorId, int packId);
        Task<bool> IsDuplicate(Promotion promotion);
        Task Create(Promotion promotion);
        Task Update(Promotion promotion);
    }
}
