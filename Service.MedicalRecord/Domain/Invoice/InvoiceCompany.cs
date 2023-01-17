using System;
using System.Collections.Generic;

namespace Service.MedicalRecord.Domain.Invoice
{
    public class InvoiceCompany : BaseModel
    {
        public Guid Id { get; set; }
        public virtual ICollection<Domain.Request.Request> Solicitudes { get; set; }
        public virtual string Estatus { get; set; }
        public string TipoFactura { get; set; }
        public Guid FacturaId { get; set; }
        public string FacturapiId { get; set; }
    }
    
}
