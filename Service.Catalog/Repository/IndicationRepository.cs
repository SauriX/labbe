using EFCore.BulkExtensions;
using Microsoft.EntityFrameworkCore;
using Service.Catalog.Context;
using Service.Catalog.Domain.Indication;
using Service.Catalog.Repository.IRepository;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Service.Catalog.Repository
{
    public class IndicationRepository : IIndicationRepository
    {
        private readonly ApplicationDbContext _context;

        public IndicationRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Indication> GetById(int Id)
        {
            return await _context.CAT_Indicacion
            .Include(x => x.Estudios)
            .ThenInclude(x => x.Estudio)
            .ThenInclude(x => x.Area)
            .FirstOrDefaultAsync(x => x.Id == Id);
        }

        public async Task<List<Indication>> GetAll(string search)
        {
            var Indications = _context.CAT_Indicacion.AsQueryable();
            search = search.Trim().ToLower();

            if (!string.IsNullOrWhiteSpace(search) && search != "all")
            {
                Indications = Indications.Where(x => x.Clave.ToLower().Contains(search) || x.Nombre.ToLower().Contains(search));
            }

            return await Indications.ToListAsync();
        }

        public async Task<bool> IsDuplicate(Indication indication)
        {
            var isDuplicate = await _context.CAT_Indicacion.AnyAsync(x => x.Id != indication.Id && x.Clave == indication.Clave);

            return isDuplicate;
        }

        public async Task Create(Indication indication)
        {
            using var transaction = _context.Database.BeginTransaction();

            try
            {
                var clinics = indication.Estudios.ToList();

                indication.Estudios = null;
                _context.CAT_Indicacion.Add(indication);

                await _context.SaveChangesAsync();

                clinics.ForEach(x => x.IndicacionId = indication.Id);
                await _context.BulkInsertOrUpdateOrDeleteAsync(clinics);

                transaction.Commit();
            }
            catch (System.Exception)
            {
                transaction.Rollback();
                throw;
            }
        }

        public async Task Update(Indication indicacion)
        {
            var study = indicacion.Estudios.ToList();

            indicacion.Estudios = null;
            _context.CAT_Indicacion.Update(indicacion);

            await _context.BulkInsertOrUpdateOrDeleteAsync(study);

            await _context.SaveChangesAsync();
        }

        public async Task<bool> ValidateClave(string clave)
        {
            var validate = _context.CAT_Indicacion.Where(x => x.Clave == clave).Count();

            if (validate == 0)
            {
                return false;
            }
            else
            {

                return true;
            }

        }

    }
}
