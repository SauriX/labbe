using System;
using System.Collections.Generic;

namespace Service.Catalog.Dtos.PriceList
{
    public class PriceListInfoFilterDto
    {
        public int? EstudioId { get; set; }
        public int? PaqueteId { get; set; }
        public Guid SucursalId { get; set; }
        public Guid MedicoId { get; set; }
        public Guid CompañiaId { get; set; }
        public Guid ListaPrecioId { get; set; }
        public bool OmitirPrecio { get; set; }
        public List<string> Estudios { get; set; }
    }
}
