using Microsoft.AspNetCore.Mvc;
using Service.Catalog.Application.IApplication;
using Service.Catalog.Domain.Constant;
using Service.Catalog.Dtos.Common;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Service.Catalog.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LocationController : ControllerBase
    {
        private readonly ILocationApplication _service;

        public LocationController(ILocationApplication service)
        {
            _service = service;
        }

        [HttpGet("getByZipCode/{zipCode}")]
        public async Task<LocationDto> GetColoniesByZipCode(string zipCode)
        {
            return await _service.GetColoniesByZipCode(zipCode);
        }
        [HttpGet("getcity")]
        public async Task<List<City>> Getcity()
        {
            return await _service.Getcity();
        }
    }
}
