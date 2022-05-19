using System;

namespace Service.Catalog.Dtos.Promotion
{
    public class PromotionEstudioListDto
    {
        public int Id { get; set; }
        public string Clave { get; set; }
        public string Nombre { get; set; }
        public decimal DescuentoPorcentaje { get; set; }
        public decimal DescuentoCantidad { get; set; }
        public bool Lealtad { get; set; }
        public DateTime FechaInicial { get; set; }
        public DateTime FechaFinal { get; set; }
        public bool Activo { get; set; }
        public decimal Precio { get; set; }
        public decimal PrecioFinal { get; set; }
        public bool Paquete { get; set; }
    }
}
