using Service.Catalog.Domain.Catalog;
using Service.Catalog.Domain.Equipment;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Service.Catalog.Repository.IRepository
{
    public interface IEquipmentRepository
    {
        Task<List<Equipos>> GetAll(string search);
        Task<Equipos> GetById(int Id);
        Task<bool> IsDuplicate(Equipos equipment);
        Task Create(Equipos equipment);
        Task Update(Equipos equipment);
    }
}
