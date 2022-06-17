using Service.Catalog.Dtos.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Catalog.Application.IApplication
{
    public interface IConfigurationApplication
    {
        Task<ConfigurationEmailDto> GetEmail(bool pass = false);
        Task<ConfigurationGeneralDto> GetGeneral();
        Task<ConfigurationFiscalDto> GetFiscal();
        Task UpdateEmail(ConfigurationEmailDto email);
        Task UpdateGeneral(ConfigurationGeneralDto general);
        Task UpdateFiscal(ConfigurationFiscalDto fiscal);
    }
}
