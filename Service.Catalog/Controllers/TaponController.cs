using Microsoft.AspNetCore.Mvc;
using Service.Catalog.Application.IApplication;
using Service.Catalog.Domain.Tapon;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Service.Catalog.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TaponController
    {
        private readonly ITaponApplication _Service;

        public TaponController(ITaponApplication Service)
        {
            _Service = Service;
        }

        [HttpGet("all")]
        public async Task<IEnumerable<Tapon>> GetAll()
        {
            return await _Service.GetAll();
        }
    }
}
