using Service.MedicalRecord.Domain;
using System;

namespace Service.Catalog.Domain.Promotion
{
    public class PromotionStudy : BaseModel
    {
        public int PromocionId { get; set; }
        public virtual Promotion Promocion { get; set; }
        public int EstudioId { get; set; }
        public virtual Study.Study Estudio { get; set; }
        public decimal DescuentoPorcentaje { get; set; }
        public decimal DescuentoCantidad { get; set; }
        public decimal Precio { get; set; }
        public decimal PrecioFinal { get; set; }
        public bool Lunes { get; set; }
        public bool Martes { get; set; }
        public bool Miercoles { get; set; }
        public bool Jueves { get; set; }
        public bool Viernes { get; set; }
        public bool Sabado { get; set; }
        public bool Domingo { get; set; }
        public DateTime FechaInicial { get; set; }
        public DateTime FechaFinal { get; set; }
        public bool Activo { get; set; }
    }
}
