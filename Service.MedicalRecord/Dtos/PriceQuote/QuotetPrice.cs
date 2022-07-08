using System;

namespace Service.MedicalRecord.Dtos.PriceQuote
{
    public class QuotetPrice
    {
        public Guid PrecioListaId { get; set; }
        public Guid ListaPrecioId { get; set; }
        public int? PromocionId { get; set; }
        public int? EstudioId { get; set; }
        public int? PaqueteId { get; set; }
        public byte EstatusId { get; set; }
        public bool Descuento { get; set; }
        public bool Cargo { get; set; }
        public bool Copago { get; set; }
        public decimal Precio { get; set; }
        public decimal PrecioFinal { get; set; }
        public string Nombre { get; set; } 
    }
}
