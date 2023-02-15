using System;

namespace Service.Billing.Domain.Invoice
{
    public class InvoiceCompanyRequests : BaseModel
    {
        public Guid Id { get; set; }
        //public Guid InvoiceCompanyId { get; set; }
        //public virtual InvoiceCompany InvoiceCompany { get; set; }
        public Guid? InvoiceId { get; set; }
        public virtual Invoice Invoice { get; set; }
        public Guid SolicitudId { get; set; }

    }
}
