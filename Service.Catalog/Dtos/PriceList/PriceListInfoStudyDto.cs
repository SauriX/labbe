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
        public int? PromocionId { get; set; }
        public string Promocion { get; set; }
        public int EstudioId { get; set; }
        public string Clave { get; set; }
        public string Nombre { get; set; }
        public int DepartamentoId { get; set; }
        public int AreaId { get; set; }
        public int Dias { get; set; }
        public int Horas { get; set; }
        public decimal Precio { get; set; }
        public decimal Descuento { get; set; }
        public decimal DescuentoPorcentaje { get; set; }
        public decimal PrecioFinal => Precio - Descuento;
        public IEnumerable<ParameterListDto> Parametros { get; set; }
        public IEnumerable<IndicationListDto> Indicaciones { get; set; }
    }
}
