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

        public async Task<List<Indication>> GetAll(string search)
        {
            var indications = _context.CAT_Indicacion.AsQueryable();

            search = search.Trim().ToLower();

            if (!string.IsNullOrWhiteSpace(search) && search != "all")
            {
                indications = indications.Where(x => x.Clave.ToLower().Contains(search) || x.Nombre.ToLower().Contains(search));
            }

            return await indications.ToListAsync();
        }

        public async Task<Indication> GetById(int Id)
        {
            var indication = await _context.CAT_Indicacion
                .Include(x => x.Estudios).ThenInclude(x => x.Estudio).ThenInclude(x => x.Area)
                .FirstOrDefaultAsync(x => x.Id == Id);

            return indication;
        }

        public async Task<bool> IsDuplicate(Indication indication)
        {
            var isDuplicate = await _context.CAT_Indicacion.AnyAsync(x => x.Id != indication.Id && x.Clave == indication.Clave || x.Nombre == indication.Nombre);

            return isDuplicate;
        }

        public async Task Create(Indication indication)
        {
            _context.CAT_Indicacion.Add(indication);

            await _context.SaveChangesAsync();
        }

        public async Task Update(Indication indicacion)
        {
            _context.CAT_Indicacion.Update(indicacion);

            await _context.SaveChangesAsync();
        }
    }
}
