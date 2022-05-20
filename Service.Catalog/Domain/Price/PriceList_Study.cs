using System;

namespace Service.Catalog.Domain.Price
{
    public class PriceList_Study
    {
        public Guid PrecioListaId { get; set; }
        public virtual Price.PriceList PrecioLista { get; set; }
        public int EstudioId { get; set; }
        public virtual Study.Study Estudio { get; set; }
        public int Id { get; set; }
        public string Area { get; set; }
        public decimal Precio { get; set; }
        public bool Activo { get; set; }
        public long UsuarioCreoId { get; set; }
        public DateTime FechaCreo { get; set; }
        public string UsuarioModId { get; set; }
        public DateTime FechaMod { get; set; }
    }
}
