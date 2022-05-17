﻿using System;

namespace Service.Catalog.Domain.Promotion
{
    public class PromotionPack
    {
        public int PromotionId { get; set; }
        public virtual Promotion Promotion { get; set; }
        public int PackId { get; set; }
        public virtual Packet.Packet Pack { get; set; }
        public decimal Discountporcent { get; set; }
        public decimal DiscountNumeric { get; set; }
        public decimal Price { get; set; }
        public decimal FinalPrice { get; set; }
        public bool Loyality { get; set; }
        public DateTime FechaInicio { get; set; }
        public DateTime FechaFinal { get; set; }
        public bool Activo { get; set; }
        public Guid UsuarioCreoId { get; set; }
        public DateTime FechaCreo { get; set; }
        public Guid UsuarioModId { get; set; }
        public DateTime? FechaMod { get; set; }
    }
}
