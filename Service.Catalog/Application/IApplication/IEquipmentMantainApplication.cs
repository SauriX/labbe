using Service.Catalog.Dtos.Equipmentmantain;
using Service.Catalog.Repository;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Service.Catalog.Application.IApplication
{
    public interface IEquipmentMantainApplication
    {
        Task<List<MantainListDto>> GetAll(MantainSearchDto search);
        Task<MantainFormDto> GetById(Guid Id);
        Task<MantainListDto> Create(MantainFormDto mantain);
        Task<MantainListDto> Update(MantainFormDto mantain);
        Task<string> SaveImage(MantainImageDto requestDto);
        Task<byte[]> Print(Guid Id, string sucursal);
        Task<EquimentDetailDto> Getequip(int Id);
        Task DeleteImage(Guid Id, string code);
        Task<MantainListDto> UpdateStatus(Guid id);
    }
}
