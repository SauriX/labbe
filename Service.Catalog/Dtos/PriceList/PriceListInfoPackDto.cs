using System;
using System.Collections.Generic;

namespace Service.Catalog.Dtos.PriceList
{
    public class PriceListInfoPackDto
    {
        public string PrecioListaId { get; set; }
        public int PaqueteId { get; set; }
        public string Clave { get; set; }
        public string Nombre { get; set; }
        public decimal PrecioListaPrecio { get; set; }
        public IEnumerable<PriceListInfoStudyDto> Estudios { get; set; }
    }
}
