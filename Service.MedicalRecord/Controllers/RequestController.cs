using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Service.MedicalRecord.Application.IApplication;
using Service.MedicalRecord.Dtos.Request;
using Shared.Dictionary;
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

        [HttpPost("ticket")]
        public async Task<IActionResult> GetTicket()
        {
            var file = await _service.GetTicket();

            return File(file, MimeType.PDF, "ticket.pdf");
        }

        [HttpPost("order")]
        public async Task<IActionResult> GetOrder()
        {
            var file = await _service.GetOrder();

            return File(file, MimeType.PDF, "order.pdf");
        }

        [HttpPost]
        //[Authorize(Policies.Create)]
        public async Task<string> Create(RequestDto request)
        {
            //request.UsuarioId = (Guid)HttpContext.Items["userId"];

            return await _service.Create(request);
        }

        [HttpGet("test/email")]
        [AllowAnonymous]
        public async Task SendTestEmail()
        {
            await _service.SendTestEmail();
        }
    }
}
