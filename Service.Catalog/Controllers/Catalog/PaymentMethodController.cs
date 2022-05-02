using Microsoft.AspNetCore.Authorization;
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
        [HttpGet("paymentMethod/all/{search?}")]
        [Authorize(Policies.Access)]
        public async Task<IEnumerable<CatalogListDto>> GetAllPaymentMethod(string search = null)
        {
            return await _paymentMethodService.GetAll(search);
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
    }
}