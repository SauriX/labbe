﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Service.Billing.Application.IApplication;
using System.Collections.Generic;
using System.Threading.Tasks;
using System;
using Microsoft.AspNetCore.Authorization;
using Shared.Dictionary;
using Service.Billing.Dtos.Invoice;

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

        [HttpPost("print/xml/{invoiceId}")]
        [Authorize(Policies.Download)]
        public async Task<IActionResult> PrintInvoiceXML(Guid invoiceId)
        {
            var (file, fileName) = await _service.PrintInvoiceXML(invoiceId);

            return File(file, MimeType.XML, fileName);
        }
    }
}