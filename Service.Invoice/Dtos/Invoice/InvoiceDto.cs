using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Service.Billing.Dtos.Invoice
{
    public class InvoiceDto
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
        public Guid? SolicitudId { get; set; }
        public string Solicitud { get; set; }
        public Guid? ExpedienteId { get; set; }
        public string Expediente { get; set; }
        public string Paciente { get; set; }
        public ClientDto Cliente { get; set; }
        public List<Guid> SolicitudesId { get; set; }
        public List<ProductDto> Productos { get; set; }
    }
}