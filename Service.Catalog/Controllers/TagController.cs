using Microsoft.AspNetCore.Mvc;
using Service.Catalog.Application.IApplication;
using Service.Catalog.Domain.Tapon;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Service.Catalog.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TagController
    {
        private readonly ITagApplication _Service;

        public TagController(ITagApplication Service)
        {
            _Service = Service;
        }

        [HttpGet("all")]
        public async Task<IEnumerable<Tag>> GetAll()
        {
            return await _Service.GetAll();
        }
    }
}
