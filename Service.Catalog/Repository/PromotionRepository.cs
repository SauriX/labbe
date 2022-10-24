using EFCore.BulkExtensions;
using Microsoft.EntityFrameworkCore;
using Service.Catalog.Context;
using Service.Catalog.Domain.Price;
using Service.Catalog.Domain.Promotion;
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
            var promotions = _context.CAT_Promocion
                    .Include(x => x.prices)
                    .ThenInclude(x => x.PrecioLista)
                    .AsQueryable();
            if (!string.IsNullOrWhiteSpace(search) && search != "all")
            {
                search = search.Trim().ToLower();
                promotions = promotions.Where(x => x.Clave.ToLower().Contains(search) || x.Nombre.ToLower().Contains(search));
            }
            var listas = _context.CAT_ListaPrecio.AsQueryable();
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
             .ThenInclude(x => x.loyalities)
             .Include(x => x.branches)
             .ThenInclude(x => x.Branch.Departamentos)
             .ThenInclude(x => x.Departamento)
             .Include(x => x.packs)
             .ThenInclude(x => x.Pack.Area.Departamento)
             .Include(x => x.studies)
             .ThenInclude(x => x.Study.Area.Departamento)
             .Include(x => x.medics).ThenInclude(x => x.Medic)
             .AsQueryable()
            .FirstOrDefaultAsync(x => x.Id == id);

            return await promotions;
        }
        public async Task<List<Promotion>> GetActive()
        {
            var promotions = await _context.CAT_Promocion.Where(x => x.Activo).ToListAsync();

            return promotions;
        }

        public async Task<List<PromotionStudy>> GetStudyPromos(Guid priceListId, Guid branchId, Guid? doctorId, int studyId)
        {
            var today = DateTime.Today.Date;

            var promo = await
                (from p in _context.CAT_ListaP_Promocion.Include(x => x.Promocion).Where(x => x.PrecioListaId == priceListId)
                 join ps in _context.Relacion_Promocion_Estudio.Include(x => x.Promotion).Include(x => x.Study).Where(x => x.StudyId == studyId && x.Activo) on p.PromocionId equals ps.PromotionId
                 join pb in _context.Relacion_Promocion_Sucursal.Where(x => x.BranchId == branchId) on p.PromocionId equals pb.PromotionId into lfpb
                 from subpb in lfpb.DefaultIfEmpty()
                 join pm in _context.Relacion_Promocion_medicos.Where(x => x.MedicId == doctorId) on p.PromocionId equals pm.PromotionId into lfpm
                 from subpm in lfpm.DefaultIfEmpty()
                 where p.Activo && p.Promocion.FechaInicio.Date <= today && p.Promocion.FechaFinal.Date >= today
                 && ((today.DayOfWeek == DayOfWeek.Monday && ps.Lunes)
                 || (today.DayOfWeek == DayOfWeek.Tuesday && ps.Martes)
                 || (today.DayOfWeek == DayOfWeek.Wednesday && ps.Miercoles)
                 || (today.DayOfWeek == DayOfWeek.Thursday && ps.Jueves)
                 || (today.DayOfWeek == DayOfWeek.Friday && ps.Viernes)
                 || (today.DayOfWeek == DayOfWeek.Saturday && ps.Sabado)
                 || (today.DayOfWeek == DayOfWeek.Sunday && ps.Domingo))
                 orderby subpm.MedicId descending
                 select ps).ToListAsync();

            return promo;
        }

        public async Task<List<PromotionPack>> GetPackPromos(Guid priceListId, Guid branchId, Guid? doctorId, int packId)
        {
            var today = DateTime.Today.Date;

            var promo = await
                (from p in _context.CAT_ListaP_Promocion.Include(x => x.Promocion).Where(x => x.PrecioListaId == priceListId)
                 join pp in _context.Relacion_Promocion_Paquete.Include(x => x.Promotion).Include(x => x.Pack).Where(x => x.PackId == packId && x.Activo) on p.PromocionId equals pp.PromotionId
                 join pb in _context.Relacion_Promocion_Sucursal.Where(x => x.BranchId == branchId) on p.PromocionId equals pb.PromotionId into lfpb
                 from subpb in lfpb.DefaultIfEmpty()
                 join pm in _context.Relacion_Promocion_medicos.Where(x => x.MedicId == doctorId) on p.PromocionId equals pm.PromotionId into lfpm
                 from subpm in lfpm.DefaultIfEmpty()
                 where p.Activo && p.Promocion.FechaInicio.Date <= today && p.Promocion.FechaFinal.Date >= today
                 && ((today.DayOfWeek == DayOfWeek.Monday && pp.Lunes)
                 || (today.DayOfWeek == DayOfWeek.Tuesday && pp.Martes)
                 || (today.DayOfWeek == DayOfWeek.Wednesday && pp.Miercoles)
                 || (today.DayOfWeek == DayOfWeek.Thursday && pp.Jueves)
                 || (today.DayOfWeek == DayOfWeek.Friday && pp.Viernes)
                 || (today.DayOfWeek == DayOfWeek.Saturday && pp.Sabado)
                 || (today.DayOfWeek == DayOfWeek.Sunday && pp.Domingo))
                 orderby subpm.MedicId descending
                 select pp).ToListAsync();

            return promo;
        }

        public async Task Create(Promotion promotion)
        {

            var lista = _context.CAT_ListaPrecio.Where(x => x.Id == promotion.PrecioListaId);
            promotion.prices = lista.Select(x => new Price_Promotion
            {


                PrecioListaId = x.Id,
                PromocionId = promotion.Id,
                Activo = true,
                Precio = 0,
                UsuarioCreoId = 2,
                FechaCreo = System.DateTime.Now,
                UsuarioModId = promotion.UsuarioCreoId.ToString(),
                FechaMod = System.DateTime.Now,

            }).ToList();
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
            var lista = _context.CAT_ListaPrecio.Where(x => x.Id == promotion.PrecioListaId);
            promotion.prices = lista.Select(x => new Price_Promotion
            {


                PrecioListaId = x.Id,
                PromocionId = promotion.Id,
                Activo = true,
                Precio = 0,
                UsuarioCreoId = 2,
                FechaCreo = System.DateTime.Now,
                UsuarioModId = promotion.UsuarioCreoId.ToString(),
                FechaMod = System.DateTime.Now,

            }).ToList();
            var branches = promotion.branches.ToList();
            var packs = promotion.packs.ToList();
            var studies = promotion.studies.ToList();
            var prices = promotion.prices.ToList();
            var medics = promotion.medics.ToList();
            promotion.branches = null;
            promotion.loyalities = null;
            promotion.prices = null;
            promotion.packs = null;
            promotion.studies = null;
            promotion.medics = null;

            _context.CAT_Promocion.Update(promotion);
            var config = new BulkConfig();
            config.SetSynchronizeFilter<PromotionBranch>(x => x.PromotionId == promotion.Id);
            branches.ForEach(x => x.PromotionId = promotion.Id);
            await _context.BulkInsertOrUpdateOrDeleteAsync(branches, config);

            config.SetSynchronizeFilter<PromotionPack>(x => x.PromotionId == promotion.Id);
            packs.ForEach(x => x.PromotionId = promotion.Id);
            await _context.BulkInsertOrUpdateOrDeleteAsync(packs, config);

            config.SetSynchronizeFilter<PromotionStudy>(x => x.PromotionId == promotion.Id);
            studies.ForEach(x => x.PromotionId = promotion.Id);
            await _context.BulkInsertOrUpdateOrDeleteAsync(studies, config);

            config.SetSynchronizeFilter<Price_Promotion>(x => x.PromocionId == promotion.Id);
            prices.ForEach(x => x.PromocionId = promotion.Id);
            await _context.BulkInsertOrUpdateOrDeleteAsync(prices, config);

            config.SetSynchronizeFilter<PromotionMedics>(x => x.PromotionId == promotion.Id);
            medics.ForEach(x => x.PromotionId = promotion.Id);
            await _context.BulkInsertOrUpdateOrDeleteAsync(medics, config);

            await _context.SaveChangesAsync();
        }

        public async Task<bool> IsDuplicate(Promotion promotion)
        {
            var isDuplicate = await _context.CAT_Promocion.AnyAsync(x => x.Id != promotion.Id && (x.Clave == promotion.Clave || x.Nombre == promotion.Nombre));

            return isDuplicate;
        }

        public async Task<(bool existe, string nombre)> PackIsOnPromotrtion(int id)
        {
            var paquetePromotion = _context.Relacion_Promocion_Paquete.Include(x => x.Pack).Include(x => x.Promotion).AsQueryable();
            var paquetes = _context.CAT_Paquete.AsQueryable();
            var IsOnPromotrtion = await _context.Relacion_Promocion_Paquete.AnyAsync(x => x.PackId == id);
            var nombrePaquete = paquetes.Where(x => x.Id == id).First();
            return (IsOnPromotrtion, nombrePaquete.Nombre);
        }
        public async Task<List<PriceList_Packet>> packsIsPriceList(Guid id)
        {
            return _context.Relacion_ListaP_Paquete.AsQueryable().Where(x => x.PrecioListaId == id).ToList();
        }
        public async Task<(bool existe, string nombre)> PackIsOnInvalidPromotion(int PackId)
        {
            var paquetePromotion = _context.Relacion_Promocion_Paquete.Include(x => x.Pack).AsQueryable();
            var paquete = await paquetePromotion.AnyAsync(x => x.PackId == PackId && x.FechaFinal < DateTime.Now);
            var paquetes = _context.CAT_Paquete.AsQueryable();
            var nombrePaquete = paquetes.Where(x => x.Id == PackId).First();

            return (paquete, nombrePaquete.Nombre);
        }
    }
}
