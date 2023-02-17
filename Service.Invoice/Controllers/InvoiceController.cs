using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Service.Billing.Application.IApplication;
using System.Collections.Generic;
using System.Threading.Tasks;
using System;
using Microsoft.AspNetCore.Authorization;
using Shared.Dictionary;
using Service.Billing.Dtos.Invoice;
using Service.Billing.Dtos.Facturapi;

namespace Service.Billing.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InvoiceController : ControllerBase
    {
        private readonly IInvoiceApplication _service;

        public InvoiceController(IInvoiceApplication service)
        {
            _service = service;
        }
        [HttpPost("all")]
        public async Task<List<InvoiceDto>> GetAll(InvoiceSearch search)
        {
            return await _service.GetAllInvoice(search);
        }

        [HttpGet("{invoiceId}")]
        public async Task<InvoiceDto> GetById(Guid invoiceId)
        {
            return await _service.GetById(invoiceId);
        }

        [HttpGet("record/{recordId}")]
        public async Task<List<InvoiceDto>> GetByRecord(Guid recordId)
        {
            return await _service.GetByRecord(recordId);
        }

        [HttpGet("request/{requestId}")]
        public async Task<List<InvoiceDto>> GetByRequest(Guid requestId)
        {
            return await _service.GetByRequest(requestId);
        }

        [HttpPost]
        public async Task<InvoiceDto> Create(InvoiceDto invoiceDto)
        {
            return await _service.Create(invoiceDto);
        }

        [HttpPost("create/invoiceCompany")]
        public async Task<InvoiceDto> CreateInvoiceCompany(InvoiceDto invoiceDto)
        {
            return await _service.CreateInvoiceCompany(invoiceDto);
        }
        [HttpPost("cancel")]
        public async Task<string> CancelInvoiceCompany(InvoiceCancelation invoiceDto)
        {
            return await _service.Cancel(invoiceDto);
        }

        [HttpPost("print/xml/{invoiceId}")]
        public async Task<IActionResult> PrintInvoiceXML(Guid invoiceId)
        {
            var (file, fileName) = await _service.PrintInvoiceXML(invoiceId);

            return File(file, MimeType.XML, fileName);
        }

        [HttpPost("print/pdf/{invoiceId}")]
        public async Task<IActionResult> PrintInvoicePDF(string invoiceId)
        {
            var (file, fileName) = await _service.PrintInvoicePDF(invoiceId);

            return File(file, MimeType.PDF, fileName);
        }
    }
}