using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Service.MedicalRecord.Application.IApplication;
using Service.MedicalRecord.Client.IClient;
using Service.MedicalRecord.Dtos.Request;
using Shared.Dictionary;
using System;
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

        [HttpPost]
        //[Authorize(Policies.Create)]
        public async Task<string> Create(RequestDto request)
        {
            //request.UsuarioId = (Guid)HttpContext.Items["userId"];

            return await _service.Create(request);
        }
    }
}
