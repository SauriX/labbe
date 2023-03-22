using EFCore.BulkExtensions;
using Microsoft.EntityFrameworkCore;
using Service.Catalog.Context;
using Service.Catalog.Domain.Loyalty;
using Service.Catalog.Repository.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Service.Catalog.Repository
{
    public class LoyaltyRepository : ILoyaltyRepository
    {
        private readonly ApplicationDbContext _context;

        public LoyaltyRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<Loyalty>> GetAll(string search)
        {
            var loyaltys = _context.CAT_Lealtad
                .Include(x => x.PrecioLista).ThenInclude(x => x.PrecioLista)
                    .AsQueryable();

            search = search.Trim().ToLower();

            if (!string.IsNullOrWhiteSpace(search) && search != "all")
            {
                loyaltys = loyaltys.Where(x => x.Clave.ToLower().Contains(search) || x.Nombre.ToLower().Contains(search));
            }

            return await loyaltys.ToListAsync();
        }

        public async Task<List<Loyalty>> GetActive()
        {
            var loyalty = await _context.CAT_Lealtad.Where(x => x.Activo)
                .Include(x => x.PrecioLista).ThenInclude(x => x.PrecioLista).ToListAsync();

            return loyalty;
        }

        public async Task<Loyalty> GetById(Guid Id)
        {
            var loyalty = await _context.CAT_Lealtad
                .Include(x => x.PrecioLista).ThenInclude(x => x.PrecioLista)
            .FirstOrDefaultAsync(x => x.Id == Id);

            return loyalty;
        }

        public async Task<Loyalty> GetByDate(DateTime fecha)
        {
            var loyalty = _context.CAT_Lealtad
                .Include(x => x.PrecioLista).ThenInclude(x => x.PrecioLista)
                .AsQueryable();

            loyalty.Where(x => x.FechaInicial <= fecha && fecha <= x.FechaFinal);

            loyalty.Where(x => x.Activo == true);

            var loyalList = await loyalty.FirstOrDefaultAsync();

            return loyalList;
        }

        public async Task<Loyalty> GetByPriceListDate(DateTime date, Guid priceList)
        {
            var loyalty = _context.CAT_Lealtad
                .Include(x => x.PrecioLista).ThenInclude(x => x.PrecioLista)
                .FirstOrDefaultAsync(x => date >= x.FechaInicial && x.FechaFinal >= date && x.PrecioLista.Any(x => x.PrecioListaId == priceList) && x.Activo);

            return await loyalty;
        }

        public async Task<bool> IsDuplicateDate(DateTime fechainicial, DateTime fechafinal, Guid id)
        {
            return await _context.CAT_Lealtad.AnyAsync
                (x =>
                ((x.FechaInicial.Date >= fechainicial.Date && x.FechaInicial.Date <= fechafinal.Date)
                || (x.FechaFinal.Date >= fechainicial.Date && x.FechaFinal.Date <= fechafinal.Date)
                || x.FechaInicial.Date == fechafinal.Date || x.FechaFinal.Date == fechainicial.Date
                ||
                (fechainicial.Date >= x.FechaInicial.Date && fechainicial.Date <= x.FechaFinal.Date)
                && (fechafinal.Date >= x.FechaInicial.Date && fechafinal.Date <= x.FechaFinal.Date))
                && x.Id != id);
        }

        public async Task Create(Loyalty loyalty)
        {
            using var transaction = _context.Database.BeginTransaction();

            try
            {
                var priceLists = loyalty.PrecioLista.ToList();

                loyalty.PrecioLista = null;

                _context.CAT_Lealtad.Add(loyalty);

                await _context.SaveChangesAsync();

                priceLists.ForEach(x => x.LoyaltyId = loyalty.Id);

                var config = new BulkConfig();
                config.SetSynchronizeFilter<LoyaltyPriceList>(x => x.LoyaltyId == loyalty.Id);

                await _context.BulkInsertOrUpdateOrDeleteAsync(priceLists, config);

                transaction.Commit();
            }
            catch (Exception)
            {
                transaction.Rollback();
                throw;
            }
        }

        public async Task Update(Loyalty loyalty)
        {
            using var transaction = _context.Database.BeginTransaction();

            try
            {
                var priceLists = loyalty.PrecioLista.ToList();

                loyalty.PrecioLista = null;

                _context.CAT_Lealtad.Update(loyalty);

                await _context.SaveChangesAsync();

                var config = new BulkConfig();
                config.SetSynchronizeFilter<LoyaltyPriceList>(x => x.LoyaltyId == loyalty.Id);

                await _context.BulkInsertOrUpdateOrDeleteAsync(priceLists, config);

                transaction.Commit();
            }
            catch (Exception)
            {
                transaction.Rollback();
                throw;
            }
        }

        public async Task<bool> IsPorcentaje(Loyalty loyalty)
        {
            var isDuplicate = await _context.CAT_Rutas.AnyAsync(x => x.Id != loyalty.Id && (loyalty.TipoDescuento == "Porcentaje" && loyalty.CantidadDescuento > 100));

            return isDuplicate;
        }

    }
}
