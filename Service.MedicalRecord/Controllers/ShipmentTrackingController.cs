using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Service.MedicalRecord.Application.IApplication;
using Service.MedicalRecord.Dtos.Request;
using Service.MedicalRecord.Dtos.ShipmentTracking;
using Service.MedicalRecord.Dtos.TrackingOrder;
using Shared.Dictionary;
using System;
using System.Threading.Tasks;

namespace Service.MedicalRecord.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ShipmentTrackingController : ControllerBase
    {
        private readonly IShipmentTrackingApplication _service;

        public ShipmentTrackingController(IShipmentTrackingApplication service)
        {
            _service = service;
  
        }

        [HttpGet("order/{id}")]
        //[Authorize(Policies.Access)]
        public async Task<ShipmentTrackingDto> getByid(Guid id)
        {
            var requestedStudy = await _service.getByid(id);
            return requestedStudy;
        }

        [HttpGet("recive/{id}")]
        //[Authorize(Policies.Access)]
        public async Task<ReciveShipmentTracking> getByidRecive(Guid id)
        {
            var requestedStudy = await _service.getByidRecive(id);
            return requestedStudy;
        }
        [HttpGet("{id}")]
        //[Authorize(Policies.Access)]
        public async Task<TrackingOrderFormDto> getorder(Guid id)
        {
            var requestedStudy = await _service.getorder(id);
            return requestedStudy;
        }
        [HttpPut("recive")]
        [Authorize(Policies.Update)]
        public async Task<bool> Updateorder(ReciveShipmentTracking reciveShipment)
        {
            reciveShipment.IdUser = (Guid)HttpContext.Items["userId"];
            await _service.UpdateTracking(reciveShipment);
            return true;
        }
    }
}
