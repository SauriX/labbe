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
        Task<PriceListListDto> Create(PriceListFormDto indicacion);
        Task<PriceListListDto> Update(PriceListFormDto indication);
        Task<IEnumerable<PriceListListDto>> GetActive();
        Task<(byte[] file, string fileName)> ExportList(string search);
        Task<(byte[] file, string fileName)> ExportForm(string id);
        //Task<IEnumerable<PriceListListDto>> GetAllCompany(int companyId);
        //Task<IEnumerable<PriceListListDto>> GetAllBranch(Guid branchId);
        //Task<IEnumerable<PriceListListDto>> GetAllMedics(int medicsId);
    }
}
