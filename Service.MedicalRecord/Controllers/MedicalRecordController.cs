using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Service.MedicalRecord.Application.IApplication;
using Service.MedicalRecord.Dtos;
using Service.MedicalRecord.Dtos.MedicalRecords;
using Service.MedicalRecord.Dtos.Reports;
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
        private readonly IMedicalRecordApplication _service;

        public MedicalRecordController(IMedicalRecordApplication Service)
        {
            _service = Service;
        }

        [HttpGet("all")]
        [Authorize(Policies.Access)]
        public async Task<IEnumerable<MedicalRecordsListDto>> GetAll()
        {
            return await _service.GetAll();
        }

        [HttpPost("now")]
        //[Authorize(Policies.Access)]
        public async Task<List<MedicalRecordsListDto>> GetNow(MedicalRecordSearch search = null)
        {
            return await _service.GetNow(search);
        }

        [HttpPost("report/expedientes")]
        [Authorize(Policies.Access)]
        public async Task<List<MedicalRecordDto>> GetMedicalRecord(List<Guid> records)
        {
            return await _service.GetMedicalRecord(records);
        }

        [HttpPost("coincidencias")]
        [Authorize(Policies.Access)]
        public async Task<List<MedicalRecordsListDto>> GetCoincidencias(MedicalRecordsFormDto expediente)
        {
            return await _service.Coincidencias(expediente);
        }
        

        [HttpGet("active")]
        [Authorize(Policies.Access)]
        public async Task<List<MedicalRecordsListDto>> GetActive()
        {
            return await _service.GetActive();
        }

        [HttpGet("taxData/{recordId}")]
        [Authorize(Policies.Access)]
        public async Task<List<TaxDataDto>> GetTaxData(Guid recordId)
        {
            return await _service.GetTaxData(recordId);
        }

        [HttpGet("{id}")]
        [Authorize(Policies.Access)]
        public async Task<MedicalRecordsFormDto> GetById(string id)
        {
            return await _service.GetById(Guid.Parse(id));
        }

        [HttpPost]
        [Authorize(Policies.Create)]
        public async Task<MedicalRecordsListDto> Create(MedicalRecordsFormDto expediente)
        {
            expediente.UsuarioId = (Guid)HttpContext.Items["userId"];
            return await _service.Create(expediente);
        }

        [HttpPost("taxData")]
        [Authorize(Policies.Create)]
        public async Task<string> CreateTaxData(TaxDataDto taxData)
        {
            taxData.UsuarioId = (Guid)HttpContext.Items["userId"];
            return await _service.CreateTaxData(taxData);
        }

        [HttpPut]
        [Authorize(Policies.Update)]
        public async Task<MedicalRecordsListDto> Update(MedicalRecordsFormDto expediente)
        {
            expediente.UsuarioId = (Guid)HttpContext.Items["userId"];
            return await _service.Update(expediente);
        }

        [HttpPut("observations")]
        [Authorize(Policies.Update)]
        public async Task UpdateObservations(MedicalRecordObservationsDto observation)
        {
            var usuarioId = (Guid)HttpContext.Items["userId"];
            await _service.UpdateObservation(observation);
        }



        [HttpPut("taxData")]
        [Authorize(Policies.Update)]
        public async Task UpdateTaxData(TaxDataDto taxData)
        {
            taxData.UsuarioId = (Guid)HttpContext.Items["userId"];
            await _service.UpdateTaxData(taxData);
        }

        [HttpPut("taxData/default")]
        [Authorize(Policies.Update)]
        public async Task UpdateDefaultTaxData([FromBody] Guid taxDataid)
        {
            await _service.UpdateDefaultTaxData(taxDataid);
        }

        [HttpPost("export/list")]
        [Authorize(Policies.Download)]
        public async Task<IActionResult> ExportListPriceList(MedicalRecordSearch search = null)
        {
            var (file, fileName) = await _service.ExportList(search);
            return File(file, MimeType.XLSX, fileName);
        }

        [HttpPost("export/form/{id}")]
        public async Task<IActionResult> ExportFormPriceList(string id)
        {
            var (file, fileName) = await _service.ExportForm(Guid.Parse(id));
            return File(file, MimeType.XLSX, fileName);
        }
        [HttpPost("updateWallet")]
        public async Task<bool> UpdateWallet(ExpedienteMonederoDto monedero)
        {
             
            return await _service.UpdateWallet(monedero);
        }

    }
}
