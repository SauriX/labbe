using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Service.Catalog.Application.IApplication;
using Service.Catalog.Domain.Catalog;
using Service.Catalog.Dtos.Catalog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Service.Catalog.Controllers
{
    public partial class CatalogController : ControllerBase
    {
        [HttpGet("delivery/all/{search?}")]
        public async Task<IEnumerable<CatalogListDto>> GetAllDelivery(string search = null)
        {
            return await _deliveryService.GetAll(search);
        }

        [HttpGet("delivery/{id}")]
        public async Task<CatalogFormDto> GetDeliveryById(int id)
        {
            return await _deliveryService.GetById(id);
        }

        [HttpPost("delivery")]
        public async Task CreateDelivery(CatalogFormDto catalog)
        {
            catalog.UsuarioId = "userId";
            await _deliveryService.Create(catalog);
        }

        [HttpPut("delivery")]
        public async Task UpdateDelivery(CatalogFormDto catalog)
        {
            catalog.UsuarioId = "userId";
            await _deliveryService.Update(catalog);
        }
    }
}
