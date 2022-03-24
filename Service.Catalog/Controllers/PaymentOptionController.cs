using Microsoft.AspNetCore.Mvc;
using Service.Catalog.Dtos.Catalog;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Service.Catalog.Controllers
{
    public partial class CatalogController : ControllerBase
    {

        [HttpGet("paymentOption/all/{search?}")]
        public async Task<IEnumerable<CatalogListDto>> GetAllPaymentOption(string search = null)
        {
            return await _paymentOptionService.GetAll(search);
        }

        [HttpGet("paymentOption/{id}")]
        public async Task<CatalogFormDto> GetPaymentOptionById(int id)
        {
            return await _paymentOptionService.GetById(id);
        }

        [HttpPost("paymentOption")]
        public async Task CreatePaymentOption(CatalogFormDto catalog)
        {
            catalog.UsuarioId = "userId";
            await _paymentOptionService.Create(catalog);
        }

        [HttpPut("paymentOption")]
        public async Task UpdatePaymentOption(CatalogFormDto catalog)
        {
            catalog.UsuarioId = "userId";
            await _paymentOptionService.Update(catalog);
        }
    }
}