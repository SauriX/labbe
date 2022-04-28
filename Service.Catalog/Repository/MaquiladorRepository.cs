using Microsoft.EntityFrameworkCore;
using Service.Catalog.Context;
using Service.Catalog.Domain.Maquilador;
using Service.Catalog.Repository.IRepository;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Service.Catalog.Repository
{
    public class MaquiladorRepository : IMaquiladorRepository
    {
        private readonly ApplicationDbContext _context;

        public MaquiladorRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        //public async Task<List<Maquilador>> GetActive()
        //{
        //    var catalogs = _context.CAT_Maquilador.Where(x => x.Activo);

        //    return await catalogs.ToListAsync();
        //}

        public async Task<Maquilador> GetById(int Id)
        {
            return await _context.CAT_Maquilador.FindAsync(Id);
        }

        public async Task<List<Maquilador>> GetAll(string search)
        {
            var Maqui = _context.CAT_Maquilador.AsQueryable();
            search = search.Trim().ToLower();

            if (!string.IsNullOrWhiteSpace(search) && search != "all")
            {
                Maqui = Maqui.Where(x => x.Clave.ToLower().Contains(search) || x.Nombre.ToLower().Contains(search));
            }

            return await Maqui.ToListAsync();
        }

        public async Task Create(Maquilador maqui)
        {
           
            _context.CAT_Maquilador.Add(maqui);

            await _context.SaveChangesAsync();

          
        }

        public async Task Update(Maquilador maqui)
        {
           
            _context.CAT_Maquilador.Update(maqui);

            await _context.SaveChangesAsync();
        }

        public async Task<bool> ValidateClaveName(string clave, string nombre)
        {
            var validate = _context.CAT_Maquilador.Where(x => x.Clave == clave || x.Nombre == nombre).Count();

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
