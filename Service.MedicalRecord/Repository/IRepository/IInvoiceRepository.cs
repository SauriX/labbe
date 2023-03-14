using Service.MedicalRecord.Domain.Invoice;
using Service.MedicalRecord.Domain.Request;
using Service.MedicalRecord.Dtos.InvoiceCompany;
using Service.MedicalRecord.Dtos.Request;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Service.MedicalRecord.Repository.IRepository
{
    public interface IInvoiceRepository
    {
        Task<List<Request>> InvoiceCompanyFilter(InvoiceCompanyFilterDto filter);
        Task<List<InvoiceCompany>> InvoiceFreeFilter(InvoiceFreeFilterDto filter);
        Task CreateInvoiceCompanyData(InvoiceCompany invoiceCompnay, List<RequestInvoiceCompany> requestInvoiceCompany);
        Task UpdateInvoiceCompany(InvoiceCompany invoiceCompnay);
        Task<InvoiceCompany> GetInvoiceCompanyByFacturapiId(string id);
        Task<InvoiceCompany> GetInvoiceById(string invoiceId);
    }
}
