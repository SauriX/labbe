using System;
using System.Collections.Generic;

namespace Service.Catalog.Domain.Price
{
    public class Price
    {
        public int Id { get; set; }
        public string Clave { get; set; }
        public string Nombre { get; set; }
        public bool Visibilidad { get; set; }
        public bool Activo { get; set; }
        public string UsuarioCreoId { get; set; }
        public DateTime FechaCreo { get; set; }
        public string UsuarioModificoId { get; set; }
        public DateTime? FechaModifico { get; set; }
        public virtual ICollection<Price_Promotion> Promocion { get; set; }
    }
}
