using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Service.MedicalRecord.Application.IApplication;
using Service.MedicalRecord.Dtos.Quotation;
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

        public const string ENVIAR_CODIGO_NUEVO = "1";
        public const string COMPARAR_CODIGO = "2";
        public const string REENVIAR_CODIGO_VIGENTE = "3";

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

        [HttpGet("email/{quotationId}/{email}")]
        [Authorize(Policies.Mail)]
        public async Task SendTestEmail(Guid quotationId, string email)
        {
            var quotationDto = new QuotationSendDto
            {
                CotizacionId = quotationId,
                Correo = email,
                UsuarioId = (Guid)HttpContext.Items["userId"]
            };

            await _service.SendTestEmail(quotationDto);
        }

        [HttpGet("whatsapp/{quotationId}/{phone}")]
        [Authorize(Policies.Wapp)]
        public async Task SendTestWhatsapp(Guid quotationId, string phone)
        {
            var quotationDto = new QuotationSendDto
            {
                CotizacionId = quotationId,
                Telefono = phone,
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

        [HttpPut("general")]
        [Authorize(Policies.Update)]
        public async Task UpdateGeneral(QuotationGeneralDto quotationDto)
        {
            quotationDto.UsuarioId = (Guid)HttpContext.Items["userId"];

            await _service.UpdateGeneral(quotationDto);
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
        public async Task UpdateStudies(QuotationStudyUpdateDto quotationDto)
        {
            await _service.UpdateStudies(quotationDto);
        }

        [HttpPut("cancel/{quotationId}")]
        [Authorize(Policies.Update)]
        public async Task CancelQuotation(Guid quotationId)
        {
            var userId = (Guid)HttpContext.Items["userId"];

            await _service.CancelQuotation(quotationId, userId);
        }

        [HttpPut("studies/cancel")]
        [Authorize(Policies.Update)]
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
    }
}
