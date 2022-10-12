using Service.Catalog.Domain.Catalog;
using Service.Catalog.Domain.EquipmentMantain;
using Service.Catalog.Dtos.Equipmentmantain;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Service.Catalog.Repository.IRepository
{
    public interface IEquipmentMantainRepository
    {
        Task<List<Mantain>> GetAll(MantainSearchDto search);
        Task<Mantain> GetById(Guid Id);
        Task Create(Mantain mantain);
        Task Update(Mantain mantain);
        Task<Equipos> GetEquip(int Id);

        Task<MantainImages> GetImage(Guid requestId, string code);
        Task UpdateImage(MantainImages requestImage);
    }
}
