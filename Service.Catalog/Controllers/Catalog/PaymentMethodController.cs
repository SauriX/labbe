﻿using Microsoft.AspNetCore.Mvc;
using Service.Catalog.Dtos.Catalog;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Service.Catalog.Controllers.Catalog
{
    public partial class CatalogController : ControllerBase
    {
        [HttpGet("paymentMethod/all/{search?}")]
        public async Task<IEnumerable<CatalogListDto>> GetAllPaymentMethod(string search = null)
        {
            return await _paymentMethodService.GetAll(search);
        }

        [HttpGet("paymentMethod/active")]
        public async Task<IEnumerable<CatalogListDto>> GetActivePaymentMethod(int id)
        {
            return await _paymentMethodService.GetActive();
        }

        [HttpGet("paymentMethod/{id}")]
        public async Task<CatalogFormDto> GetPaymentMethodById(int id)
        {
            return await _paymentMethodService.GetById(id);
        }

        [HttpPost("paymentMethod")]
        public async Task<CatalogListDto> CreatePaymentMethod(CatalogFormDto catalog)
        {
            catalog.UsuarioId = "userId";
            return await _paymentMethodService.Create(catalog);
        }

        [HttpPut("paymentMethod")]
        public async Task<CatalogListDto> UpdatePaymentMethod(CatalogFormDto catalog)
        {
            catalog.UsuarioId = "userId";
            return await _paymentMethodService.Update(catalog);
        }
    }
}