﻿using Service.Catalog.Domain.Medics;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Service.Catalog.Dtos.Medicos;
namespace Service.Catalog.Repository.IRepository
{

    public interface IMedicsRepository
    {
        Task<Medics> GetById(Guid Id);
        Task Create(Medics doctors);
        Task Update(Medics Doctors);
        Task<List<Medics>> GetAll(string search = null);
        Task<List<Medics>> GetActive();
        Task<Medics> GetByCode(string code);


    }

}
