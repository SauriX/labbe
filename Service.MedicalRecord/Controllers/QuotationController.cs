using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Service.MedicalRecord.Application.IApplication;
using Service.MedicalRecord.Dtos.Quotation;
using Service.MedicalRecord.Dtos.Request;
using Service.MedicalRecord.Dtos.WeeClinic;
using Shared.Dictionary;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Service.MedicalRecord.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class QuotationController : ControllerBase
    {
        private readonly IQuotationApplication _service;

        public QuotationController(IQuotationApplication service)
        {
            _service = service;
        }

        [HttpPost("filter")]
        [Authorize(Policies.Access)]
        public async Task<IEnumerable<QuotationInfoDto>> GetByFilter(QuotationFilterDto filter)
        {
            return await _service.GetByFilter(filter);
        }

        [HttpGet("active")]
        [Authorize(Policies.Access)]
        public async Task<IEnumerable<QuotationInfoDto>> GetActive()
        {
            return await _service.GetActive();
        }

        [HttpGet("{quotationId}")]
        [Authorize(Policies.Access)]
        public async Task<QuotationDto> GetById(Guid quotationId)
        {
            return await _service.GetById(quotationId);
        }

        [HttpGet("general/{quotationId}")]
        [Authorize(Policies.Access)]
        public async Task<QuotationGeneralDto> GetGeneral(Guid quotationId)
        {
            return await _service.GetGeneral(quotationId);
        }

        [HttpGet("studies/{quotationId}")]
        public async Task<QuotationStudyUpdateDto> GetStudies(Guid quotationId)
        {
            return await _service.GetStudies(quotationId);
        }

        [HttpPost("email/{quotationId}")]
        [Authorize(Policies.Mail)]
        public async Task SendTestEmail(Guid quotationId, [FromBody] List<string> emails)
        {
            var quotationDto = new QuotationSendDto
            {
                CotizacionId = quotationId,
                Correos = emails,
                UsuarioId = (Guid)HttpContext.Items["userId"]
            };

            await _service.SendTestEmail(quotationDto);
        }

        [HttpPost("whatsapp/{quotationId}")]
        [Authorize(Policies.Wapp)]
        public async Task SendTestWhatsapp(Guid quotationId, [FromBody] List<string> phones)
        {
            var quotationDto = new QuotationSendDto
            {
                CotizacionId = quotationId,
                Telefonos = phones,
                UsuarioId = (Guid)HttpContext.Items["userId"]
            };

            await _service.SendTestWhatsapp(quotationDto);
        }

        [HttpPost]
        [Authorize(Policies.Create)]
        public async Task<string> Create(QuotationDto quotationDto)
        {
            quotationDto.UsuarioId = (Guid)HttpContext.Items["userId"];
            quotationDto.Usuario = HttpContext.Items["userName"].ToString();

            return await _service.Create(quotationDto);
        }

        [HttpPost("convert/{quotationId}")]
        [Authorize(Policies.Create)]
        public async Task<string> ConvertToRequest(Guid quotationId)
        {
            var userId = (Guid)HttpContext.Items["userId"];
            var userName = HttpContext.Items["userName"].ToString();

            return await _service.ConvertToRequest(quotationId, userId, userName);
        }

        [HttpPut("general")]
        [Authorize(Policies.Update)]
        public async Task UpdateGeneral(QuotationGeneralDto quotationDto)
        {
            quotationDto.UsuarioId = (Guid)HttpContext.Items["userId"];

            await _service.UpdateGeneral(quotationDto);
        }

        [HttpPut("assign/{quotationId}/{recordId?}")]
        [Authorize(Policies.Update)]
        public async Task AssignRecord(Guid quotationId, Guid? recordId = null)
        {
            var userId = (Guid)HttpContext.Items["userId"];

            await _service.AssignRecord(quotationId, recordId, userId);
        }

        [HttpPut("totals")]
        [Authorize(Policies.Update)]
        public async Task UpdateTotals(QuotationTotalDto quotationDto)
        {
            quotationDto.UsuarioId = (Guid)HttpContext.Items["userId"];

            await _service.UpdateTotals(quotationDto);
        }

        [HttpPost("studies")]
        [Authorize(Policies.Create)]
        public async Task<QuotationStudyUpdateDto> UpdateStudies(QuotationStudyUpdateDto quotationDto)
        {
            quotationDto.UsuarioId = (Guid)HttpContext.Items["userId"];

            return await _service.UpdateStudies(quotationDto);
        }

        [HttpPut("cancel/{quotationId}")]
        [Authorize(Policies.Update)]
        public async Task CancelQuotation(Guid quotationId)
        {
            var userId = (Guid)HttpContext.Items["userId"];

            await _service.CancelQuotation(quotationId, userId);
        }

        [HttpDelete("delete/{quotationId}")]
        public async Task DeleteQuotation(Guid quotationId)
        {
            await _service.DeleteQuotation(quotationId);
        }

        [HttpPut("studies/cancel")]
        public async Task DeleteStudies(QuotationStudyUpdateDto quotationDto)
        {
            await _service.DeleteStudies(quotationDto);
        }

        [HttpPost("ticket/{quotationId}")]
        //[Authorize(Policies.Print)]
        public async Task<IActionResult> PrintQuotation(Guid quotationId)
        {
            var file = await _service.PrintQuotation(quotationId);

            return File(file, MimeType.PDF, "quotation.pdf");
        }

        [HttpPost("quote/{id}")]
        //[Authorize(Policies.Print)]
        public async Task<IActionResult> PrintQuote(Guid id)
        {
            var file = await _service.ExportQuote(id);

            return File(file, MimeType.PDF, "Cotización.pdf");
        }
    }
}
