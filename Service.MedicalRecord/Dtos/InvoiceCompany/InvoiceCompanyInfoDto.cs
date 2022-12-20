using System;
using System.Collections.Generic;

namespace Service.MedicalRecord.Dtos.InvoiceCompany
{
    public class InvoiceCompanyInfoDto
    {
        public int TotalSolicitudes { get; set; }
        public int TotalEstudios { get; set; }
        public decimal TotalPrecio { get; set; }
        public decimal TotalD { get; set; }
        public decimal TotalC { get; set; }
        public decimal Total { get; set; }
        public List<InvoiceCompanyRequestsInfoDto> Solicitudes { get; set; }

    }
    public class InvoiceCompanyRequestsInfoDto
    {
        public Guid SolicitudId { get; set; }
        public string Clave { get; set; }
        public string Nombre { get; set; }
        public decimal Cargo { get; set; }
        public decimal Descuento { get; set; }
        public decimal Monto { get; set; }
        public string Compania { get; set; }
        public string RFC { get; set; }
        public string ClavePatologica { get; set; }
        public List<InvoiceCompanyFacturaDto> Facturas { get; set; }
        public List<InvoiceCompanyStudiesInfoDto> Estudios { get; set; }
    }
    public class InvoiceCompanyFacturaDto
    {
        public Guid FacturaId { get; set; }
        public byte Estatus { get; set; }
    }
    public class InvoiceCompanyStudiesInfoDto
    {
        public Guid SolicitudEstudioId { get; set; }
        public string Estudio { get; set; }
        public string Clave { get; set; }
        public int? Area { get; set; }
        public decimal Precio { get; set; }

    }
}
