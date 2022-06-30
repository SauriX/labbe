using Api.PDF.Service;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web.Http;

namespace Api.PDF.Controllers
{
    [RoutePrefix("api/pdf")]
    public class PdfController : ApiController
    {
        [HttpGet]
        [Route("ticket")]
        public HttpResponseMessage Ticket()
        {
            var file = TicketService.Generate();

            var result = new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new ByteArrayContent(file)
            };

            result.Content.Headers.ContentDisposition =
                new ContentDispositionHeaderValue("attachment")
                {
                    FileName = "ticket.pdf"
                };

            result.Content.Headers.ContentType =
                new MediaTypeHeaderValue("application/pdf");

            return result;
        }
    }
}
