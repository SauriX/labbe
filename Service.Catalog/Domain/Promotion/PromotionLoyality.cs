using System;

namespace Service.Catalog.Domain.Promotion
{
    public class PromotionLoyality
    {
        public int PromotionId { get; set; }
        public virtual Promotion Promotion { get; set; }
        public Guid LoyaltyId { get; set; }
        public virtual Loyalty.Loyalty loyalities { get; set;}
        public bool Activo { get; set; }
        public Guid UsuarioCreoId { get; set; }
        public DateTime FechaCreo { get; set; }
        public Guid UsuarioModId { get; set; }
        public DateTime? FechaMod { get; set; }
    }
}
