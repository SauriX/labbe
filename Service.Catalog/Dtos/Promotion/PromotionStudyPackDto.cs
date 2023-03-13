using System;

namespace Service.Catalog.Dtos.Promotion
{
    public class PromotionStudyPackDto
    {
        public int? EstudioId { get; set; }
        public int? PaqueteId { get; set; }
        public bool EsEstudio => EstudioId != null;
        public string Tipo => EstudioId != null ? "study" : "pack";
        public string Clave { get; set; }
        public string Nombre { get; set; }
        public int? DepartamentoId { get; set; }
        public int? AreaId { get; set; }
        public string Area { get; set; }
        public decimal DescuentoPorcentaje { get; set; }
        public decimal DescuentoCantidad { get; set; }
        public DateTime FechaInicial { get; set; }
        public DateTime FechaFinal { get; set; }
        public bool Activo { get; set; }
        public decimal Precio { get; set; }
        public decimal PrecioFinal { get; set; }
        public bool Lunes { get; set; }
        public bool Martes { get; set; }
        public bool Miercoles { get; set; }
        public bool Jueves { get; set; }
        public bool Viernes { get; set; }
        public bool Sabado { get; set; }
        public bool Domingo { get; set; }
    }
}
