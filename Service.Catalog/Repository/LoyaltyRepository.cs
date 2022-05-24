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
    public class LoyaltyRepository: ILoyaltyRepository
    {
        private readonly ApplicationDbContext _context;

        public LoyaltyRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<Loyalty>> GetAll(string search)
        {
            var loyaltys = _context.CAT_Lealtad.AsQueryable();

            search = search.Trim().ToLower();

            if (!string.IsNullOrWhiteSpace(search) && search != "all")
            {
                loyaltys = loyaltys.Where(x => x.Clave.ToLower().Contains(search) || x.Nombre.ToLower().Contains(search));
            }

            return await loyaltys.ToListAsync();
        }

        public async Task<Loyalty> GetById(Guid Id)
        {
            var loyalty = await _context.CAT_Lealtad.FirstOrDefaultAsync(x => x.Id == Id);

            return loyalty;
        }

        public async Task<bool> IsDuplicate(Loyalty loyalty)
        {
            var isDuplicate = await _context.CAT_Lealtad.AnyAsync(x => x.Id != loyalty.Id && x.Clave == loyalty.Clave || x.Nombre == loyalty.Nombre);

            return isDuplicate;
        }

        public async Task Create(Loyalty loyalty)
        {
            _context.CAT_Lealtad.Add(loyalty);

            await _context.SaveChangesAsync();
        }

        public async Task Update(Loyalty loyalty)
        {
            _context.CAT_Lealtad.Update(loyalty);

            await _context.SaveChangesAsync();
        }
    }
}
