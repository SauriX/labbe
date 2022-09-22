
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Service.MedicalRecord.Application.IApplication;
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

        [HttpPut]
      //  [Authorize(Policies.Update)]
        public async Task UpdateStatus(List<RequestedStudyUpdateDto> requestDto)
        {
           // await _service.UpdateStatus(requestDto);
        }
        [HttpPost("export/form/{order}")]
        //[Authorize(Policies.Download)]
        public async Task<IActionResult> ExportFormTrackingOrder(Guid order)
        {
            var (file, fileName) = await _service.ExportForm(order);
            return File(file, MimeType.XLSX, fileName);
        }
    }
}
