using System;
using System.Collections.Generic;
using System.Linq;

namespace Service.Catalog.Dtos.PriceList
{
    public class PriceListInfoPackDto
    {
        public Guid ListaPrecioId { get; set; }
        public string ListaPrecio { get; set; }
        public int? PromocionId { get; set; }
        public string Promocion { get; set; }
        public int PaqueteId { get; set; }
        public string Clave { get; set; }
        public string Nombre { get; set; }
        public int DepartamentoId { get; set; }
        public int AreaId { get; set; }
        public int Dias => Estudios.Max(x => x.Dias);
        public int Horas => Estudios.Max(x => x.Horas);
        public decimal Precio { get; set; }
        public decimal Descuento { get; set; }
        public decimal DescuentoPorcentaje { get; set; }
        public decimal PrecioFinal => Precio - Descuento;
        public List<PriceListInfoStudyDto> Estudios { get; set; }
    }
}
