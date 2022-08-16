using Microsoft.EntityFrameworkCore;
using Service.Catalog.Context;
using Service.Catalog.Domain.Constant;
using Service.Catalog.Repository.IRepository;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Service.Catalog.Repository
{
    public class LocationRepository : ILocationRepository
    {
        private readonly ApplicationDbContext _context;

        public LocationRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<Colony>> GetColoniesByZipCode(string zipCode)
        {
            var colonies = await _context.CAT_Colonia
                .Include(x => x.Ciudad).ThenInclude(x => x.Estado)
                .Where(x => x.CodigoPostal == zipCode).ToListAsync();

            return colonies;
        }

        public async Task<City> GetCityByName(string cityName)
        {
            var city = await _context.CAT_Ciudad.FirstOrDefaultAsync(x => x.Ciudad == cityName);

            return city;
        }

        public async Task<List<City>> GetCities()
        {
            var cities = await _context.CAT_Ciudad.ToListAsync();
            return cities;
        }
    }
}
