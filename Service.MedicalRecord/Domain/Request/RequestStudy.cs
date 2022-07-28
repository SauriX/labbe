using System;

namespace Service.MedicalRecord.Domain.Request
{
    public class RequestStudy : BaseModel
    {
        public Guid SolicitudId { get; set; }
        public int EstudioId { get; set; }
        public string Clave { get; set; }
        public string Nombre { get; set; }
        public int? PaqueteId { get; set; }
        public string Paquete { get; set; }
        public Guid ListaPrecioId { get; set; }
        public string ListaPrecio { get; set; }
        public int? PromocionId { get; set; }
        public string Promocion { get; set; }
        public byte EstatusId { get; set; }
        public bool Descuento { get; set; }
        public bool Cargo { get; set; }
        public bool Copago { get; set; }
        public decimal Precio { get; set; }
        public decimal PrecioFinal { get; set; }
    }
}
