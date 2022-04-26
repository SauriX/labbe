using System;
using System.Collections.Generic;

namespace Service.Catalog.Dtos
{
    public class PriceFormDto
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
        public virtual IEnumerable<PromotionFormDto> Promocion { get; set; }
    }
}
