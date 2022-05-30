using Microsoft.EntityFrameworkCore;
using Service.Catalog.Context;
using Service.Catalog.Domain.Tapon;
using Service.Catalog.Repository.IRepository;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Service.Catalog.Repository
{
    public class TaponRepository:ITaponRepository
    {
        private readonly ApplicationDbContext _context;

        public TaponRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<List<Tapon>> GetAll()
        {
            var branchs = _context.CAT_Tipo_Tapon.AsQueryable();

            return await branchs.ToListAsync();
        }
    }
}
