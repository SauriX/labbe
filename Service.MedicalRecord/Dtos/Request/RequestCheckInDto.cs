using System;
using System.Collections.Generic;

namespace Service.MedicalRecord.Dtos.Request
{
    public class RequestCheckInDto
    {
        public Guid ExpedienteId { get; set; }
        public Guid SolicitudId { get; set; }
        public Guid DatoFiscalId { get; set; }
        public string UsoCFDI { get; set; }
        public string FormaPago { get; set; }
        public bool Desglozado { get; set; }
        public bool ConNombre { get; set; }
        public bool EnvioCorreo { get; set; }
        public bool EnvioWhatsapp { get; set; }
        public List<RequestPaymentDto> Pagos { get; set; }

        public Guid UsuarioId { get; set; }
        public string UsuarioRegistra { get; set; }
    }
}
