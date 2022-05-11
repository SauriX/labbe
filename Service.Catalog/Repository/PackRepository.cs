using Microsoft.EntityFrameworkCore;
using Service.Catalog.Context;
using Service.Catalog.Domain.Packet;
using Service.Catalog.Repository.IRepository;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Service.Catalog.Repository
{
    public class PackRepository:IPackRepository
    {
        private readonly ApplicationDbContext _context;

        public PackRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<Packet>> GetAll(string search)
        {
            var Packs = _context.CAT_Paquete
                    .Include(x => x.Area)
                    .ThenInclude(x => x.Departamento)
                    .Include(x => x.studies)
                    .ThenInclude(x=>x.Estudio)
                    .AsQueryable();



            if (!string.IsNullOrWhiteSpace(search) && search != "all")
            {
                search = search.Trim().ToLower();
                Packs = Packs.Where(x => x.Clave.ToLower().Contains(search) || x.Nombre.ToLower().Contains(search));
            }

            return await Packs.ToListAsync();
        }
    }
}
