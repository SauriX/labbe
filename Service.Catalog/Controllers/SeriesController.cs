using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Service.Catalog.Application.IApplication;
using Service.Catalog.Dtos.Series;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Service.Catalog.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SeriesController : ControllerBase
    {
        private readonly ISeriesApplication _service;

        public SeriesController(ISeriesApplication service)
        {
            _service = service;
        }

        [HttpGet("branch/{branchId}/{type}")]
        public async Task<IEnumerable<SeriesListDto>> GetByBranch(Guid branchId, byte type)
        {
            var series = await _service.GetByBranch(branchId, type);

            return series;
        }
    }
}
