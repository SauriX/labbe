using Service.Catalog.Dtos.Equipment;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Service.Catalog.Application.IApplication
{
public interface IEquipmentApplication
    {
        Task<IEnumerable<EquipmentListDto>> GetAll(string search);
        Task<EquipmentFormDto> GetById(int Id);
        Task<EquipmentListDto> Create(EquipmentFormDto equipment);
        Task<EquipmentListDto> Update(EquipmentFormDto equipment);
        Task<(byte[] file, string fileName)> ExportList(string search);
        Task<(byte[] file, string fileName)> ExportForm(int id);
    }
}
