﻿using EFCore.BulkExtensions;
using Microsoft.EntityFrameworkCore;
using Service.Catalog.Context;
using Service.Catalog.Domain.Branch;
using Service.Catalog.Domain.Company;
using Service.Catalog.Domain.Price;
using Service.Catalog.Repository.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Service.Catalog.Repository
{
    public class PriceListRepository : IPriceListRepository
    {
        private readonly ApplicationDbContext _context;

        public PriceListRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<PriceList>> GetAll(string search)
        {
            var indications = _context.CAT_ListaPrecio.AsQueryable()
                    .Include(x => x.Estudios)
                    .ThenInclude(x => x.Estudio).ThenInclude(x => x.Area).ThenInclude(x => x.Departamento)
                    .Include(x => x.Sucursales).ThenInclude(x => x.Sucursal)
                    .Include(x => x.Compañia).ThenInclude(x => x.Compañia)
                    .Include(x => x.Medicos).ThenInclude(x => x.Medico)
                    .AsQueryable();

            search = search.Trim().ToLower();

            if (!string.IsNullOrWhiteSpace(search) && search != "all")
            {
                indications = indications.Where(x => x.Clave.ToLower().Contains(search) || x.Nombre.ToLower().Contains(search));
            }

            return await indications.ToListAsync();
        }

        public async Task<PriceList> GetById(Guid Id)
        {
            var indication = await _context.CAT_ListaPrecio
                .Include(x => x.Estudios).ThenInclude(x => x.Estudio).ThenInclude(x => x.Area).ThenInclude(x => x.Departamento)
                .Include(x => x.Sucursales).ThenInclude(x => x.Sucursal)
                .Include(x=>x.Compañia).ThenInclude(x=>x.Compañia)
                .Include(x=>x.Medicos).ThenInclude(x=>x.Medico)
                .FirstOrDefaultAsync(x => x.Id == Id);

            return indication;
        }
        public async Task<List<PriceList>> GetActive()
        {
            var prices = await _context.CAT_ListaPrecio.Where(x => x.Activo).ToListAsync();

            return prices;
        }
        public async Task<bool> IsDuplicate(PriceList price)
        {
            var isDuplicate = await _context.CAT_ListaPrecio.AnyAsync(x => x.Id != price.Id && (x.Clave == price.Clave || x.Nombre == price.Nombre));

            return isDuplicate;
        }

        public async Task Create(PriceList price)
        {
            _context.CAT_ListaPrecio.Add(price);

            await _context.SaveChangesAsync();
        }

        public async Task Update(PriceList price)
        {
            var branches = price.Sucursales.ToList();
            var packs = price.Paquete.ToList();
            var studies = price.Estudios.ToList();
            var medic= price.Medicos.ToList();
           price.Sucursales = null;
           price.Paquete = null;
           price.Estudios = null;
           price.Medicos = null;
            _context.CAT_ListaPrecio.Update(price);
            var config = new BulkConfig();
            config.SetSynchronizeFilter<Price_Branch>(x => x.PrecioListaId ==price.Id);
            branches.ForEach(x => x.PrecioListaId =price.Id);
            await _context.BulkInsertOrUpdateOrDeleteAsync(branches, config);

            config.SetSynchronizeFilter<PriceList_Packet>(x => x.PrecioListaId == price.Id);
            packs.ForEach(x => x.PrecioListaId = price.Id);
            await _context.BulkInsertOrUpdateOrDeleteAsync(packs, config);

            config.SetSynchronizeFilter<PriceList_Study>(x => x.PrecioListaId == price.Id);
            studies.ForEach(x => x.PrecioListaId =price.Id);
            await _context.BulkInsertOrUpdateOrDeleteAsync(studies, config);

            config.SetSynchronizeFilter<Price_Promotion>(x => x.PrecioListaId == price.Id);
            medic.ForEach(x => x.PrecioListaId = price.Id);
            await _context.BulkInsertOrUpdateOrDeleteAsync(medic, config);
            await _context.SaveChangesAsync();
        }
        public async Task<List<Price_Company>> GetAllCompany(Guid companyId)
        {
            var asignado = await
                (from company in _context.CAT_Compañia
                 join priceList in _context.CAT_ListaP_Compañia.Where(x => x.CompañiaId == companyId) on company.Id equals priceList.CompañiaId into ljPriceList
                 from pList in ljPriceList.DefaultIfEmpty()
                 select new { company, pList })
                 .Select(x => new Price_Company
                 {
                     Compañia = x.company,
                     PrecioLista = x.pList == null ? null : x.pList.PrecioLista,
                 })
                .ToListAsync();

            return asignado;
        }
        public async Task<List<Price_Branch>> GetAllBranch(Guid branchId)
        {
            var asignado = await
                (from branch in _context.CAT_Sucursal
                 join priceList in _context.CAT_ListaP_Sucursal.Where(x => x.SucursalId == branchId) on branch.Id equals priceList.SucursalId into ljPriceList
                 from pList in ljPriceList.DefaultIfEmpty()
                 select new { branch, pList })
                 .Select(x => new Price_Branch
                 {
                     Sucursal = x.branch,
                     PrecioLista = x.pList == null ? null : x.pList.PrecioLista,
                     SucursalId = x.branch.Id
                 })
                .ToListAsync();

            return asignado;
        }

        public async Task<List<Price_Medics>> GetAllMedics(Guid medicsId)
        {
            var asignado = await
                (from medics in _context.CAT_Medicos
                 join priceList in _context.CAT_ListaP_Medicos.Where(x => x.MedicoId == medicsId) on medics.IdMedico equals priceList.MedicoId into ljPriceList
                 from pList in ljPriceList.DefaultIfEmpty()
                 select new { medics, pList })
                 .Select(x => new Price_Medics
                 {
                     MedicoId = x.medics.IdMedico,
                     Medico = x.medics,
                     PrecioLista = x.pList == null ? null : x.pList.PrecioLista,
                 })
                .ToListAsync();

            return asignado;
        }
    }
}
