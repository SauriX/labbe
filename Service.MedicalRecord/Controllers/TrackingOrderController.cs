using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Service.MedicalRecord.Application.IApplication;
using Service.MedicalRecord.Dtos.TrackingOrder;
using Shared.Dictionary;
using System;
using System.Threading.Tasks;

namespace Service.MedicalRecord.Controllers
{
    public class TrackingOrderController : ControllerBase
    {
        private readonly ITrackingOrderApplication _service;
        public TrackingOrderController(ITrackingOrderApplication service)
        {
            _service = service;
        }


        [HttpGet("{id}")]
        [Authorize(Policies.Access)]
        public async Task<TrackingOrderDto> GetById(Guid id)
        {
            return await _service.GetById(id);
        }

        [HttpPost]
        [Authorize(Policies.Create)]
        public async Task<TrackingOrderDto> Create(TrackingOrderFormDto order)
        {
            order.UsuarioId = (Guid)HttpContext.Items["userId"];
            return await _service.Create(order);
        }

        [HttpGet("newOrder")]
        [Authorize(Policies.Access)]
        public async Task<TrackingOrderDto> GetTrackingOrder(TrackingOrderFormDto order)
        {
            return await _service.GetTrackingOrder(order);
        }


        [HttpPut]
        [Authorize(Policies.Update)]
        public async Task<TrackingOrderDto> Update(TrackingOrderFormDto order)
        {
            order.UsuarioId = (Guid)HttpContext.Items["userId"];
            return await _service.Update(order);
        }
    }
}
