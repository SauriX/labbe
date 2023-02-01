using EFCore.BulkExtensions;
using Microsoft.EntityFrameworkCore;
using Service.Catalog.Context;
using Service.Catalog.Domain.Configuration;
using Service.Catalog.Repository.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
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

        public async Task<TaxConfiguration> GetByTaxId(Guid id)
        {
            var tax = await _context.CAT_Configuracion_Fiscal.FirstOrDefaultAsync(x => x.UsuarioId == id);

            return tax;
        }

        public async Task Update(List<Configuration> configuration)
        {
            await _context.BulkInsertOrUpdateAsync(configuration);
            await _context.SaveChangesAsync();
        }

        public async Task CreateTax(TaxConfiguration taxConfiguration)
        {
            _context.Add(taxConfiguration);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateTax(TaxConfiguration taxConfiguration)
        {
            _context.Update(taxConfiguration);
            await _context.SaveChangesAsync();
        }
    }
}
