using Service.Catalog.Dtos.Indication;
using Service.Catalog.Dtos.Parameter;
using System.Collections.Generic;

namespace Service.Catalog.Dtos.PriceList
{
    public class PriceListInfoStudyDto
    {
        public string PrecioListaId { get; set; }
        public int EstudioId { get; set; }
        public string Clave { get; set; }
        public string Nombre { get; set; }
        public decimal PrecioListaPrecio { get; set; }
        public IEnumerable<ParameterListDto> Parametros { get; set; }
        public IEnumerable<IndicationListDto> Indicaciones { get; set; }
    }
}
