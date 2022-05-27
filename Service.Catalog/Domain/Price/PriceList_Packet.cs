using System;

namespace Service.Catalog.Domain.Price
{
    public class PriceList_Packet
    {
        public Guid PrecioListaId { get; set; }
        public virtual Price.PriceList PrecioLista { get; set; }
        public int PaqueteId { get; set; }
        public virtual Packet.Packet Paquete { get; set; }
        public bool Activo { get; set; }
        public decimal Precio { get; set; }
        public long UsuarioCreoId { get; set; }
        public DateTime FechaCreo { get; set; }
        public string UsuarioModId { get; set; }
        public DateTime FechaMod { get; set; }


    }
}
