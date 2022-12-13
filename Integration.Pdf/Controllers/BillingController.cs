using Integration.Pdf.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web.Http;

namespace Integration.Pdf.Controllers
{
    [RoutePrefix("api/billing")]
    public class BillingController : ApiController
    {
        [HttpPost]
        [Route("invoice")]
        public HttpResponseMessage Ticket()
        {
            var file = InvoiceService.Generate();

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
