using Service.MedicalRecord.Dtos.Invoice;
using Service.MedicalRecord.Dtos.InvoiceCatalog;
using Service.MedicalRecord.Dtos.InvoiceCompany;
using Service.MedicalRecord.Dtos.Series;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Service.MedicalRecord.Client.IClient
{
    public interface IBillingClient
    {
        Task<InvoiceDto> CheckInPayment(InvoiceDto invoiceDto);
        Task<InvoiceDto> CheckInPaymentCompany(InvoiceDto invoiceDto);
        Task<byte[]> DownloadPDF(Guid invoiceId);
        Task<string> CancelInvoice(InvoiceCancelation invoiceDto);
        Task<List<InvoiceDto>> getAllInvoice(InvoiceCatalogSearch search);
    }
}
