using System;

namespace Service.Catalog.Domain.Price
{
    public class PriceList_Packet
    {
        public Guid PrecioId { get; set; }
        public virtual Price.PriceList Precio { get; set; }
        public int? PaqueteId { get; set; }
        public virtual Packet.Packet Paquete { get; set; }
        public bool Activo { get; set; }
        public long UsuarioCreoId { get; set; }
        public DateTime FechaCreo { get; set; }
        public string UsuarioModId { get; set; }
        public DateTime FechaMod { get; set; }
    }
}
