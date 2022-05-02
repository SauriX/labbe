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
        [HttpGet("delivery/all/{search?}")]
        [Authorize(Policies.Access)]
        public async Task<IEnumerable<CatalogListDto>> GetAllDelivery(string search = null)
        {
            return await _deliveryService.GetAll(search);
        }

        [HttpGet("delivery/{id}")]
        [Authorize(Policies.Access)]
        public async Task<CatalogFormDto> GetDeliveryById(int id)
        {
            return await _deliveryService.GetById(id);
        }

        [HttpPost("delivery")]
        [Authorize(Policies.Create)]
        public async Task<CatalogListDto> CreateDelivery(CatalogFormDto catalog)
        {
            catalog.UsuarioId = (Guid)HttpContext.Items["userId"];
            return await _deliveryService.Create(catalog);
        }

        [HttpPut("delivery")]
        [Authorize(Policies.Update)]
        public async Task<CatalogListDto> UpdateDelivery(CatalogFormDto catalog)
        {
            catalog.UsuarioId = (Guid)HttpContext.Items["userId"];
            return await _deliveryService.Update(catalog);
        }
    }
}
