using Microsoft.AspNetCore.Mvc;
using Service.Catalog.Dtos.Catalog;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Service.Catalog.Controllers
{
    public partial class CatalogDescController : ControllerBase
    {

        [HttpGet("paymentOption/all/{search?}")]
        public async Task<IEnumerable<CatalogDescListDto>> GetAllPaymentOption(string search = null)
        {
            return await _paymentOptionService.GetAll(search);
        }

        [HttpGet("paymentOption/{id}")]
        public async Task<CatalogDescFormDto> GetPaymentOptionById(int id)
        {
            return await _paymentOptionService.GetById(id);
        }

        [HttpPost("paymentOption")]
        public async Task CreatePaymentOption(CatalogDescFormDto catalog)
        {
            catalog.UsuarioId = "userId";
            await _paymentOptionService.Create(catalog);
        }

        [HttpPut("paymentOption")]
        public async Task UpdatePaymentOption(CatalogDescFormDto catalog)
        {
            catalog.UsuarioId = "userId";
            await _paymentOptionService.Update(catalog);
        }
    }
}