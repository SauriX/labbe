using System;

namespace Service.Catalog.Domain.Price
{
    public class Price_Medics
    {
        public Guid PrecioListaId { get; set; }
        public virtual PriceList PrecioLista { get; set; }
        public Guid MedicoId { get; set; }
        public virtual Medics.Medics Medico { get; set; }
        public Guid Id { get; set; }
        public string Clave { get; set; }
        public string Nombre { get; set; }
        public decimal Precio { get; set; }
        public bool Activo { get; set; }
        public long UsuarioCreoId { get; set; }
        public DateTime FechaCreo { get; set; }
        public string UsuarioModId { get; set; }
        public DateTime FechaMod { get; set; }
    }
}
