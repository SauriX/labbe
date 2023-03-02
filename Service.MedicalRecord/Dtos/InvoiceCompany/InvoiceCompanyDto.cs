using Service.MedicalRecord.Dtos.Invoice;
using System;
using System.Collections.Generic;

namespace Service.MedicalRecord.Dtos.InvoiceCompany
{
    public class InvoiceCompanyDto
    {
        public Guid Id { get; set; }
        public Guid? CompanyId { get; set; }
        public string Estatus { get; set; }
        public List<Guid> SolicitudesId { get; set; }
        public string TipoFactura { get; set; }
        public string OrigenFactura { get; set; }
        public string RFC { get; set; }
        public string DireccionFiscal { get; set; }
        public string RazonSocial { get; set; }
        public string RegimenFiscal { get; set; }
        public List<InvoiceCompanyStudiesInfoDto> Estudios { get; set; }
        public Guid? TaxDataId { get; set; }
        public Guid? CompañiaId { get; set; }
        public Guid? ExpedienteId { get; set; }
        public Guid? FacturaId { get; set; }
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
        public string FacturapiId { get; set; }
        public string MetodoPago { get; set; }
        public Cliente Cliente { get; set; }
        public string TipoPago { get; set; }
        public int FormaPagoId { get; set; }
        public int BancoId { get; set; }
        public string ClaveExterna { get; set; }
        public int DiasCredito { get; set; }
        public List<InvoiceDetail> Detalles { get; set; }

    }
    public class Cliente
    {
        public string RazonSocial { get; set; }
        public string RFC { get; set; }
        public string RegimenFiscal { get; set; }
        public string DireccionFiscal { get; set; }
        public string Correo { get; set; }
        public string Telefono { get; set; }
        public string CodigoPostal { get; set; }
        public string Calle { get; set; }
        public string NumeroExterior { get; set; }
        public string Colonia { get; set; }
        public string Ciudad { get; set; }
        public string Municipio { get; set; }
        public string Estado { get; set; }
        public string Pais { get; set; }
    }
    public class InvoiceDetail
    {
        public Guid Id { get; set; }
        public string ClaveProdServ { get; set; }
        public string SolicitudClave { get; set; }
        public string EstudioClave { get; set; }
        public string Concepto { get; set; }
        public int Cantidad { get; set; }
        public decimal Importe { get; set; }
        public decimal Descuento { get; set; }
    }
}
