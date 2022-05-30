using System;
using System.Collections.Generic;

namespace Service.Catalog.Dtos.Promotion
{
    public class PromotionEstudioListDto
    {
        public int Id { get; set; }
        public string Clave { get; set; }
        public string Nombre { get; set; }
        public string Departamento { get; set; }
        public string Area { get; set; }
        public decimal DescuentoPorcentaje { get; set; }
        public decimal DescuentoCantidad { get; set; }
        public bool Lealtad { get; set; }
        public DateTime FechaInicial { get; set; }
        public DateTime FechaFinal { get; set; }
        public bool Activo { get; set; }
        public decimal Precio { get; set; }
        public decimal PrecioFinal { get; set; }
        public bool Paquete { get; set; }
        public bool Lunes { get; set; }
        public bool Martes { get; set; }
        public bool Miercoles { get; set; }
        public bool Jueves { get; set; }
        public bool Viernes { get; set; }
        public bool Sabado { get; set; }
        public bool Domingo { get; set; }
    }
}
