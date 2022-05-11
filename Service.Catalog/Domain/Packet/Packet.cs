using Service.Catalog.Domain.Catalog;
using Service.Catalog.Domain.Study;
using System.Collections.Generic;

namespace Service.Catalog.Domain.Packet
{
    public class Packet : GenericCatalog
    {
        public int AreaId { get; set; }
        public virtual Area Area { get; set; }
        public string NombreLargo { get; set; }
        public bool Visibilidad { get; set; }
        public ICollection<PacketStudy> studies { get; set;}

    }
}
