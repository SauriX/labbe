using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;

namespace Service.Catalog.Domain.Promotion
{
    public class PromotionMedics 
    {
        public Guid MedicId { get; set; }
        public virtual Medics.Medics Medic { get; set; }
        public int PromotionId { get; set; }
        public virtual Promotion Promotion { get; set; }
        public bool Activo { get; set; }
        public Guid UsuarioCreoId { get; set; }
        public DateTime FechaCreo { get; set; }
        public Guid UsuarioModId { get; set; }
        public DateTime? FechaMod { get; set; }
    }
}
