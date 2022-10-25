using System;

namespace Service.MedicalRecord.Domain.Request
{
    public class RequestPayment : BaseModel
    {
        public Guid Id { get; set; }
        public Guid SolicitudId { get; set; }
        public virtual Request Solicitud { get; set; }
        public int FormaPagoId { get; set; }
        public string FormaPago { get; set; }
        public string NumeroCuenta { get; set; }
        public decimal Cantidad { get; set; }
        public string Serie { get; set; }
        public string Numero { get; set; }
        public DateTime FechaPago { get; set; }
        public string UsuarioRegistra { get; set; }
    }
}
