
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Service.MedicalRecord.Application.IApplication;
using Service.MedicalRecord.Dtos.PendingRecive;
using Service.MedicalRecord.Dtos.RequestedStudy;
using Service.MedicalRecord.Dtos.RouteTracking;
using Shared.Dictionary;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Service.MedicalRecord.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RouteTrackingController: ControllerBase
    {
        private readonly IRouteTrackingApplication _service;

        public RouteTrackingController(IRouteTrackingApplication service)
        {
            _service = service;
           
        }
        [HttpPost("all")]
        [Authorize(Policies.Access)]
        public async Task<List<RouteTrackingListDto>> GetAll(RouteTrackingSearchDto search)
        {
            var requestedStudy = await _service.GetAll(search);
            return requestedStudy;
        }

        [HttpPost("export/form/{order}")]
        //[Authorize(Policies.Download)]
        public async Task<IActionResult> ExportFormTrackingOrder(Guid order)
        {
            var (file, fileName) = await _service.ExportForm(order);
            return File(file, MimeType.XLSX, fileName);
        }
        [HttpPost("exportOrder/{id}")]
     //   [Authorize(Policies.Download)]
        public async Task<IActionResult> ExportDeliverOrder(string id)
        {
            var file = await _service.ExportDeliver(Guid.Parse(id));
            return File(file, MimeType.PDF, "Orden de envio.pdf");
        }
        [HttpPost("allrecive")]
        [Authorize(Policies.Access)]
        public async Task<List<PendingReciveDto>> GetAllRecive(PendingSearchDto search)     {
            var requestedStudy = await _service.GetAllRecive(search);
            return requestedStudy;
        }

        [HttpPost("report")]
        //[Authorize(Policies.Print)]
        public async Task<IActionResult> PrintOrder(PendingSearchDto search)
        {
            var file = await _service.Print(search);

            return File(file, MimeType.PDF, "Pendientes a recibir.pdf");
        }
    }
}
