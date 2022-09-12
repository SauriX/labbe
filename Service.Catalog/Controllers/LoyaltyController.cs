using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Service.Catalog.Application.IApplication;
using Service.Catalog.Dtos.Loyalty;
using Shared.Dictionary;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Service.Catalog.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoyaltyController : ControllerBase
    {
        private readonly ILoyaltyApplication _service;

        public LoyaltyController(ILoyaltyApplication service)
        {
            _service = service;
        }

        [HttpGet("all/{search}")]
        [Authorize(Policies.Access)]
        public async Task<IEnumerable<LoyaltyListDto>> GetAll(string search)
        {
            return await _service.GetAll(search);
        }

        [HttpGet("active")]
        public async Task<IEnumerable<LoyaltyListDto>> GetActive()
        {
            return await _service.GetActive();
        }
        [HttpPost("findByDate")]
        public async Task<LoyaltyListDto> GetByDate([FromBody]DateTime fecha)
        {
            return await _service.GetByDate(fecha);
        }

        [HttpGet("{id}")]
        [Authorize(Policies.Access)]
        public async Task<LoyaltyFormDto> GetById(Guid id)
        {
            return await _service.GetById(id);
        }

        [HttpPost]
        [Authorize(Policies.Create)]
        public async Task<LoyaltyListDto> Create(LoyaltyFormDto loyalty)
        {
            loyalty.UsuarioId = (Guid)HttpContext.Items["userId"];
            return await _service.Create(loyalty);
        }

        [HttpPut]
        [Authorize(Policies.Update)]
        public async Task<LoyaltyListDto> Update(LoyaltyFormDto loyalty)
        {
            loyalty.UsuarioId = (Guid)HttpContext.Items["userId"];
            return await _service.Update(loyalty);
        }

        [HttpPost("export/list/{search}")]
        [Authorize(Policies.Download)]
        public async Task<IActionResult> ExportList(string search)
        {
            var (file, fileName) = await _service.ExportList(search);
            return File(file, MimeType.XLSX, fileName);
        }

        [HttpPost("export/form/{id}")]
        [Authorize(Policies.Download)]
        public async Task<IActionResult> ExportForm(Guid id)
        {
            var (file, fileName) = await _service.ExportForm(id);
            return File(file, MimeType.XLSX, fileName);
        }
        [HttpPost("crearReagendado")]
        [Authorize(Policies.Create)]
        public async Task<LoyaltyListDto> CreateReschedule(LoyaltyFormDto loyalty)
        {
            loyalty.UsuarioId = (Guid)HttpContext.Items["userId"];
            return await _service.CreateReschedule(loyalty);
        }
    }
}
