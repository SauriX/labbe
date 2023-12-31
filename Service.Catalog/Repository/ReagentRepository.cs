﻿using Microsoft.EntityFrameworkCore;
using Service.Catalog.Context;
using Service.Catalog.Domain.Reagent;
using Service.Catalog.Repository.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Service.Catalog.Repository
{
    public class ReagentRepository : IReagentRepository
    {
        private readonly ApplicationDbContext _context;

        public ReagentRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<Reagent>> GetAll(string search)
        {
            var reagents = _context.CAT_Reactivo_Contpaq.AsQueryable();

            search = search.Trim().ToLower();

            if (!string.IsNullOrWhiteSpace(search) && search != "all")
            {
                reagents = reagents.Where(x => x.Clave.ToLower().Contains(search) || x.Nombre.ToLower().Contains(search));
            }

            return await reagents.ToListAsync();
        }

        public async Task<List<Reagent>> GetActive()
        {
            var reagents = await _context.CAT_Reactivo_Contpaq.Where(x => x.Activo).ToListAsync();

            return reagents;
        }

        public async Task<Reagent> GetById(Guid id)
        {
            var reagent = await _context.CAT_Reactivo_Contpaq.FindAsync(id);

            return reagent;
        }

        public async Task<bool> IsDuplicate(Reagent reagent)
        {
            var isDuplicate = await _context.CAT_Reactivo_Contpaq.AnyAsync(x => x.Id != reagent.Id && (x.Clave == reagent.Clave || x.Nombre == reagent.Nombre));

            return isDuplicate;
        }

        public async Task Create(Reagent reagent)
        {
            _context.CAT_Reactivo_Contpaq.Add(reagent);

            await _context.SaveChangesAsync();
        }

        public async Task Update(Reagent reagent)
        {
            _context.CAT_Reactivo_Contpaq.Update(reagent);

            await _context.SaveChangesAsync();
        }
    }
}
