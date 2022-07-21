using EFCore.BulkExtensions;
using Microsoft.EntityFrameworkCore;
using Service.Catalog.Context;
using Service.Catalog.Domain.Configuration;
using Service.Catalog.Repository.IRepository;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Service.Catalog.Repository
{
    public class ConfigurationRepository : IConfigurationRepository
    {
        private readonly ApplicationDbContext _context;

        public ConfigurationRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<Configuration>> GetAll()
        {
            return await _context.CAT_Configuracion.ToListAsync();
        }

        public async Task Update(List<Configuration> configuration)
        {
            await _context.BulkInsertOrUpdateAsync(configuration);
            await _context.SaveChangesAsync();
        }
    }
}
