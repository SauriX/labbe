using System;

namespace Service.MedicalRecord.Domain.Invoice
{
    public class InvoiceCompanyDetail
    {
        public Guid Id { get; set; }
        public Guid FacturaId { get; set; }
        public virtual InvoiceCompany Factura { get; set; }
        public string SolicitudClave { get; set; }
        public string EstudioClave { get; set; }
        public string Concepto { get; set; }
        public int Cantidad { get; set; }
        public decimal Importe { get; set; }

    }
}
