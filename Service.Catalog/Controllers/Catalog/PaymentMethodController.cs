﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Service.Catalog.Dtos.Catalog;
using Shared.Dictionary;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Service.Catalog.Controllers.Catalog
{
    public partial class CatalogController : ControllerBase
    {
        [HttpGet("paymentMethod/all/{search}")]
        [Authorize(Policies.Access)]
        public async Task<IEnumerable<CatalogListDto>> GetAllPaymentMethod(string search)
        {
            return await _paymentMethodService.GetAll(search);
        }

        [HttpGet("paymentMethod/active")]
        public async Task<IEnumerable<CatalogListDto>> GetActivePaymentMethod()
        {
            return await _paymentMethodService.GetActive();
        }

        [HttpGet("paymentMethod/{id}")]
        [Authorize(Policies.Access)]
        public async Task<CatalogFormDto> GetPaymentMethodById(int id)
        {
            return await _paymentMethodService.GetById(id);
        }

        [HttpPost("paymentMethod")]
        [Authorize(Policies.Create)]
        public async Task<CatalogListDto> CreatePaymentMethod(CatalogFormDto catalog)
        {
            catalog.UsuarioId = (Guid)HttpContext.Items["userId"];
            return await _paymentMethodService.Create(catalog);
        }

        [HttpPut("paymentMethod")]
        [Authorize(Policies.Update)]
        public async Task<CatalogListDto> UpdatePaymentMethod(CatalogFormDto catalog)
        {
            catalog.UsuarioId = (Guid)HttpContext.Items["userId"];
            return await _paymentMethodService.Update(catalog);
        }

        [HttpPost("paymentMethod/export/list/{search}")]
        [Authorize(Policies.Download)]
        public async Task<IActionResult> ExportListPaymentMethod(string search)
        {
            var file = await _paymentMethodService.ExportList(search, "Métodos de pago");
            return File(file, MimeType.XLSX, "Catálogo de Métodos de pago.xlsx");
        }

        [HttpPost("paymentMethod/export/form/{id}")]
        [Authorize(Policies.Download)]
        public async Task<IActionResult> ExportFormPaymentMethod(int id)
        {
            var (file, code) = await _paymentMethodService.ExportForm(id, "Métodos de pago");
            return File(file, MimeType.XLSX, $"Catálogo de Método de pago ({code}).xlsx");
        }
    }
}