using Microsoft.AspNetCore.Mvc;
using Service.MedicalRecord.Application.IApplication;
using Service.MedicalRecord.Dtos.MedicalRecords;
using Service.MedicalRecord.Dtos.PriceQuote;
using Service.MedicalRecord.Dtos.Request;
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
        private readonly IRequestApplication _ServiceRequest;
        public PriceQuoteController(IPriceQuoteApplication Service, IRequestApplication request)
        {
            _Service = Service;
            _ServiceRequest = request;
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

        [HttpPost("solicitud")]
        //[Authorize(Policies.Create)]
        public async Task<string> CreateSolicitud(RequestConvertDto request)
        {
            //request.UsuarioId = (Guid)HttpContext.Items["userId"];
            var id = await _ServiceRequest.Convert(request);
            //await _Service.convert
            return id;
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
        public async Task<List<MedicalRecordsListDto>> GetMedicalRecord(PriceQuoteExpedienteSearch search)
        {
            return await _Service.GetMedicalRecord(search);
        }

        [HttpPost("ticket")]
        public async Task<IActionResult> GetTicket()
        {
            var file = await _Service.GetTicket();

            return File(file, MimeType.PDF, "ticket.pdf");
        }
    }
}
