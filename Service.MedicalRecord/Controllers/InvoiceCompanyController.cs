using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Service.MedicalRecord.Application.IApplication;
using Service.MedicalRecord.Client.IClient;
using Service.MedicalRecord.Dtos.Invoice;
using Service.MedicalRecord.Dtos.InvoiceCompany;
using Shared.Dictionary;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Service.MedicalRecord.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InvoiceCompanyController : ControllerBase
    {
        private readonly IInvoiceCompanyApplication _service;
        private readonly IBillingClient _billingClient;
        public InvoiceCompanyController(IInvoiceCompanyApplication service, IBillingClient billingClient)
        {
            _service = service;
            _billingClient = billingClient;
        }

        [HttpPost("filter")]
        [Authorize(Policies.Access)]
        public async Task<InvoiceCompanyInfoDto> GetByFilter(InvoiceCompanyFilterDto filter)
        {
            return await _service.GetByFilter(filter);
        } 
        [HttpPost("filter/free")]
        [Authorize(Policies.Access)]
        public async Task<List<InvoiceFreeDataDto>> GetByFilterFree(InvoiceFreeFilterDto filter)
        {
            return await _service.GetByFilterFree(filter);
        }

        [HttpGet("getConsecutiveBySerie/{serie}")]
        [Authorize(Policies.Access)]
        public async Task<string> GetConsecutiveBySerie(string serie)
        {
            return await _service.GetNextPaymentNumber(serie);
        }

        [HttpGet("{invoiceId}")]
        public async Task<InvoiceCompanyDto> GetById(string invoiceId)
        {
            return await _service.GetById(invoiceId);
        }

        [HttpPost("checkin")]
        [Authorize(Policies.Access)]
        public async Task<InvoiceDto> CheckInPayment(InvoiceCompanyDto invoice)
        {
            return await _service.CheckInPayment(invoice);
        }

        [HttpPost("checkin/company")]
        [Authorize(Policies.Access)]
        public async Task<InvoiceDto> CheckInPaymentCompany(InvoiceCompanyDto invoice)
        {
            return await _service.CheckInPaymentCompany(invoice);
        }

        [HttpPost("chekin/global")]
        [Authorize(Policies.Access)]
        public async Task CheckInPaymentGlobal(List<Guid> requests)
        {
            await _service.CheckInInvoiceGlobal(requests);
        }

        [HttpPost("download/pdf/{facturapiId}")]
        [Authorize(Policies.Access)]
        public async Task<IActionResult> DownloadPDF(Guid facturapiId)
        {
            var file = await _billingClient.DownloadPDF(facturapiId);

            return File(file, MimeType.PDF, "Facturacion compañias.pdf");
        }

        [HttpPost("print/pdf/{facturapiId}")]
        [Authorize(Policies.Access)]
        public async Task<IActionResult>  PrintPDF(Guid facturapiId)
        {
            var file = await _billingClient.DownloadPDF(facturapiId);

            return File(file, MimeType.PDF, "Facturacion compañias.pdf");
        }

        [HttpPost("print/xml/{facturapiId}")]
        [Authorize(Policies.Access)]
        public async Task<IActionResult>  PrintXML(Guid facturapiId)
        {
            var file = await _billingClient.DownloadXML(facturapiId);

            return File(file, MimeType.XML, "Facturacion compañias.xml");
        }
        [HttpPost("send")]
        [Authorize(Policies.Access)]
        public async Task<bool> EnvioFactura(InvoiceCompanyDeliverDto envio)
        {
            await _service.EnvioFactura(envio);

            return true;
        }
        [HttpPost("cancel")]
        [Authorize(Policies.Access)]
        public async Task<string> CancelInvoiceCompany(InvoiceCancelation invoice)
        {
            return await _service.Cancel(invoice);
        }

        [HttpPost("ticket")]
        [Authorize(Policies.Download)]
        public async Task<IActionResult> PrintTicket(ReceiptCompanyDto receipt)
        {
            var userName = HttpContext.Items["userName"].ToString();

            var file = await _service.PrintTicket(receipt);

            return File(file, MimeType.PDF, "ticket.pdf");
        }



    }
}
