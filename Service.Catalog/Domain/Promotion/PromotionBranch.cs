using System;

namespace Service.Catalog.Domain.Promotion
{
    public class PromotionBranch
    {
        public int PromotionId { get; set; }
        public virtual Promotion Promotion { get; set; }
        public Guid BranchId { get; set; }
        public virtual Branch.Branch Branch { get; set; }
        public bool Activo { get; set; }
        public Guid UsuarioCreoId { get; set; }
        public DateTime FechaCreo { get; set; }
        public Guid UsuarioModId { get; set; }
        public DateTime? FechaMod { get; set; }
    }
}
