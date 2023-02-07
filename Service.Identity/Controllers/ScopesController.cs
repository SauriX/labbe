using Microsoft.AspNetCore.Mvc;
using Service.Identity.Application.IApplication;
using Service.Identity.Dictionary;
using Service.Identity.Dtos.Scopes;
using System;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Service.Identity.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ScopesController : ControllerBase
    {
        private readonly IProfileApplication _service;

        public ScopesController(IProfileApplication service)
        {
            _service = service;
        }

        
        [HttpGet("{scopeController}")]
        public async Task<ScopesDto> GetQuotationScopes(string scopeController)
        {
            var userId = GetUserId();
            return await _service.GetScopes(userId, scopeController);
        }
        private Guid GetUserId()
        {
            return Guid.Parse(User.Claims.SingleOrDefault(x => x.Type == ClaimTypes.NameIdentifier)?.Value);
        }
    }

}
