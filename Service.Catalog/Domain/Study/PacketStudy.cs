using System;

namespace Service.Catalog.Domain.Study
{
    public class PacketStudy
    {
        public PacketStudy()
        {
        }

        public PacketStudy(int paqueteId, int estudioId, int orden)
        {
            PacketId = paqueteId;
            EstudioId = estudioId;
            Activo = true;
            FechaCreo = DateTime.Now;
            Orden = orden;
        }

        public int PacketId { get; set; }
        public virtual Domain.Packet.Packet Packet { get; set; }
        public int EstudioId { get; set; }
        public virtual Study Estudio { get; set; }
        public int Orden { get; set; }
        public bool Activo { get; set; }
        public Guid? UsuarioCreoId { get; set; }
        public DateTime FechaCreo { get; set; }
        public Guid? UsuarioModificoId { get; set; }
        public DateTime? FechaModifico { get; set; }
    }
}
