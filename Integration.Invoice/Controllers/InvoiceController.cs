using Integration.Invoice.Dtos;
using Integration.Invoice.Service;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web.Http;

namespace Integration.Invoice.Controllers
{
    [RoutePrefix("api/invoice")]
    public class InvoiceController : ApiController
    {
        [HttpPost]
        [Route("")]
        public async Task<object> Ticket(FacturapiDto invoiceDto)
        {
            var file = await InvoiceService.Create(invoiceDto);
            return file;
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<object> GetTicket(string id)
        {
            var file = await InvoiceService.GetById(id);
            return file;
        }

        [HttpGet]
        [Route("xml/{id}")]
        public async Task<HttpResponseMessage> Tag(string id)
        {
            var file = await InvoiceService.GetXml(id);

            var result = new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new ByteArrayContent(file)
            };

            result.Content.Headers.ContentDisposition =
                new ContentDispositionHeaderValue("attachment")
                {
                    FileName = "order.xml"
                };

            result.Content.Headers.ContentType =
                new MediaTypeHeaderValue("application/xml");

            return result;
        }

        [HttpGet]
        [Route("pdf/{id}")]
        public async Task<HttpResponseMessage> PDF(string id)
        {
            var file = await InvoiceService.GetPdf(id);

            var result = new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new ByteArrayContent(file)
            };

            result.Content.Headers.ContentDisposition =
                new ContentDispositionHeaderValue("attachment")
                {
                    FileName = "order.pdf"
                };

            result.Content.Headers.ContentType =
                new MediaTypeHeaderValue("application/pdf");

            return result;
        }

        [HttpPost]
        [Route("cancel")]
        public async Task<object> Cancel(InvoiceCancelation invoiceDto)
        {
            var file = await InvoiceService.Cancel(invoiceDto);
            return file;
        }
    }
}
