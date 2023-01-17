using System;
using System.Collections.Generic;

namespace Service.Billing.Domain.Invoice
{
    public class InvoiceCompany : BaseModel
    {
        public Guid Id { get; set; }
        public string FacturapiId { get; set; }
        public string FormaPago { get; set; }
        public string MetodoPago { get; set; }
        public string UsoCFDI { get; set; }
        public string RegimenFiscal { get; set; }
        public string RFC { get; set; }
        public bool Desglozado { get; set; }
        public bool ConNombre { get; set; }
        public string EnvioCorreo { get; set; }
        public string EnvioWhatsapp { get; set; }
        public Guid CompañiaId { get; set; }
        public string Compañia { get; set; }
        public string Estatus { get; set; }
        public virtual ICollection<InvoiceCompanyRequests> Solicitudes { get; set; }
    }
}
