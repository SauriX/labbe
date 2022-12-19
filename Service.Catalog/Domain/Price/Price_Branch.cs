using System;

namespace Service.Catalog.Domain.Price
{
    public class Price_Branch
    {
        public Price_Branch()
        {
        }

        public Price_Branch(Guid precioListaId, Guid sucursalId)
        {
            PrecioListaId = precioListaId;
            SucursalId = sucursalId;
            Activo = true;
            FechaCreo = DateTime.Now;
        }

        public Guid PrecioListaId { get; set; }
        public virtual PriceList PrecioLista { get; set; }
        public Guid SucursalId { get; set; }
        public virtual Branch.Branch Sucursal { get; set; }
        public Guid Id { get; set; }
        public decimal Precio { get; set; }
        public bool Activo { get; set; }
        public string UsuarioCreoId { get; set; }
        public DateTime FechaCreo { get; set; }
        public string UsuarioModId { get; set; }
        public DateTime? FechaMod { get; set; }
    }
}
