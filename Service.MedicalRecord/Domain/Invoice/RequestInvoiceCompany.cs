using System;

namespace Service.MedicalRecord.Domain.Invoice
{
    public class RequestInvoiceCompany : BaseModel
    {
        public Guid SolicitudId { get; set; }
        public virtual Domain.Request.Request Solicitud { get; set; }
        public Guid InvoiceCompanyId { get; set; }
        public virtual InvoiceCompany InvoiceCompany { get; set; }
        public bool Activo { get; set; }

    }
}
