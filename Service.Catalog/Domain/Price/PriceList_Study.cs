using System;

namespace Service.Catalog.Domain.Price
{
    public class PriceList_Study
    {
        public PriceList_Study()
        {
        }

        public PriceList_Study(Guid preciosListaId, int estudioId, decimal precio)
        {
            PrecioListaId = preciosListaId;
            EstudioId = estudioId;
            Precio = precio;
            Activo = true;
            FechaCreo = DateTime.Now;
        }

        public Guid PrecioListaId { get; set; }
        public virtual Price.PriceList PrecioLista { get; set; }
        public int EstudioId { get; set; }
        public virtual Study.Study Estudio { get; set; }
        public int Id { get; set; }
        public decimal Precio { get; set; }
        public bool Activo { get; set; }
        public Guid? UsuarioCreoId { get; set; }
        public DateTime FechaCreo { get; set; }
        public Guid? UsuarioModId { get; set; }
        public DateTime? FechaMod { get; set; }
    }
}
