﻿using System;

namespace Service.Catalog.Domain.Study
{
    public class PacketStudy
    {
        public int PacketId { get; set; }
        public virtual Domain.Packet.Packet Packet { get; set; }
        public int EstudioId { get; set; }
        public virtual Study Estudio { get; set; }
        public bool Activo { get; set; }
        public string UsuarioCreoId { get; set; }
        public DateTime FechaCreo { get; set; }
        public int? UsuarioModId { get; set; }
        public DateTime? FechaMod { get; set; }
    }
}
