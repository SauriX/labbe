﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Service.MedicalRecord.Application.IApplication;
using Service.MedicalRecord.Client.IClient;
using Service.MedicalRecord.Dtos.Invoice;
using Service.MedicalRecord.Dtos.InvoiceCompany;
using Shared.Dictionary;
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

        [HttpGet("getConsecutiveBySerie/{serie}")]
        [Authorize(Policies.Access)]
        public async Task<string> GetConsecutiveBySerie(string serie)
        {
            return await _service.GetNextPaymentNumber(serie);
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
        [HttpGet("download/pdf/{facturapiId}")]
        [Authorize(Policies.Access)]
        public async Task<IActionResult> DownloadPDF(string facturapiId)
        {
            var file = await _billingClient.DownloadPDF(facturapiId);

            return File(file, MimeType.PDF, "Facturacion compañias.pdf");
        }

        [HttpGet("print/pdf{facturapiId}")]
        [Authorize(Policies.Access)]
        public async Task<IActionResult>  PrintPDF(string facturapiId)
        {
            var file = await _billingClient.DownloadPDF(facturapiId);

            return File(file, MimeType.PDF, "Facturacion compañias.pdf");
        }

    }
}
