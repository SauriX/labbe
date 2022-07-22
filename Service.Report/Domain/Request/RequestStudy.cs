using System;

namespace Service.Report.Domain.Request
{
    public class RequestStudy
    {
        public Guid Id { get; set; }
        public Guid SolicitudId { get; set; }
        public virtual Request Solicitud { get; set; }
        public string Estudio { get; set; }
        public string Clave { get; set; }
        public byte EstatusId { get; set; }
        public bool Descuento { get; set; }
        public decimal Precio { get; set; }
        public decimal PrecioFinal { get; set; }
    }
}
