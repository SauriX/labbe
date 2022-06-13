using Service.Catalog.Dtos.Promotion;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Service.Catalog.Application.IApplication
{
    public interface IPromotionApplication
    {
        Task<IEnumerable<PromotionListDto>> GetAll(string search);
        Task<PromotionFormDto> GetById(int id);
        Task<IEnumerable<PromotionListDto>> GetActive();
        Task<PromotionListDto> Create(PromotionFormDto parameter);
        Task<PromotionListDto> Update(PromotionFormDto parameter);
        Task<(byte[] file, string fileName)> ExportList(string search);
        Task<(byte[] file, string fileName)> ExportForm(int id);
    }
}
