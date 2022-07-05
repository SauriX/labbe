using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Service.MedicalRecord.Client.IClient;
using Shared.Dictionary;
using System.Threading.Tasks;

namespace Service.MedicalRecord.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SolicitudController : ControllerBase
    {
        private readonly IPdfClient _pdfCliente;

        public SolicitudController(IPdfClient pdfCliente)
        {
            _pdfCliente = pdfCliente;
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> GenerarTicket()
        {
            var pdf = await _pdfCliente.GenerarTicket();

            return File(pdf, MimeType.PDF, "ticket.pdf");
        }
    }
}
