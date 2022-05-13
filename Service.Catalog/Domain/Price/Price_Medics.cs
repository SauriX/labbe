using System;

namespace Service.Catalog.Domain.Price
{
    public class Price_Medics
    {
        public Guid PrecioId { get; set; }
        public virtual PriceList Precio { get; set; }
        public int MedicoId { get; set; }
        public virtual Medics.Medics Medico { get; set; }
        public bool Activo { get; set; }
        public long UsuarioCreoId { get; set; }
        public DateTime FechaCreo { get; set; }
        public string UsuarioModId { get; set; }
        public DateTime FechaMod { get; set; }
    }
}
