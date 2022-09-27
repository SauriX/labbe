using System;
using System.Collections.Generic;

namespace Service.MedicalRecord.Dtos.Request
{
    public class RequestInfoStudyDto
    {
        public Guid ListaPrecioId { get; set; }
        public string ListaPrecio { get; set; }
        public int? PromocionId { get; set; }
        public string Promocion { get; set; }
        public int EstudioId { get; set; }
        public string Clave { get; set; }
        public string Nombre { get; set; }
        public int TaponId { get; set; }
        public string TaponColor { get; set; }
        public int DepartamentoId { get; set; }
        public int AreaId { get; set; }
        public int Dias { get; set; }
        public int Horas { get; set; }
        public decimal Precio { get; set; }
        public decimal Descuento { get; set; }
        public decimal DescuentoPorcentaje { get; set; }
        public decimal PrecioFinal => Precio - Descuento;
    }
}
