using Microsoft.EntityFrameworkCore;
using Service.Catalog.Context;
using Service.Catalog.Domain.Maquila;
using Service.Catalog.Repository.IRepository;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Service.Catalog.Repository
{
    public class MaquilaRepository : IMaquilaRepository
    {
        private readonly ApplicationDbContext _context;

        public MaquilaRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<Maquila>> GetAll(string search)
        {
            var maquilas = _context.CAT_Maquilador
                .Include(x => x.Colonia).ThenInclude(x => x.Ciudad).ThenInclude(x => x.Estado)
                .AsQueryable();

            search = search.Trim().ToLower();

            if (!string.IsNullOrWhiteSpace(search) && search != "all")
            {
                maquilas = maquilas.Where(x => x.Clave.ToLower().Contains(search) || x.Nombre.ToLower().Contains(search));
            }

            return await maquilas.ToListAsync();
        }

        public async Task<List<Maquila>> GetActive()
        {
            var maquilas = await _context.CAT_Maquilador.Where(x => x.Activo).ToListAsync();

            return maquilas;
        }

        public async Task<Maquila> GetById(int Id)
        {
            var maquila = await _context.CAT_Maquilador
                .Include(x => x.Colonia).ThenInclude(x => x.Ciudad).ThenInclude(x => x.Estado)
                .FirstOrDefaultAsync(x => x.Id == Id);

            return maquila;
        }

        public async Task<bool> IsDuplicate(Maquila maquila)
        {
            var isDuplicate = await _context.CAT_Maquilador.AnyAsync(x => x.Id != maquila.Id && (x.Clave == maquila.Clave || x.Nombre == maquila.Nombre || x.Correo == maquila.Correo));

            return isDuplicate;
        }

        public async Task Create(Maquila maquila)
        {
            _context.CAT_Maquilador.Add(maquila);
            //
            await _context.SaveChangesAsync();
        }

        public async Task Update(Maquila maquila)
        {
            _context.CAT_Maquilador.Update(maquila);

            await _context.SaveChangesAsync();
        }
    }
}
