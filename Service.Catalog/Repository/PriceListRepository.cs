using Microsoft.EntityFrameworkCore;
using Service.Catalog.Context;
using Service.Catalog.Domain.Price;
using Service.Catalog.Repository.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Service.Catalog.Repository
{
    public class PriceListRepository : IPriceListRepository
    {
        private readonly ApplicationDbContext _context;

        public PriceListRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<PriceList>> GetAll(string search)
        {
            var indications = _context.CAT_ListaPrecio.AsQueryable();

            search = search.Trim().ToLower();

            if (!string.IsNullOrWhiteSpace(search) && search != "all")
            {
                indications = indications.Where(x => x.Clave.ToLower().Contains(search) || x.Nombre.ToLower().Contains(search));
            }

            return await indications.ToListAsync();
        }

        public async Task<PriceList> GetById(int Id)
        {
            var indication = await _context.CAT_ListaPrecio.FirstOrDefaultAsync(x => x.Id == Id); ;
                //.Include(x => x.Estudios).ThenInclude(x => x.Estudio).ThenInclude(x => x.Area)
                //.FirstOrDefaultAsync(x => x.Id == Id);

           return indication;
        }
        public async Task<List<PriceList>> GetActive()
        {
            var prices = await _context.CAT_ListaPrecio.Where(x => x.Activo).ToListAsync();

            return prices;
        }
        public async Task<bool> IsDuplicate(PriceList price)
        {
            var isDuplicate = await _context.CAT_ListaPrecio.AnyAsync(x => x.Id != price.Id && x.Clave == price.Clave);

            return isDuplicate;
        }

        public async Task Create(PriceList price)
        {
            _context.CAT_ListaPrecio.Add(price);

            await _context.SaveChangesAsync();
        }

        public async Task Update(PriceList price)
        {
            _context.CAT_ListaPrecio.Update(price);

            await _context.SaveChangesAsync();
        }
    }
}
