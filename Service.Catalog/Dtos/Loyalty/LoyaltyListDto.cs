using System;

namespace Service.Catalog.Dtos.Loyalty
{
    public class LoyaltyListDto
    {
        public Guid Id { get; set; }
        public string Clave { get; set; }
        public string Nombre { get; set; }
        public  decimal CantidadDescuento { get; set; }
        public string TipoDescuento { get; set; }
        public string Fecha { get; set; }
        public string IdListaPrecios { get; set; }
        public bool Activo { get; set; }
    }
}
