using Microsoft.AspNetCore.Mvc;
using Service.Catalog.Dtos.Catalog;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Service.Catalog.Controllers
{
    public partial class CatalogController : ControllerBase
    {

        [HttpGet("paymentMethod/all/{search?}")]
        public async Task<IEnumerable<CatalogListDto>> GetAllPaymentMethod(string search = null)
        {
            return await _paymentMethodService.GetAll(search);
        }

        [HttpGet("paymentMethod/{id}")]
        public async Task<CatalogFormDto> GetPaymentMethodById(int id)
        {
            return await _paymentMethodService.GetById(id);
        }

        [HttpPost("paymentMethod")]
        public async Task CreatePaymentMethod(CatalogFormDto catalog)
        {
            catalog.UsuarioId = "userId";
            await _paymentMethodService.Create(catalog);
        }

        [HttpPut("paymentMethod")]
        public async Task UpdatePaymentMethod(CatalogFormDto catalog)
        {
            catalog.UsuarioId = "userId";
            await _paymentMethodService.Update(catalog);
        }
    }
}