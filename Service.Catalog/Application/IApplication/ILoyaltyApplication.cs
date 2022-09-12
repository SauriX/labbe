using Service.Catalog.Dtos.Loyalty;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Service.Catalog.Application.IApplication
{
    public interface ILoyaltyApplication
    {
        Task<IEnumerable<LoyaltyListDto>> GetAll(string search);
        Task<IEnumerable<LoyaltyListDto>> GetActive();
        Task<LoyaltyFormDto> GetById(Guid Id);
        Task<LoyaltyListDto> GetByDate(DateTime fecha);
        Task<LoyaltyListDto> Create(LoyaltyFormDto indicacion);
        Task<LoyaltyListDto> Update(LoyaltyFormDto indication);
        Task<(byte[] file, string fileName)> ExportList(string search);
        Task<(byte[] file, string fileName)> ExportForm(Guid id);
        Task<LoyaltyListDto> CreateReschedule(LoyaltyFormDto indicacion);


    }
}
