using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Service.Catalog.Application.IApplication;
using Service.Catalog.Dtos.Equipment;
using Shared.Dictionary;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Service.Catalog.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EquipmentController : ControllerBase
    {
        private readonly IEquipmentApplication _service;
        public EquipmentController(IEquipmentApplication service)
        {
            _service = service;
        }

        [HttpGet("all/{search}")]
        [Authorize(Policies.Access)]
        public async Task<IEnumerable<EquipmentListDto>> GetAll(string search)
        {
            return await _service.GetAll(search);
        }

        [HttpGet("{id}")]
        [Authorize(Policies.Access)]
        public async Task<EquipmentFormDto> GetById(int id)
        {
            return await _service.GetById(id);
        }

        [HttpPost]
        [Authorize(Policies.Create)]
        public async Task<EquipmentListDto> Create(EquipmentFormDto equipment)
        {
            equipment.UsuarioId = (Guid)HttpContext.Items["userId"];
            return await _service.Create(equipment);
        }

        [HttpPut]
        [Authorize(Policies.Update)]
        public async Task<EquipmentListDto> Update(EquipmentFormDto equipment)
        {
            equipment.UsuarioId = (Guid)HttpContext.Items["userId"];
            return await _service.Update(equipment);
        }

        [HttpPost("export/list/{search}")]
        [Authorize(Policies.Download)]
        public async Task<IActionResult> ExportListEquipment(string search)
        {
            var (file, fileName) = await _service.ExportList(search);
            return File(file, MimeType.XLSX, fileName);
        }

        [HttpPost("export/form/{id}")]
        [Authorize(Policies.Download)]
        public async Task<IActionResult> ExportFormEquipment(int id)
        {
            var (file, fileName) = await _service.ExportForm(id);
            return File(file, MimeType.XLSX, fileName);
        }
    }
}
