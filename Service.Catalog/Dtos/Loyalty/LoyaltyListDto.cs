using System;
using System.Collections.Generic;

namespace Service.Catalog.Dtos.Loyalty
{
    public class LoyaltyListDto
    {
        public Guid Id { get; set; }
        public string Clave { get; set; }
        public string Nombre { get; set; }
        public decimal CantidadDescuento { get; set; }
        public string TipoDescuento { get; set; }
        public string Fecha { get; set; }
        public List<Guid> PrecioListaId { get; set; }
        public List<string> PrecioLista { get; set; }
        public string ListaPrecio => string.Join(", ", PrecioLista);
        public bool Activo { get; set; }
    }
}
