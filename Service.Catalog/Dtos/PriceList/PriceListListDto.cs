using System;

namespace Service.Catalog.Dtos.PriceList
{
    public class PriceListListDto
    {
        public string Id { get; set; }
        public string Clave { get; set; }
        public string Nombre { get; set; }
        public bool Activo { get; set; }
    }
}
