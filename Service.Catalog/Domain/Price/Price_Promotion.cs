﻿using System;

namespace Service.Catalog.Domain.Price
{
    public class Price_Promotion
    {
        public Guid PrecioListaId { get; set; }
        public virtual PriceList PrecioLista { get; set; }
        public int PromocionId { get; set; }
        public virtual Promotion.Promotion Promocion { get; set; }
        public bool Activo { get; set; }
        public decimal Precio { get; set; }
        public long UsuarioCreoId { get; set; }
        public DateTime FechaCreo { get; set; }
        public string UsuarioModId { get; set; }
        public DateTime FechaMod { get; set; }
    }
}
