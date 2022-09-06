﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Service.MedicalRecord.Application.IApplication;
using Service.MedicalRecord.Dtos.TrackingOrder;
using Shared.Dictionary;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Service.MedicalRecord.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TrackingOrderController : ControllerBase
    {
        private readonly ITrackingOrderApplication _service;
        public TrackingOrderController(ITrackingOrderApplication service)
        {
            _service = service;
        }


        

        [HttpPost]
        [Authorize(Policies.Create)]
        public async Task<TrackingOrderDto> Create(TrackingOrderFormDto order)
        {
            order.UsuarioId = (Guid)HttpContext.Items["userId"];
            return await _service.Create(order);
        }

        [HttpPost("findStudies")]
        [Authorize(Policies.Access)]
        public async Task<IEnumerable<EstudiosListDto>> FindEstudios(List<int> estudios)
        {
            return await _service.FindEstudios(estudios);
        }

        [HttpPost("confirmarRecoleccion")]
        [Authorize(Policies.Access)]
        public async Task<bool> ConfirmarRecoleccion([FromBody] string seguimientoId)
        {
            return await _service.ConfirmarRecoleccion(Guid.Parse(seguimientoId));
        }

        [HttpPost("cancelarRecoleccion")]
        [Authorize(Policies.Access)]
        public async Task<bool> CancelarRecoleccion([FromBody] string seguimientoId)
        {
            return await _service.CancelarRecoleccion(Guid.Parse(seguimientoId));
        }


        [HttpPost("export/form")]
        //[Authorize(Policies.Download)]
        public async Task<IActionResult> ExportFormTrackingOrder(TrackingOrderFormDto order)
        {
            var (file, fileName) = await _service.ExportForm(order);
            return File(file, MimeType.XLSX, fileName);
        }
    }
}
