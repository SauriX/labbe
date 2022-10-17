using Integration.Pdf.Dtos;
using Integration.Pdf.Dtos.PendingRecive;
using Integration.Pdf.Service;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web.Http;

namespace Integration.Pdf.Controllers
{
    [RoutePrefix("api/pdf")]
    public class PdfController : ApiController
    {
        [HttpPost]
        [Route("ticket")]
        public HttpResponseMessage Ticket(RequestOrderDto order)
        {
            var file = TicketService.Generate(order);

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

        [HttpGet]
        [Route("quotation")]
        public HttpResponseMessage Quotation()
        {
            var file = QuotationService.Generate();

            var result = new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new ByteArrayContent(file)
            };

            result.Content.Headers.ContentDisposition =
                new ContentDispositionHeaderValue("attachment")
                {
                    FileName = "quotation.pdf"
                };

            result.Content.Headers.ContentType =
                new MediaTypeHeaderValue("application/pdf");

            return result;
        }

        [HttpPost]
        [Route("order")]
        public HttpResponseMessage Order(RequestOrderDto order)
        {
            var file = OrderService.Generate(order);

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
        [Route("mantain")]
        public HttpResponseMessage Mantain(MantainDto order)
        {
            var file = MantainService.Generate(order);

            var result = new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new ByteArrayContent(file)
            };

            result.Content.Headers.ContentDisposition =
                new ContentDispositionHeaderValue("attachment")
                {
                    FileName = "Mantain.pdf"
                };

            result.Content.Headers.ContentType =
                new MediaTypeHeaderValue("application/pdf");

            return result;
        }
        [HttpPost]
        [Route("pending")]
        public HttpResponseMessage pending(List<PendingReciveDto> order)
        {
            var file = PendingRecive.Generate(order);

            var result = new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new ByteArrayContent(file)
            };

            result.Content.Headers.ContentDisposition =
                new ContentDispositionHeaderValue("attachment")
                {
                    FileName = "Mantain.pdf"
                };

            result.Content.Headers.ContentType =
                new MediaTypeHeaderValue("application/pdf");

            return result;
        }
        [HttpPost]
        [Route("tags")]
        public HttpResponseMessage Tag(List<RequestTagDto> tags)
        {
            var file = TagService.Generate(tags);

            var result = new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new ByteArrayContent(file)
            };

            result.Content.Headers.ContentDisposition =
                new ContentDispositionHeaderValue("attachment")
                {
                    FileName = "labels.pdf"
                };

            result.Content.Headers.ContentType =
                new MediaTypeHeaderValue("application/pdf");

            return result;
        }

        [HttpPost]
        [Route("pathologicalResults")]
        public async Task<HttpResponseMessage> PathologicalResults(PathologicalResultsDto results)
        {
            var file = await PathologicalResultService.GeneratePathologicalResultPdf(results);
            var labFile = LabResultsService.Generate(new ClinicResultsPdfDto() { SolicitudInfo = new ClinicResultsRequestDto(), CapturaResultados = new List<ClinicResultsCaptureDto>() });

            var mergeFile = PathologicalResultService.MergePdf(file, labFile);

            var result = new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new ByteArrayContent(mergeFile)
            };

            result.Content.Headers.ContentDisposition =
                new ContentDispositionHeaderValue("attachment")
                {
                    FileName = "labels.pdf"
                };

            result.Content.Headers.ContentType =
                new MediaTypeHeaderValue("application/pdf");

            return result;
        }

        [HttpPost]
        [Route("lab_results")]
        public HttpResponseMessage LabResults(ClinicResultsPdfDto results)
        {
            var file = LabResultsService.Generate(results);

            var result = new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new ByteArrayContent(file)
            };

            result.Content.Headers.ContentDisposition =
                new ContentDispositionHeaderValue("attachment")
                {
                    FileName = "labels.pdf"
                };

            result.Content.Headers.ContentType =
                new MediaTypeHeaderValue("application/pdf");

            return result;
        }

        [HttpPost]
        [Route("mergeResults")]
        public async Task<HttpResponseMessage> MergeResults(ClinicResultsMergePdfDto mergeResults)
        {
            var file = await PathologicalResultService.GeneratePathologicalResultPdf(mergeResults.PathologicalResults);
            var labFile = LabResultsService.Generate(mergeResults.LabResults);

            var mergeFile = PathologicalResultService.MergePdf(file, labFile);

            var result = new HttpResponseMessage();

            if (file.Length > 0 && labFile.Length > 0)
            {
                result = new HttpResponseMessage(HttpStatusCode.OK)
                {
                    Content = new ByteArrayContent(mergeFile)
                };
            }

            if (file.Length == 0)
            {
                result = new HttpResponseMessage(HttpStatusCode.OK)
                {
                    Content = new ByteArrayContent(labFile)
                };
            }

            if (labFile.Length == 0)
            {
                result = new HttpResponseMessage(HttpStatusCode.OK)
                {
                    Content = new ByteArrayContent(file)
                };
            }


            result.Content.Headers.ContentDisposition =
                new ContentDispositionHeaderValue("attachment")
                {
                    FileName = "labels.pdf"
                };

            result.Content.Headers.ContentType =
                new MediaTypeHeaderValue("application/pdf");

            return result;
        }
    }
}
