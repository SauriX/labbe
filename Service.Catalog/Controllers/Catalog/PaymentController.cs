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
        [HttpGet("payment/all/{search?}")]
        public async Task<IEnumerable<CatalogDescriptionListDto>> GetAllPayment(string search = null)
        {
            return await _paymentService.GetAll(search);
        }

        [HttpGet("payment/{id}")]
        [Authorize(Policies.Access)]
        public async Task<CatalogDescriptionFormDto> GetPaymentById(int id)
        {
            return await _paymentService.GetById(id);
        }

        [HttpPost("payment")]
        [Authorize(Policies.Create)]
        public async Task<CatalogListDto> CreatePayment(CatalogDescriptionFormDto catalog)
        {
            catalog.UsuarioId = (Guid)HttpContext.Items["userId"];
            return await _paymentService.Create(catalog);
        }

        [HttpPut("payment")]
        [Authorize(Policies.Update)]
        public async Task<CatalogListDto> UpdatePayment(CatalogDescriptionFormDto catalog)
        {
            catalog.UsuarioId = (Guid)HttpContext.Items["userId"];
            return await _paymentService.Update(catalog);
        }
    }
}