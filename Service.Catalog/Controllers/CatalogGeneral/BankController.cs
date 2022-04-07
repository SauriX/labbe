using Microsoft.AspNetCore.Mvc;
using Service.Catalog.Dtos.Catalog;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Service.Catalog.Controllers
{
    public partial class CatalogController : ControllerBase
    {

        [HttpGet("bank/all/{search?}")]
        public async Task<IEnumerable<CatalogListDto>> GetAllBank(string search = null)
        {
            return await _bankService.GetAll(search);
        }

        [HttpGet("bank/{id}")]
        public async Task<CatalogFormDto> GetBankById(int id)
        {
            return await _bankService.GetById(id);
        }

        [HttpPost("bank")]
        public async Task CreateBank(CatalogFormDto catalog)
        {
            catalog.UsuarioId = "userId";
            await _bankService.Create(catalog);
        }

        [HttpPut("bank")]
        public async Task UpdateBank(CatalogFormDto catalog)
        {
            catalog.UsuarioId = "userId";
            await _bankService.Update(catalog);
        }
    }
}
