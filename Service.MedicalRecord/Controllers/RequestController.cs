using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Service.MedicalRecord.Application.IApplication;
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
    public class RequestController : ControllerBase
    {
        private readonly IRequestApplication _service;

        public const string ENVIAR_CODIGO_NUEVO = "1";
        public const string COMPARAR_CODIGO = "2";
        public const string REENVIAR_CODIGO_VIGENTE = "3";

        public RequestController(IRequestApplication service)
        {
            _service = service;
        }

        [HttpPost("filter")]
        [Authorize(Policies.Access)]
        public async Task<IEnumerable<RequestInfoDto>> GetByFilter(RequestFilterDto filter)
        {
            filter.SucursalesId = (List<Guid>)HttpContext.Items["sucursales"];
            return await _service.GetByFilter(filter);
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

        [HttpGet("payments/{recordId}/{requestId}")]
        public async Task<IEnumerable<RequestPaymentDto>> GetPayments(Guid recordId, Guid requestId)
        {
            return await _service.GetPayments(recordId, requestId);
        }      
        
        [HttpGet("tags/{recordId}/{requestId}")]
        public async Task<IEnumerable<RequestTagDto>> GetTags(Guid recordId, Guid requestId)
        {
            return await _service.GetTags(recordId, requestId);
        }

        [HttpGet("images/{recordId}/{requestId}")]
        public async Task<IEnumerable<string>> GetImages(Guid recordId, Guid requestId)
        {
            return await _service.GetImages(recordId, requestId);
        }

        [HttpGet("nextPaymentCode/{serie}")]
        public async Task<string> GetNextPaymentNumber(string serie)
        {
            return await _service.GetNextPaymentNumber(serie);
        }

        [HttpPost("email/{recordId}/{requestId}")]
        [Authorize(Policies.Mail)]
        public async Task SendTestEmail(Guid recordId, Guid requestId, [FromBody] List<string> emails)
        {
            var requestDto = new RequestSendDto
            {
                ExpedienteId = recordId,
                SolicitudId = requestId,
                Correos = emails,
                UsuarioId = (Guid)HttpContext.Items["userId"]
            };

            await _service.SendTestEmail(requestDto);
        }

        [HttpPost("whatsapp/{recordId}/{requestId}")]
        [Authorize(Policies.Wapp)]
        public async Task SendTestWhatsapp(Guid recordId, Guid requestId, [FromBody] List<string> phones)
        {
            var requestDto = new RequestSendDto
            {
                ExpedienteId = recordId,
                SolicitudId = requestId,
                Telefonos = phones,
                UsuarioId = (Guid)HttpContext.Items["userId"]
            };

            await _service.SendTestWhatsapp(requestDto);
        }

        [HttpPost]
        [Authorize(Policies.Create)]
        public async Task<string> Create(RequestDto requestDto)
        {
            requestDto.UsuarioId = (Guid)HttpContext.Items["userId"];
            requestDto.Usuario = HttpContext.Items["userName"].ToString();

            return await _service.Create(requestDto);
        }

        [HttpPost("weeClinic")]
        [Authorize(Policies.Create)]
        public async Task<string> CreateWeeClinic(RequestDto requestDto)
        {
            requestDto.UsuarioId = (Guid)HttpContext.Items["userId"];
            requestDto.Usuario = HttpContext.Items["userName"].ToString();

            return await _service.CreateWeeClinic(requestDto);
        }

        [HttpPost("payment")]
        [Authorize(Policies.Create)]
        public async Task<RequestPaymentDto> CreatePayment(RequestPaymentDto requestDto)
        {
            requestDto.UsuarioId = (Guid)HttpContext.Items["userId"];
            requestDto.UsuarioRegistra = HttpContext.Items["userName"].ToString();

            return await _service.CreatePayment(requestDto);
        }

        [HttpPost("payment/checkin")]
        [Authorize(Policies.Create)]
        public async Task<IEnumerable<RequestPaymentDto>> CheckInPayment(RequestCheckInDto checkInDto)
        {
            checkInDto.UsuarioId = (Guid)HttpContext.Items["userId"];
            checkInDto.UsuarioRegistra = HttpContext.Items["userName"].ToString();

            return await _service.CheckInPayment(checkInDto);
        }

        [HttpPut("series")]
        [Authorize(Policies.Update)]
        public async Task<string> UpdateSeries(RequestDto requestDto)
        {
            requestDto.UsuarioId = (Guid)HttpContext.Items["userId"];

            return await _service.UpdateSeries(requestDto);
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
        public async Task<RequestStudyUpdateDto> UpdateStudies(RequestStudyUpdateDto requestDto)
        {
            requestDto.UsuarioId = (Guid)HttpContext.Items["userId"];

            return await _service.UpdateStudies(requestDto);
        }

        [HttpPost("tags/{recordId}/{requestId}")]
        [Authorize(Policies.Create)]
        public async Task<List<RequestTagDto>> UpdateTags(Guid recordId, Guid requestId, [FromBody] List<RequestTagDto> tagsDto)
        {
            var userId = (Guid)HttpContext.Items["userId"];

            foreach (var tagDto in tagsDto)
            {
                tagDto.UsuarioId = userId;
            }

            return await _service.UpdateTags(recordId, requestId, tagsDto);
        }

        [HttpPut("cancel/{recordId}/{requestId}")]
        [Authorize(Policies.Update)]
        public async Task CancelRequest(Guid recordId, Guid requestId)
        {
            var userId = (Guid)HttpContext.Items["userId"];

            await _service.CancelRequest(recordId, requestId, userId);
        }

        [HttpDelete("delete/{recordId}/{requestId}")]
        public async Task DeleteRequest(Guid recordId, Guid requestId)
        {
            await _service.DeleteRequest(recordId, requestId);
        }

        [HttpPut("studies/cancel")]
        [Authorize(Policies.Update)]
        public async Task CancelStudies(RequestStudyUpdateDto requestDto)
        {
            await _service.CancelStudies(requestDto);
        }

        [HttpPut("payment/cancel/{recordId}/{requestId}")]
        [Authorize(Policies.Update)]
        public async Task<List<RequestPaymentDto>> CancelPayment(Guid recordId, Guid requestId, List<RequestPaymentDto> paymentsDto)
        {
            foreach (var paymentDto in paymentsDto)
            {
                paymentDto.UsuarioId = (Guid)HttpContext.Items["userId"];
                paymentDto.UsuarioRegistra = HttpContext.Items["userName"].ToString();
            }

            return await _service.CancelPayment(recordId, requestId, paymentsDto);
        }

        [HttpPut("studies/sampling")]
        [Authorize(Policies.Update)]
        public async Task SendStudiesToSampling(RequestStudyUpdateDto requestDto)
        {
            var userId = (Guid)HttpContext.Items["userId"];
            var userName = HttpContext.Items["userName"].ToString();

            requestDto.UsuarioId = userId;
            requestDto.Usuario = userName;

            await _service.SendStudiesToSampling(requestDto);
        }

        [HttpPut("studies/request")]
        [Authorize(Policies.Update)]
        public async Task SendStudiesToRequest(RequestStudyUpdateDto requestDto)
        {
            var userId = (Guid)HttpContext.Items["userId"];
            var userName = HttpContext.Items["userName"].ToString();

            requestDto.UsuarioId = userId;
            requestDto.Usuario = userName;

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
        [Authorize(Policies.Download)]
        public async Task<IActionResult> PrintTicket(Guid recordId, Guid requestId)
        {
            var userName = HttpContext.Items["userName"].ToString();

            var file = await _service.PrintTicket(recordId, requestId, userName);

            return File(file, MimeType.PDF, "ticket.pdf");
        }

        [HttpPost("order/{recordId}/{requestId}")]
        [Authorize(Policies.Download)]
        public async Task<IActionResult> PrintOrder(Guid recordId, Guid requestId)
        {
            var userName = HttpContext.Items["userName"].ToString();

            var file = await _service.PrintOrder(recordId, requestId, userName);

            return File(file, MimeType.PDF, "order.pdf");
        }

        [HttpPost("print/tags/{recordId}/{requestId}")]
        [Authorize(Policies.Download)]
        public async Task<IActionResult> PrintTags(Guid recordId, Guid requestId, List<RequestTagDto> tags)
        {
            var file = await _service.PrintTags(recordId, requestId, tags);

            return File(file, MimeType.PDF, "tags.pdf");
        }

        [HttpPut("images")]
        [Authorize(Policies.Update)]
        public async Task<string> SaveImage([FromForm] RequestImageDto requestDto)
        {
            requestDto.UsuarioId = (Guid)HttpContext.Items["userId"];

            return await _service.SaveImage(requestDto);
        }

        [HttpDelete("image/{recordId}/{requestId}/{code}")]
        public async Task DeleteImage(Guid recordId, Guid requestId, string code)
        {
            await _service.DeleteImage(recordId, requestId, code);
        }

        [HttpPost("wee/sendToken")]
        [Authorize(Policies.Update)]
        public async Task<WeeTokenValidationDto> SendWeeToken(RequestTokenDto requestDto)
        {
            return await _service.SendCompareToken(requestDto, requestDto.Reenviar ? REENVIAR_CODIGO_VIGENTE : ENVIAR_CODIGO_NUEVO);
        }

        [HttpPost("wee/compareToken")]
        [Authorize(Policies.Update)]
        public async Task<WeeTokenValidationDto> CompareWeeToken(RequestTokenDto requestDto)
        {
            return await _service.SendCompareToken(requestDto, COMPARAR_CODIGO);
        }

        [HttpPost("wee/verifyToken")]
        [Authorize(Policies.Update)]
        public async Task<WeeTokenVerificationDto> VerifyWeeToken(RequestTokenDto requestDto)
        {
            requestDto.UsuarioId = (Guid)HttpContext.Items["userId"];

            return await _service.VerifyWeeToken(requestDto);
        }

        [HttpPut("wee/assignServices/{recordId}/{requestId}")]
        [Authorize(Policies.Update)]
        public async Task<List<WeeServiceAssignmentDto>> AssignWeeServices(Guid recordId, Guid requestId)
        {
            var userId = (Guid)HttpContext.Items["userId"];

            return await _service.AssignWeeServices(recordId, requestId, userId);
        }
    }
}
