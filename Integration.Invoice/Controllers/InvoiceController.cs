using Integration.Invoice.Service;
using System.Threading.Tasks;
using System.Web.Http;

namespace Integration.Invoice.Controllers
{
    [RoutePrefix("api/invoice")]
    public class InvoiceController : ApiController
    {
        [HttpPost]
        [Route("")]
        public async Task<string> Ticket()
        {
            var file = await InvoiceService.Create();
            return file;
        }
    }
}
