using System;

namespace Service.Catalog.Domain.Company
{
    public class Price_Company
    {
        public Guid PrecioId { get; set; }
        public virtual Price.PriceList Precio { get; set; }
        public Guid CompañiaId { get; set; }
        public virtual Company Compañia { get; set; }
        public bool Activo { get; set; }
        public long UsuarioCreoId { get; set; }
        public DateTime FechaCreo { get; set; }
        public string UsuarioModId { get; set; }
        public DateTime FechaMod { get; set; }
    }
}
