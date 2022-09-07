using System;

namespace Service.Catalog.Domain.Loyalty
{
    public class LoyaltyPriceList
    {
        public Guid PrecioListaId { get; set; }
        public virtual Price.PriceList PrecioLista { get; set; }
        public Guid LoyaltyId { get; set; }
        public virtual Loyalty Loyalty { get; set; }
        public Guid? UsuarioCreoId { get; set; }
        public DateTime FechaCreo { get; set; }
    }
} 