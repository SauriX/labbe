using Service.Catalog.Dtos.Indication;
using Service.Catalog.Dtos.Parameter;
using System;
using System.Collections.Generic;

namespace Service.Catalog.Dtos.PriceList
{
    public class PriceListInfoStudyDto
    {
        public Guid ListaPrecioId { get; set; }
        public string ListaPrecio { get; set; }
        public int EstudioId { get; set; }
        public string Clave { get; set; }
        public string Nombre { get; set; }
        public int TaponId { get; set; }
        public string TaponColor { get; set; }
        public int DepartamentoId { get; set; }
        public int AreaId { get; set; }
        public int Dias { get; set; }
        public int Horas { get; set; }
        public int Orden { get; set; }
        public decimal Precio { get; set; }
        public List<PriceListInfoPromo> Promociones { get; set; }
        public IEnumerable<ParameterListDto> Parametros { get; set; }
        public IEnumerable<IndicationListDto> Indicaciones { get; set; }
    }

    public class PriceListInfoPromo
    {
        public int? PromocionId { get; set; }
        public string Promocion { get; set; }
        public decimal Descuento { get; set; }
        public decimal DescuentoPorcentaje { get; set; }
    }
}
