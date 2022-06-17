using Microsoft.Extensions.Configuration;
using Service.Catalog.Application.IApplication;
using Service.Catalog.Dtos.Configuration;
using Service.Catalog.Mapper;
using Service.Catalog.Repository.IRepository;
using Shared.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Service.Catalog.Application
{
    public class ConfigurationApplication : IConfigurationApplication
    {
        private readonly string _key;
        private readonly IConfigurationRepository _repository;

        public ConfigurationApplication(IConfiguration configuration, IConfigurationRepository repository)
        {
            _key = configuration.GetValue<string>("EmailPassKey");
            _repository = repository;
        }

        public async Task<ConfigurationEmailDto> GetEmail(bool pass = false)
        {
            var configuration = await _repository.GetAll();

            return configuration.ToConfigurationEmailDto(pass);
        }

        public async Task<ConfigurationGeneralDto> GetGeneral()
        {
            var configuration = await _repository.GetAll();

            return configuration.ToConfigurationGeneralDto();
        }

        public async Task<ConfigurationFiscalDto> GetFiscal()
        {
            var configuration = await _repository.GetAll();

            return configuration.ToConfigurationFiscalDto();
        }

        public async Task UpdateEmail(ConfigurationEmailDto email)
        {
            var current = await _repository.GetAll();

            var configuration = email.ToModel(current, _key);

            await _repository.Update(configuration);
        }

        public async Task UpdateGeneral(ConfigurationGeneralDto general)
        {
            var current = await _repository.GetAll();

            if (general.Logo != null)
            {
                await general.Logo.SaveFileAsync("wwwroot/images", "logo.png");
            }

            var configuration = general.ToModel(current);

            await _repository.Update(configuration);
        }

        public async Task UpdateFiscal(ConfigurationFiscalDto fiscal)
        {
            var current = await _repository.GetAll();

            var configuration = fiscal.ToModel(current);

            await _repository.Update(configuration);
        }
    }
}
