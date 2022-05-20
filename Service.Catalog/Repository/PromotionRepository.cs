using EFCore.BulkExtensions;
using Microsoft.EntityFrameworkCore;
using Service.Catalog.Context;
using Service.Catalog.Domain.Price;
using Service.Catalog.Domain.Promotion;
using Service.Catalog.Repository.IRepository;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Service.Catalog.Repository
{
    public class PromotionRepository : IPromotionRepository
    {
        private readonly ApplicationDbContext _context;

        public PromotionRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<Promotion>> GetAll(string search)
        {
            var promotions = _context.CAT_Promocion
                    .Include(x => x.prices)
                    .ThenInclude(x => x.Precio)
                    .AsQueryable();
            if (!string.IsNullOrWhiteSpace(search) && search != "all")
            {
                search = search.Trim().ToLower();
                promotions= promotions.Where(x => x.Clave.ToLower().Contains(search) || x.Nombre.ToLower().Contains(search));
            }

            return await promotions.ToListAsync();
        }

        public async Task<Promotion> GetById(int id)
        {
            var promotions = _context.CAT_Promocion
                 .Include(x => x.prices)
                 .ThenInclude(x => x.PrecioLista.Paquete)
                 .ThenInclude(x => x.Paquete.Area.Departamento)
                 .Include(x => x.prices)
                 .ThenInclude(x => x.PrecioLista.Estudios)
                 .ThenInclude(x => x.Estudio.Area.Departamento)
                 .Include(x => x.loyalities)
                 .ThenInclude(x => x.Loyality)
                 .Include(x => x.branches)
                 .ThenInclude(x => x.Branch.Departamentos)
                 .ThenInclude(x => x.Departamento)
                 .Include(x => x.packs)
                 .ThenInclude(x => x.Pack.Area.Departamento)
                 .Include(x => x.studies)
                 .ThenInclude(x => x.Study.Area.Departamento)
                 .AsQueryable()
                .FirstOrDefaultAsync(x => x.Id == id);

            return await promotions;
        }

        public async Task Create(Promotion promotion)
        {
            using var transaction = _context.Database.BeginTransaction();
            try
            {
                _context.CAT_Promocion.Add(promotion);
                await _context.SaveChangesAsync();
                transaction.Commit();


            }
            catch (System.Exception)
            {
                transaction.Rollback();
                throw;
            }
        }

        public async Task Update(Promotion promotion)
        {
            var branches = promotion.branches.ToList();
            var loyalitys = promotion.loyalities.ToList();
            var packs = promotion.packs.ToList();
            var studies = promotion.studies.ToList();
            var prices = promotion.prices.ToList();
            promotion.branches = null;
            promotion.loyalities = null;
            promotion.prices = null;
            promotion.packs = null;
            promotion.studies = null;

            _context.CAT_Promocion.Update(promotion);
            var config = new BulkConfig();
            config.SetSynchronizeFilter<PromotionBranch>(x => x.PromotionId == promotion.Id);
            branches.ForEach(x => x.PromotionId = promotion.Id);
            await _context.BulkInsertOrUpdateOrDeleteAsync(branches,config);

            config.SetSynchronizeFilter<PromotionLoyality>(x => x.PromotionId == promotion.Id);
            loyalitys.ForEach(x => x.PromotionId = promotion.Id);
            await _context.BulkInsertOrUpdateOrDeleteAsync(loyalitys,config);

            config.SetSynchronizeFilter<PromotionPack>(x => x.PromotionId == promotion.Id);
            packs.ForEach(x => x.PromotionId = promotion.Id);
            await _context.BulkInsertOrUpdateOrDeleteAsync(packs,config);

            config.SetSynchronizeFilter<PromotionStudy>(x => x.PromotionId == promotion.Id);
            studies.ForEach(x => x.PromotionId = promotion.Id);
            await _context.BulkInsertOrUpdateOrDeleteAsync(studies,config);

            config.SetSynchronizeFilter<Price_Promotion>(x => x.PromocionId == promotion.Id);
            prices.ForEach(x => x.PromocionId = promotion.Id);
            await _context.BulkInsertOrUpdateOrDeleteAsync(prices,config);

            await _context.SaveChangesAsync();
        }

        public async Task<bool> IsDuplicate(Promotion promotion)
        {
            var isDuplicate = await _context.CAT_Promocion.AnyAsync(x => x.Id != promotion.Id && (x.Clave == promotion.Clave || x.Nombre == promotion.Nombre));

            return isDuplicate;
        }
    }
}
