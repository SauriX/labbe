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
        Task<bool> SaveImage(MantainImageDto[] requestDto);
        Task<byte[]> Print(Guid Id);
        Task<EquimentDetailDto> Getequip(int Id);
    }
}
