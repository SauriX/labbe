using System;

namespace Service.Catalog.Domain.Price
{
    public class Price_Branch
    {
        public Guid PrecioId { get; set; }
        public virtual PriceList Precio { get; set; }
        public Guid SucursalId { get; set; }
        public virtual Branch.Branch Sucursal { get; set; }
        public bool Activo { get; set; }
        public long UsuarioCreoId { get; set; }
        public DateTime FechaCreo { get; set; }
        public string UsuarioModId { get; set; }
        public DateTime FechaMod { get; set; }
    }
}
