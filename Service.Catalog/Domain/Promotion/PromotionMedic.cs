using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Service.MedicalRecord.Domain;
using System;

namespace Service.Catalog.Domain.Promotion
{
    public class PromotionMedic : BaseModel
    {
        public Guid MedicoId { get; set; }
        public virtual Medics.Medics Medico { get; set; }
        public int PromocionId { get; set; }
        public virtual Promotion Promocion { get; set; }
    }
}
