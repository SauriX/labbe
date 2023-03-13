using EFCore.BulkExtensions;
using Microsoft.EntityFrameworkCore;
using Service.Catalog.Context;
using Service.Catalog.Domain.Price;
using Service.Catalog.Domain.Promotion;
using Service.Catalog.Domain.Reagent;
using Service.Catalog.Repository.IRepository;
using System;
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
            var promotions = _context.CAT_Promocion.Include(x => x.ListaPrecio).AsQueryable();

            search = search.Trim().ToLower();

            if (!string.IsNullOrWhiteSpace(search) && search != "all")
            {
                promotions = promotions.Where(x => x.Clave.ToLower().Contains(search) || x.Nombre.ToLower().Contains(search));
            }

            return await promotions.ToListAsync();
        }

        public async Task<List<Promotion>> GetActive()
        {
            var promotions = await _context.CAT_Promocion.Where(x => x.Activo).ToListAsync();

            return promotions;
        }

        public async Task<Promotion> GetById(int id)
        {
            var promotions = await _context.CAT_Promocion
                .Include(x => x.ListaPrecio)
                .Include(x => x.Sucursales)
                .Include(x => x.Medicos)
                .FirstOrDefaultAsync(x => x.Id == id);

            return promotions;
        }

        public async Task<Promotion> GetStudiesAndPacks(int promotionId)
        {
            var priceList = await _context.CAT_Promocion
                .Include(x => x.Estudios)
                .Include(x => x.Paquetes)
                .FirstOrDefaultAsync(x => x.Id == promotionId);

            return priceList;
        }

        public async Task<List<PromotionStudy>> GetStudyPromos(Guid priceListId, Guid branchId, Guid? doctorId, int studyId)
        {
            var today = DateTime.Today.Date;

            var promo = await
                (from p in _context.CAT_Promocion.Where(x => x.ListaPrecioId == priceListId)
                 join ps in _context.Relacion_Promocion_Estudio.Include(x => x.Promocion).Where(x => x.EstudioId == studyId && x.Activo) on p.Id equals ps.PromocionId
                 join pb in _context.Relacion_Promocion_Sucursal.Where(x => x.SucursalId == branchId) on p.Id equals pb.PromocionId into lfpb
                 from subpb in lfpb.DefaultIfEmpty()
                 join pm in _context.Relacion_Promocion_medicos.Where(x => x.MedicoId == doctorId) on p.Id equals pm.PromocionId into lfpm
                 from subpm in lfpm.DefaultIfEmpty()
                 where p.Activo && p.FechaInicial.Date <= today && p.FechaFinal.Date >= today
                 && ((today.DayOfWeek == DayOfWeek.Monday && ps.Lunes)
                 || (today.DayOfWeek == DayOfWeek.Tuesday && ps.Martes)
                 || (today.DayOfWeek == DayOfWeek.Wednesday && ps.Miercoles)
                 || (today.DayOfWeek == DayOfWeek.Thursday && ps.Jueves)
                 || (today.DayOfWeek == DayOfWeek.Friday && ps.Viernes)
                 || (today.DayOfWeek == DayOfWeek.Saturday && ps.Sabado)
                 || (today.DayOfWeek == DayOfWeek.Sunday && ps.Domingo))
                 orderby subpm.MedicoId descending
                 select ps).ToListAsync();

            return promo;
        }

        public async Task<List<PromotionPack>> GetPackPromos(Guid priceListId, Guid branchId, Guid? doctorId, int packId)
        {
            var today = DateTime.Today.Date;

            var promo = await
                (from p in _context.CAT_Promocion.Where(x => x.ListaPrecioId == priceListId)
                 join pp in _context.Relacion_Promocion_Paquete.Include(x => x.Promocion).Where(x => x.PaqueteId == packId && x.Activo) on p.Id equals pp.PromocionId
                 join pb in _context.Relacion_Promocion_Sucursal.Where(x => x.SucursalId == branchId) on p.Id equals pb.PromocionId into lfpb
                 from subpb in lfpb.DefaultIfEmpty()
                 join pm in _context.Relacion_Promocion_medicos.Where(x => x.MedicoId == doctorId) on p.Id equals pm.PromocionId into lfpm
                 from subpm in lfpm.DefaultIfEmpty()
                 where p.Activo && p.FechaInicial.Date <= today && p.FechaFinal.Date >= today
                 && ((today.DayOfWeek == DayOfWeek.Monday && pp.Lunes)
                 || (today.DayOfWeek == DayOfWeek.Tuesday && pp.Martes)
                 || (today.DayOfWeek == DayOfWeek.Wednesday && pp.Miercoles)
                 || (today.DayOfWeek == DayOfWeek.Thursday && pp.Jueves)
                 || (today.DayOfWeek == DayOfWeek.Friday && pp.Viernes)
                 || (today.DayOfWeek == DayOfWeek.Saturday && pp.Sabado)
                 || (today.DayOfWeek == DayOfWeek.Sunday && pp.Domingo))
                 orderby subpm.MedicoId descending
                 select pp).ToListAsync();

            return promo;
        }

        public async Task<bool> IsDuplicate(Promotion promotion)
        {
            var isDuplicate = await _context.CAT_Promocion.AnyAsync(x => x.Id != promotion.Id && (x.Clave == promotion.Clave || x.Nombre == promotion.Nombre));

            return isDuplicate;
        }

        public async Task Create(Promotion promotion)
        {
            _context.CAT_Promocion.Add(promotion);

            await _context.SaveChangesAsync();
        }

        public async Task Update(Promotion promotion)
        {
            using var transaction = _context.Database.BeginTransaction();

            try
            {
                var branches = promotion.Sucursales.ToList();
                var medics = promotion.Medicos.ToList();
                var studies = promotion.Estudios.ToList();
                var packs = promotion.Paquetes.ToList();

                promotion.Sucursales = null;
                promotion.Paquetes = null;
                promotion.Estudios = null;
                promotion.Medicos = null;

                _context.CAT_Promocion.Update(promotion);
                await _context.SaveChangesAsync();

                var config = new BulkConfig();
                config.SetSynchronizeFilter<PromotionBranch>(x => x.PromocionId == promotion.Id);
                await _context.BulkInsertOrUpdateOrDeleteAsync(branches, config);

                config.SetSynchronizeFilter<PromotionMedic>(x => x.PromocionId == promotion.Id);
                await _context.BulkInsertOrUpdateOrDeleteAsync(medics, config);

                config.SetSynchronizeFilter<PromotionStudy>(x => x.PromocionId == promotion.Id);
                await _context.BulkInsertOrUpdateOrDeleteAsync(studies, config);

                config.SetSynchronizeFilter<PromotionPack>(x => x.PromocionId == promotion.Id);
                await _context.BulkInsertOrUpdateOrDeleteAsync(packs, config);

                transaction.Commit();
            }
            catch (Exception)
            {
                transaction.Rollback();
                throw;
            }
        }
    }
}
