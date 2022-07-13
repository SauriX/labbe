using Service.Catalog.Domain.Price;
using Service.Catalog.Dtos;
using Service.Catalog.Dtos.PriceList;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Service.Catalog.Application.IApplication
{
    public interface IPriceListApplication
    {
        Task<IEnumerable<PriceListListDto>> GetAll(string search);
        Task<PriceListFormDto> GetById(string Id);
        Task<PriceListInfoStudyDto> GetPriceStudyById(int id, Guid? companyId, Guid? doctorId, Guid? branchId);
        Task<PriceListInfoPackDto> GetPricePackById(int id, Guid? companyId, Guid? doctorId, Guid? branchId);
        Task<PriceListListDto> Create(PriceListFormDto indicacion);
        Task<PriceListListDto> Update(PriceListFormDto indication);
        Task<IEnumerable<PriceListListDto>> GetActive();
        Task<(byte[] file, string fileName)> ExportList(string search);
        Task<(byte[] file, string fileName)> ExportForm(string id);
        Task<IEnumerable<PriceListCompanyDto>> GetAllCompany(Guid companyId);
        Task<IEnumerable<PriceListBranchDto>> GetAllBranch(Guid branchId);
        Task<IEnumerable<PriceListMedicDto>> GetAllMedics(Guid medicsId);
    }
}
