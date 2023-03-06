using Service.MedicalRecord.Dtos.Invoice;
using Service.MedicalRecord.Dtos.InvoiceCompany;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Service.MedicalRecord.Application.IApplication
{
    public interface IInvoiceCompanyApplication
    {
        Task<InvoiceCompanyInfoDto> GetByFilter(InvoiceCompanyFilterDto filter);
        Task<List<InvoiceFreeDataDto>> GetByFilterFree(InvoiceFreeFilterDto filter);
        Task<string> GetNextPaymentNumber(string serie);
        Task<InvoiceDto> CheckInPayment(InvoiceCompanyDto invoice);
        Task<InvoiceDto> CheckInPaymentCompany(InvoiceCompanyDto invoice);
        Task<bool> EnvioFactura(InvoiceCompanyDeliverDto envio);
        Task<string> Cancel(InvoiceCancelation invoiceDto);
        Task<byte[]> PrintTicket(ReceiptCompanyDto receipt);
        Task<InvoiceCompanyDto> GetById(string invoiceId);
    }
}
