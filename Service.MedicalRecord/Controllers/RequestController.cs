using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Service.MedicalRecord.Application.IApplication;
using Service.MedicalRecord.Dtos.Request;
using Shared.Dictionary;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Service.MedicalRecord.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RequestController : ControllerBase
    {
        private readonly IRequestApplication _service;

        public RequestController(IRequestApplication service)
        {
            _service = service;
        }

        [HttpGet("{recordId}/{requestId}")]
        [Authorize(Policies.Access)]
        public async Task<RequestDto> GetById(Guid recordId, Guid requestId)
        {
            return await _service.GetById(recordId, requestId);
        }

        [HttpGet("general/{recordId}/{requestId}")]
        [Authorize(Policies.Access)]
        public async Task<RequestGeneralDto> GetGeneral(Guid recordId, Guid requestId)
        {
            return await _service.GetGeneral(recordId, requestId);
        }

        [HttpGet("studies/{recordId}/{requestId}")]
        public async Task<RequestStudyUpdateDto> GetStudies(Guid recordId, Guid requestId)
        {
            return await _service.GetStudies(recordId, requestId);
        }

        [HttpGet("email/{recordId}/{requestId}/{email}")]
        [Authorize(Policies.Mail)]
        public async Task SendTestEmail(Guid recordId, Guid requestId, string email)
        {
            var requestDto = new RequestSendDto
            {
                ExpedienteId = recordId,
                SolicitudId = requestId,
                Telefono = email,
                UsuarioId = (Guid)HttpContext.Items["userId"]
            };

            await _service.SendTestEmail(requestDto);
        }

        [HttpGet("whatsapp/{recordId}/{requestId}/{phone}")]
        [Authorize(Policies.Wapp)]
        public async Task SendTestWhatsapp(Guid recordId, Guid requestId, string phone)
        {
            var requestDto = new RequestSendDto
            {
                ExpedienteId = recordId,
                SolicitudId = requestId,
                Telefono = phone,
                UsuarioId = (Guid)HttpContext.Items["userId"]
            };

            await _service.SendTestWhatsapp(requestDto);
        }

        [HttpPost]
        [Authorize(Policies.Create)]
        public async Task<string> Create(RequestDto requestDto)
        {
            requestDto.UsuarioId = (Guid)HttpContext.Items["userId"];

            return await _service.Create(requestDto);
        }

        [HttpPut("general")]
        [Authorize(Policies.Update)]
        public async Task UpdateGeneral(RequestGeneralDto requestDto)
        {
            requestDto.UsuarioId = (Guid)HttpContext.Items["userId"];

            await _service.UpdateGeneral(requestDto);
        }

        [HttpPut("totals")]
        [Authorize(Policies.Update)]
        public async Task UpdateTotals(RequestTotalDto requestDto)
        {
            requestDto.UsuarioId = (Guid)HttpContext.Items["userId"];

            await _service.UpdateTotals(requestDto);
        }

        [HttpPost("studies")]
        [Authorize(Policies.Create)]
        public async Task UpdateStudies(RequestStudyUpdateDto requestDto)
        {
            await _service.UpdateStudies(requestDto);
        }

        [HttpPut("studies/cancel")]
        [Authorize(Policies.Update)]
        public async Task CancelStudies(RequestStudyUpdateDto requestDto)
        {
            await _service.CancelStudies(requestDto);
        }

        [HttpPut("studies/sampling")]
        [Authorize(Policies.Update)]
        public async Task SendStudiesToSampling(RequestStudyUpdateDto requestDto)
        {
            await _service.SendStudiesToSampling(requestDto);
        }

        [HttpPut("studies/request")]
        [Authorize(Policies.Update)]
        public async Task SendStudiesToRequest(RequestStudyUpdateDto requestDto)
        {
            await _service.SendStudiesToRequest(requestDto);
        }

        [HttpPut("partiality")]
        [Authorize(Policies.Update)]
        public async Task AddPartiality(RequestPartialityDto requestDto)
        {
            requestDto.UsuarioId = (Guid)HttpContext.Items["userId"];

            await _service.AddPartiality(requestDto);
        }

        [HttpPost("ticket/{recordId}/{requestId}")]
        //[Authorize(Policies.Print)]
        public async Task<IActionResult> PrintTicket(Guid recordId, Guid requestId)
        {
            var file = await _service.PrintTicket(recordId, requestId);

            return File(file, MimeType.PDF, "ticket.pdf");
        }

        [HttpPost("order/{recordId}/{requestId}")]
        //[Authorize(Policies.Print)]
        public async Task<IActionResult> PrintOrder(Guid recordId, Guid requestId)
        {
            var file = await _service.PrintOrder(recordId, requestId);

            return File(file, MimeType.PDF, "order.pdf");
        }

        [HttpPut("images")]
        [Authorize(Policies.Update)]
        public async Task SaveImage([FromForm] RequestImageDto requestDto)
        {
            requestDto.UsuarioId = (Guid)HttpContext.Items["userId"];

            await _service.SaveImage(requestDto);
        }
    }
}
