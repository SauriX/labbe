using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Service.MedicalRecord.Application.IApplication;
using Service.MedicalRecord.Dtos.Appointment;
using Shared.Dictionary;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Service.MedicalRecord.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AppointmentController : ControllerBase
    {
        private readonly IAppointmentApplication _service;

        public AppointmentController(IAppointmentApplication Service)
        {
            _service = Service;
        }
        [HttpPost("getLab")]
        [Authorize(Policies.Access)]
        public async Task<List<AppointmentList>> GetAllLab(SearchAppointment search)
        {
            return await _service.GetAllLab(search);
        }
        [HttpPost("getDom")]
        [Authorize(Policies.Access)]
        public async Task<List<AppointmentList>> GetAllDom(SearchAppointment search)
        {
            return await _service.GetAllDom(search);
        }
        [HttpGet("getLabById/{id}")]
        [Authorize(Policies.Access)]
        public async Task<AppointmentForm> GetByIdLab(string id)
        {
            return await _service.GetByIdLab(id);
        }
        [HttpGet("getDomById/{id}")]
        [Authorize(Policies.Access)]
        public async Task<AppointmentForm> GetByIdDom(string id)
        {
            return await _service.GetByIdDom(id);
        }
        [HttpPost("Lab")]
        [Authorize(Policies.Create)]
        public async Task<AppointmentList> CreateLab(AppointmentForm appointmentLab)
        {

            return await _service.CreateLab(appointmentLab); 
        }
        [HttpPost("Dom")]
        [Authorize(Policies.Create)]
        public async Task<AppointmentList> CreateDom(AppointmentForm appointmentDom)
        {

            return await _service.CreateDom(appointmentDom);
        }
        [HttpPut("Lab")]
        public async Task<AppointmentList> UpdateLab(AppointmentForm appointmentLab)
        {

            return await _service.UpdateLab(appointmentLab);
        }
        [HttpPut("Dom")]
        public async Task<AppointmentList> UpdateDom(AppointmentForm appointmentDom)
        {
            return await _service.UpdateDom(appointmentDom);
        }
        [HttpPost("export/list")]
        //[Authorize(Policies.Download)]
        public async Task<IActionResult> ExportListPriceList(SearchAppointment search)
        {
            var (file, fileName) = await _service.ExportList(search);
            return File(file, MimeType.XLSX, fileName);
        }

        [HttpPost("export/form")]
        //[Authorize(Policies.Download)]
        public async Task<IActionResult> ExportFormPriceList(exportFormDto data)
        {
            var (file, fileName) = await _service.ExportForm(data);
            return File(file, MimeType.XLSX, fileName);
        }
    }
}
