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
                    .ThenInclude(x => x.Estudio)
                    .Include(x => x.Paquete)
                    .Include(x => x.Compañia)
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
                .Include(x => x.Estudios).ThenInclude(x => x.Estudio).ThenInclude(x => x.Area)
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
            var isDuplicate = await _context.CAT_ListaPrecio.AnyAsync(x => x.Id != price.Id && x.Clave == price.Clave || x.Nombre == price.Nombre);

            return isDuplicate;
        }

        public async Task Create(PriceList price)
        {
            _context.CAT_ListaPrecio.Add(price);

            await _context.SaveChangesAsync();
        }

        public async Task Update(PriceList price)
        {
            _context.CAT_ListaPrecio.Update(price);

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
                     Precio = x.pList == null ? null : x.pList.Precio,
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
                     Precio = x.pList == null ? null : x.pList.Precio,
                 })
                .ToListAsync();

            return asignado;
        }

        public async Task<List<Price_Medics>> GetAllMedics(int medicsId)
        {
            var asignado = await
                (from medics in _context.CAT_Medicos
                 join priceList in _context.CAT_ListaP_Medicos.Where(x => x.MedicoId == medicsId) on medics.IdMedico equals priceList.MedicoId into ljPriceList
                 from pList in ljPriceList.DefaultIfEmpty()
                 select new { medics, pList })
                 .Select(x => new Price_Medics
                 {
                     Medico = x.medics,
                     Precio = x.pList == null ? null : x.pList.Precio,
                 })
                .ToListAsync();

            return asignado;
        }
    }
}
