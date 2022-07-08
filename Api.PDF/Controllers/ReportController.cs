using Api.PDF.Models;
using Api.PDF.Service.Report;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web.Http;

namespace Api.PDF.Controllers
{
    [RoutePrefix("api/pdf/report")]
    public class ReportController : ApiController
    {
        [HttpPost]
        [Route("generate")]
        public HttpResponseMessage Generate(ReportData reportData)
        {
            var file = ReportService.Generate(reportData);

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
    }
}
