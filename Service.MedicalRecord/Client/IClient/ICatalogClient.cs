using Service.MedicalRecord.Dtos.Branch;
using Service.MedicalRecord.Dtos.Promos;
using Service.MedicalRecord.Dtos.Request;
using Service.MedicalRecord.Dtos.Route;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Service.MedicalRecord.Client.IClient
{
    public interface ICatalogClient
    {
        Task<string> GetCodeRange(Guid branchId);
        Task<List<RequestStudyParamsDto>> GetStudies(List<int> studies);
        Task<List<PriceListInfoPromoDto>> GetStudiesPromos(List<PriceListInfoFilterDto> studies);
        Task<List<PriceListInfoPromoDto>> GetPacksPromos(List<PriceListInfoFilterDto> packs);
        Task<BranchFormDto> GetBranch(Guid id);
        Task<RouteFormDto> GetRuta(Guid id);
    }
}
