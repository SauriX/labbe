using Service.Catalog.Domain.Catalog;
using Service.Catalog.Domain.Study;
using System;
using System.Collections.Generic;

namespace Service.Catalog.Domain.Packet
{
    public class Packet : GenericCatalog
    {
        public Packet()
        {
        }

        public Packet(int id, string clave, string nombre, string nombreLargo, int areaId, int departamentoId, bool visibilidad)
        {
            Id = id;
            Clave = clave;
            Nombre = nombre;
            NombreLargo = nombreLargo;
            AreaId = areaId;
            DepartamentoId = departamentoId;
            Visibilidad = visibilidad;
            Activo = true;
            FechaCreo = DateTime.Now;
        }

        public int AreaId { get; set; }
        public virtual Area Area { get; set; }
        public int DepartamentoId { get; set; }
        public string NombreLargo { get; set; }
        public bool Visibilidad { get; set; }
        public ICollection<PacketStudy> Estudios { get; set; }
    }
}
