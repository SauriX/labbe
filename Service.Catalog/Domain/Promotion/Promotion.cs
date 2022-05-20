using Service.Catalog.Domain.Price;
using System;
using System.Collections.Generic;

namespace Service.Catalog.Domain.Promotion
{
    public class Promotion
    {
        public int Id { get; set; }
        public string Clave { get; set; }
        public string Nombre { get; set; }
        public string TipoDeDescuento { get; set; }
        public decimal CantidadDescuento { get; set; }
        public DateTime FechaInicio { get; set; }
        public DateTime FechaFinal { get; set; }
        public bool Visibilidad { get; set; }
        public bool Activo { get; set; }
        public string UsuarioCreoId { get; set; }
        public DateTime FechaCreo { get; set; }
        public string UsuarioModificoId { get; set; }
        public DateTime? FechaModifico { get; set; }
        public Guid PrecioListaId { get; set; }
        public ICollection<PromotionBranch> branches { get; set; }
        public ICollection<PromotionLoyality> loyalities { get; set; }
        public ICollection<PromotionPack> packs { get; set; }
        public ICollection<PromotionStudy> studies { get; set; }
        public ICollection<Price_Promotion> prices { get; set; }
    }
}
