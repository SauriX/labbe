using System;

namespace Service.Catalog.Dtos.Loyalty
{
    public class LoyaltyListDto
    {
        public Guid Id { get; set; }
        public string Clave { get; set; }
        public string Nombre { get; set; }
        public  decimal BeneficioAplicado { get; set; }
        public string Promocion { get; set; }
        public string Vigencia { get; set; }
        public string NombreListaPrecio { get; set; }
        public bool Activo { get; set; }
    }
}
