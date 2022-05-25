using System;

namespace Service.Catalog.Dtos.Loyalty
{
    public class LoyaltyListDto
    {
        public int Id { get; set; }
        public string Clave { get; set; }
        public string Nombre { get; set; }
        public  string BeneficioAplicado { get; set; }
        public string Promocion { get; set; }
        public DateTime Vigencia { get; set; }
        public string ListaPrecio { get; set; }
        public bool Activo { get; set; }
    }
}
