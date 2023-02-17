using System.Collections.Generic;
using System.Threading.Tasks;
using System;
using Service.Billing.Dtos.Invoice;
using Service.Billing.Dtos.Facturapi;

namespace Service.Billing.Application.IApplication
{
    public interface IInvoiceApplication
    {
        Task<InvoiceDto> GetById(Guid invoiceId);
        Task<List<InvoiceDto>> GetByRecord(Guid recordId);
        Task<List<InvoiceDto>> GetByRequest(Guid requestId);
        Task<InvoiceDto> Create(InvoiceDto invoiceDto);
        Task<InvoiceDto> CreateInvoiceCompany(InvoiceDto invoiceDto);
        Task<string> Cancel(InvoiceCancelation invoiceDto);
        Task<(byte[], string)> PrintInvoiceXML(Guid invoiceId);
        Task<(byte[], string)> PrintInvoicePDF(string invoiceId);
        Task<List<InvoiceDto>> GetAllInvoice(InvoiceSearch search);
    }
}
