using System;

namespace Service.MedicalRecord.Domain.Request
{
    public class RequestStudy : BaseModel
    {
        public Guid SolicitudId { get; set; }
        public Guid ListaPrecioId { get; set; }
        public int PromocionId { get; set; }
        public Guid? EstudioId { get; set; }
        public int? PaqueteId { get; set; }
        public byte EstatusId { get; set; }
        public bool Descuento { get; set; }
        public bool Cargo { get; set; }
        public bool Copago { get; set; }
        public decimal Precio { get; set; }
        public decimal PrecioFinal { get; set; }
    }
}
