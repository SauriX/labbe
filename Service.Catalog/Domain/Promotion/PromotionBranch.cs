using Service.MedicalRecord.Domain;
using System;

namespace Service.Catalog.Domain.Promotion
{
    public class PromotionBranch : BaseModel
    {
        public int PromocionId { get; set; }
        public virtual Promotion Promocion { get; set; }
        public Guid SucursalId { get; set; }
        public virtual Branch.Branch Sucursal { get; set; }
    }
}
