using System;
using System.Collections.Generic;

namespace Service.MedicalRecord.Dtos.Request
{
    public class RequestCheckInDto
    {
        public Guid ExpedienteId { get; set; }
        public Guid SolicitudId { get; set; }
        public Guid DatoFiscalId { get; set; }
        public string Serie { get; set; }
        public string UsoCFDI { get; set; }
        public string FormaPago { get; set; }
        public bool Desglozado { get; set; }
        public bool Simple { get; set; }
        public bool PorConcepto { get; set; }
        public bool EnvioCorreo { get; set; }
        public bool EnvioWhatsapp { get; set; }
        public List<RequestPaymentDto> Pagos { get; set; }
        public List<RequestCheckInDetailDto> Detalle { get; set; }

        public Guid UsuarioId { get; set; }
        public string UsuarioRegistra { get; set; }
    }

    public class RequestCheckInDetailDto
    {
        public string Clave { get; set; }
        public string Descripcion { get; set; }
        public decimal Precio { get; set; }
        public int Cantidad { get; set; }
        public decimal Descuento { get; set; }
    }
}
