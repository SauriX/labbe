using Microsoft.AspNetCore.Mvc;
using Service.MedicalRecord.Application.IApplication;
using Service.MedicalRecord.Dtos.MedicalRecords;
using Shared.Dictionary;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Service.MedicalRecord.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MedicalRecordController : ControllerBase
    {
        private readonly IMedicalRecordApplication _Service;

        public MedicalRecordController(IMedicalRecordApplication Service)
        {
            _Service = Service;
        }

        [HttpGet("all")]
        public async Task<IEnumerable<MedicalRecordsListDto>> GetAll()
        {
            return await _Service.GetAll();
        }

        [HttpGet("now")]
        public async Task<List<MedicalRecordsListDto>> GetNow() {
            return await _Service.GetNow();
        }

        [HttpGet("active")]
        public async Task<List<MedicalRecordsListDto>> GetActive() {
            return await _Service.GetActive();
        }

        [HttpGet("{id}")]
        public async Task<MedicalRecordsFormDto> GetById(string id) {
            return await _Service.GetById(Guid.Parse(id));        
        }
        [HttpPost]
        public async Task<MedicalRecordsListDto> Create(MedicalRecordsFormDto expediente) {
            expediente.UserId = Guid.NewGuid(); //(Guid)HttpContext.Items["userId"];
            return await _Service.Create(expediente);
        }
        [HttpPut]
        public async Task<MedicalRecordsListDto> Update(MedicalRecordsFormDto expediente) {
            expediente.UserId = Guid.NewGuid(); // (Guid)HttpContext.Items["userId"];
            return await _Service.Update(expediente);
        }
        [HttpPost("export/list")]
       
        public async Task<IActionResult> ExportListPriceList(string search)
        {
            var (file, fileName) = await _Service.ExportList();
            return File(file, MimeType.XLSX, fileName);
        }

    /*    [HttpPost("export/form/{id}")]
        
        public async Task<IActionResult> ExportFormPriceList(string id)
        {
            var (file, fileName) = await _Service.ExportForm(id);
            return File(file, MimeType.XLSX, fileName);
        }
    */
    }
}
