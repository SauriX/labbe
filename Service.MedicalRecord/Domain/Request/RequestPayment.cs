using System;

namespace Service.MedicalRecord.Domain.Request
{
    public class RequestPayment
    {
        public Guid Id { get; set; }
        public Guid SolicitudId { get; set; }
        public virtual Request Solicitud { get; set; }
        public int FormaPagoId { get; set; }
        public string FormaPago { get; set; }
        public int UsoCfdiId { get; set; }
        public string NumeroCuenta { get; set; }
        public DateTime FechaPago { get; set; }
    }
}
