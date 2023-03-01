using System;

namespace Service.MedicalRecord.Dtos.InvoiceCompany
{
    public class InvoiceFreeDataDto
    {
        public Guid Id { get; set; }
        public string Documento { get; set; }
        public string Cliente { get; set; }
        public string FechaCreacion { get; set; }
        public string FechaLimiteCredito { get; set; }
        public decimal Monto { get; set; }
        
    }
}
