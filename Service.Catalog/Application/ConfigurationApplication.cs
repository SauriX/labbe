﻿using EventBus.Messages.Catalog;
using MassTransit;
using Microsoft.Extensions.Configuration;
using Service.Catalog.Application.IApplication;
using Service.Catalog.Client.IClient;
using Service.Catalog.Dtos.Configuration;
using Service.Catalog.Mapper;
using Service.Catalog.Repository.IRepository;
using Shared.Extensions;
using System;
using System.Threading.Tasks;

namespace Service.Catalog.Application
{
    public class ConfigurationApplication : IConfigurationApplication
    {
        public const int EmailCorreo = 1;
        public const int EmailRemitente = 2;
        public const int EmailSmtp = 3;
        public const int EmailRequiereContraseña = 4;
        public const int EmailContraseña = 5;

        private readonly string _key;
        private readonly ISendEndpointProvider _sendEndpointProvider;
        private readonly IConfigurationRepository _repository;
        private readonly IIdentityClient _identityClient;

        public ConfigurationApplication(IConfiguration configuration, ISendEndpointProvider sendEndpointProvider, IConfigurationRepository repository, IIdentityClient identityClient)
        {
            _key = configuration.GetValue<string>("EmailPassKey");
            _sendEndpointProvider = sendEndpointProvider;
            _repository = repository;
            _identityClient = identityClient;
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

            var conf = configuration.ToConfigurationEmailDto(true);

            var confContract = new EmailConfigurationContract(conf.Correo, conf.Remitente, conf.Smtp, conf.RequiereContraseña, conf.Contraseña);

            var endpoint = await _sendEndpointProvider.GetSendEndpoint(new Uri("rabbitmq://axsishost.online:5672/labramos-dev/email-configuration-queue"));

            await endpoint.Send(confContract);
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

            //var currentTaxConfig = await _repository.GetByTaxId(fiscal.UsuarioId);

            //if(currentTaxConfig != null)
            //{
            //    var taxConfiguration = fiscal.ToModelUpdate(currentTaxConfig);

            //    await _repository.UpdateTax(taxConfiguration);
            //}
            //else
            //{
            //    var taxConfiguration = fiscal.ToModelCreate();

            //    var user = await _identityClient.GetUserById(fiscal.UsuarioId.ToString());
            //    taxConfiguration.SucursalId = user.SucursalId;
            //    taxConfiguration.Nombre = user.Nombre;

            //    await _repository.CreateTax(taxConfiguration);
            //}
        }
    }
}
