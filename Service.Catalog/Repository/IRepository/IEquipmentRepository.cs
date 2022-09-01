﻿using Service.Catalog.Domain.Catalog;
using Service.Catalog.Domain.Equipment;
using Service.Catalog.Dtos.Equipment;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Service.Catalog.Repository.IRepository
{
    public interface IEquipmentRepository
    {
        Task<List<Equipos>> GetAll(string search);
        Task<Equipos> GetById(int Id);
        Task<(bool, string)> IsDuplicate(Equipos equipment);
        Task Create(Equipos equipment);
        Task Update(Equipos equipment);
    }
}
