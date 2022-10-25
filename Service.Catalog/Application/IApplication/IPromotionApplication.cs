using Service.Catalog.Domain.Promotion;
using Service.Catalog.Dtos.PriceList;
using Service.Catalog.Dtos.Promotion;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Service.Catalog.Application.IApplication
{
    public interface IPromotionApplication
    {
        Task<IEnumerable<PromotionListDto>> GetAll(string search);
        Task<PromotionFormDto> GetById(int id);
        Task<IEnumerable<PromotionListDto>> GetActive();
        Task<List<PriceListInfoPromoDto>> GetStudyPromos(List<PriceListInfoFilterDto> filter);
        Task<List<PriceListInfoPromoDto>> GetPackPromos(List<PriceListInfoFilterDto> filter);
        Task<PromotionListDto> Create(PromotionFormDto parameter);
        Task<PromotionListDto> Update(PromotionFormDto parameter);
        Task<(byte[] file, string fileName)> ExportList(string search);
        Task<(byte[] file, string fileName)> ExportForm(int id);
    }
}
