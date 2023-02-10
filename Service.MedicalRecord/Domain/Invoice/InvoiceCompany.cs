using System;
using System.Collections.Generic;
using Service.MedicalRecord.Domain.Catalogs;

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
        public Guid? TaxDataId { get; set; }
        public virtual TaxData.TaxData TaxData { get; set; }
        public Guid? CompañiaId { get; set; }
        public virtual Company Compañia { get; set; }
        public Guid? ExpedienteId { get; set; }
        public Guid? FormaPagoId { get; set; }
        public string FormaPago { get; set; }
        public string NumeroCuenta { get; set; }
        public string Serie { get; set; }
        public string UsoCFDI { get; set; }
        public string TipoDesgloce { get; set; }
        public decimal CantidadTotal { get; set; }
        public decimal Subtotal { get; set; }
        public decimal IVA { get; set; }
        public virtual ICollection<InvoiceCompanyDetail> DetalleFactura { get; set; }

    }
    
}
