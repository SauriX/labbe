using System;

namespace Service.Catalog.Dtos.PriceList
{
    public class PriceListMedicDto
    {
        public Guid Id { get; set; }
        public string Clave { get; set; }
        public string Nombre { get; set; }
        public decimal Precio { get; set; }
    }
}
