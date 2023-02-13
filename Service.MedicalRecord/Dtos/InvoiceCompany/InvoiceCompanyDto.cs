using Service.MedicalRecord.Dtos.Invoice;
using System;
using System.Collections.Generic;

namespace Service.MedicalRecord.Dtos.InvoiceCompany
{
    public class InvoiceCompanyDto : InvoiceDto
    {

        public Guid? CompanyId { get; set; }
        public string Estatus { get; set; }
        public List<Guid> SolicitudesId { get; set; }
        public string TipoFactura { get; set; }
        public List<InvoiceCompanyStudiesInfoDto> Estudios { get; set; }
        public Guid? TaxDataId { get; set; }
        public Guid? CompañiaId { get; set; }
        public Guid? ExpedienteId { get; set; }
        public Guid FacturaId { get; set; }
        //public Guid? FormaPagoId { get; set; }
        public string FormaPago { get; set; }
        public string NumeroCuenta { get; set; }
        public string Serie { get; set; }
        public string UsoCFDI { get; set; }
        public string TipoDesgloce { get; set; }
        public decimal CantidadTotal { get; set; }
        public decimal Subtotal { get; set; }
        public decimal IVA { get; set; }
        public int Consecutivo { get; set; }
        public string Nombre { get; set; }
        public List<InvoiceDetail> Detalles { get; set; }

}
    public class InvoiceDetail
    {
        public string SolicitudClave { get; set; }
        public string EstudioClave { get; set; }
        public string Concepto { get; set; }
        public int Cantidad { get; set; }
        public decimal Importe { get; set; }
        public decimal Descuento { get; set; }
    }
}
