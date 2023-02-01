using Service.Catalog.Domain.Configuration;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Service.Catalog.Repository.IRepository
{
    public interface IConfigurationRepository
    {
        Task<List<Configuration>> GetAll();
        Task Update(List<Configuration> configuration);
        Task CreateTax(TaxConfiguration taxConfiguration);
        Task UpdateTax(TaxConfiguration taxConfiguration);
        Task<TaxConfiguration> GetByTaxId(Guid id);
    }
}
