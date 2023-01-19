using Service.Billing.Domain;
using System;

namespace Service.Billing.Domain.Invoice
{
    public class Invoice : BaseModel
    {
        public Guid Id { get; set; }
        public string Serie { get; set; }
        public string SerieNumero { get; set; }
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
        public string Estatus { get; set; }
    }
}
