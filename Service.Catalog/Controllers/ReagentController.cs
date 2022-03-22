using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Service.Catalog.Application.IApplication;
using Service.Catalog.Dtos.Reagent;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Service.Catalog.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReagentController : ControllerBase
    {
        private readonly IReagentApplication _service;

        public ReagentController(IReagentApplication service)
        {
            _service = service;
        }

        [HttpGet("all/{search?}")]
        public async Task<IEnumerable<ReagentListDto>> GetAll(string search = null)
        {
            return await _service.GetAll(search);
        }

        [HttpGet("{id}")]
        public async Task<ReagentFormDto> GetById(int id)
        {
            return await _service.GetById(id);
        }

        [HttpPost]
        public async Task Create(ReagentFormDto reagent)
        {
            reagent.UsuarioId = "userId";
            await _service.Create(reagent);
        }

        [HttpPut]
        public async Task Update(ReagentFormDto reagent)
        {
            reagent.UsuarioId = "userId";
            await _service.Update(reagent);
        }
    }
}
