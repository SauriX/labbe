
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Service.MedicalRecord.Application.IApplication;
using Service.MedicalRecord.Dtos.MedicalRecords;
using Service.MedicalRecord.Dtos.PriceQuote;
using Shared.Dictionary;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Service.MedicalRecord.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PriceQuoteController : ControllerBase
    {
        private readonly IPriceQuoteApplication _Service;

        public PriceQuoteController(IPriceQuoteApplication Service)
        {
            _Service = Service;
        }




        [HttpPost("now")]
        //[Authorize(Policies.Access)]
        public async Task<List<PriceQuoteListDto>> GetNow(PriceQuoteSearchDto search = null)
        {
            return await _Service.GetNow(search);
        }

        [HttpGet("active")]
        //[Authorize(Policies.Access)]
        public async Task<List<PriceQuoteListDto>> GetActive()
        {
            return await _Service.GetActive();
        }

        [HttpGet("{id}")]
        //[Authorize(Policies.Access)]
        public async Task<PriceQuoteFormDto> GetById(string id)
        {
            return await _Service.GetById(Guid.Parse(id));
        }
        [HttpPost]
        //[Authorize(Policies.Create)]
        public async Task<PriceQuoteListDto> Create(PriceQuoteFormDto expediente)
        {
            expediente.UserId = Guid.NewGuid();//(Guid)HttpContext.Items["userId"];
            return await _Service.Create(expediente);
        }
        [HttpPut]
        //[Authorize(Policies.Update)]
        public async Task<PriceQuoteListDto> Update(PriceQuoteFormDto expediente)
        {
            expediente.UserId = Guid.NewGuid(); //(Guid)HttpContext.Items["userId"];
            return await _Service.Update(expediente);
        }
        [HttpPost("export/list")]
        //[Authorize(Policies.Download)]
        public async Task<IActionResult> ExportListPriceList(PriceQuoteSearchDto search = null)
        {
            var (file, fileName) = await _Service.ExportList(search);
            return File(file, MimeType.XLSX, fileName);
        }

        [HttpPost("export/form/{id}")]
        //[Authorize(Policies.Download)]
        public async Task<IActionResult> ExportFormPriceList(string id)
        {
            var (file, fileName) = await _Service.ExportForm(Guid.Parse(id));
            return File(file, MimeType.XLSX, fileName);
        }

        [HttpPost("records")]
        public async Task<List<MedicalRecordsListDto>> GetMedicalRecord(PriceQuoteExpedienteSearch search) { 
                return await _Service.GetMedicalRecord(search);
        }
    }
}
