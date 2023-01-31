﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Service.Catalog.Application.IApplication;
using Service.Catalog.Dtos.Configuration;
using Shared.Dictionary;
using System;
using System.Threading.Tasks;

namespace Service.Catalog.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ConfigurationController : ControllerBase
    {
        private readonly IConfigurationApplication _service;

        public ConfigurationController(IConfigurationApplication service)
        {
            _service = service;
        }

        [HttpGet("email")]
        [Authorize(Policies.Access)]
        public async Task<ConfigurationEmailDto> GetEmail()
        {
            return await _service.GetEmail();
        }

        [HttpGet("general")]
        public async Task<ConfigurationGeneralDto> GetGeneral()
        {
            return await _service.GetGeneral();
        }

        [HttpGet("fiscal")]
        [Authorize(Policies.Access)]
        public async Task<ConfigurationFiscalDto> GetFiscal()
        {
            return await _service.GetFiscal();
        }

        [HttpPut("email")]
        [Authorize(Policies.Update)]
        public async Task UpdateEmail(ConfigurationEmailDto email)
        {
            await _service.UpdateEmail(email);
        }

        [HttpPut("general")]
        [Authorize(Policies.Update)]
        public async Task UpdateGeneral([FromForm] ConfigurationGeneralDto general)
        {
            await _service.UpdateGeneral(general);
        }

        [HttpPut("fiscal")]
        [Authorize(Policies.Update)]
        public async Task UpdateFiscal(ConfigurationFiscalDto fiscal)
        {
            fiscal.UsuarioId = (Guid)HttpContext.Items["userId"];
            await _service.UpdateFiscal(fiscal);
        }
    }
}
