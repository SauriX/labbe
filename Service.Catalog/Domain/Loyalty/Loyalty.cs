using System;

namespace Service.Catalog.Domain.Loyalty
{
    public class Loyalty
    {
        public Guid Id { get; set; }
        public string Clave { get; set; }
        public string Nombre { get; set; }
        public string TipoDescuento { get; set; }
        public decimal CantidadDescuento { get; set; }
        public DateTime FechaInicial { get; set; }
        public DateTime FechaFinal { get; set; }
        public bool Activo { get; set; }
        public Guid UsuarioCreoId { get; set; }
        public DateTime FechaCreo { get; set; }
        public Guid? UsuarioModId { get; set; }
        public DateTime? FechaMod { get; set; }
        public Guid? PrecioListaId { get; set; }
        public virtual Price.PriceList PrecioLista { get; set; }
    }
}