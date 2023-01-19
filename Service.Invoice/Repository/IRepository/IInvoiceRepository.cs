using Service.Billing.Domain.Invoice;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Service.Billing.Repository.IRepository
{
    public interface IInvoiceRepository
    {
        Task<Invoice> GetById(Guid invoiceId);
        Task<List<Invoice>> GetByRecord(Guid recordId);
        Task<List<Invoice>> GetByRequest(Guid requestId);
        Task<string> GetLastSeriesCode(string serie, string year);
        Task Create(Invoice invoice);
        Task CreateInvoiceCompany(InvoiceCompany invoice);
        Task Update(Invoice invoice);
        Task UpdateCompany(InvoiceCompany invoice);
        Task<InvoiceCompany> GetInvoiceCompanyByFacturapiId(string id);
        Task UpdateInvoiceCompany(InvoiceCompany invoiceCompnay);
    }
}
