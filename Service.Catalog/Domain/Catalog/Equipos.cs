using Service.Catalog.Domain.Equipment;
using System.Collections.Generic;

namespace Service.Catalog.Domain.Catalog
{
    public class Equipos: GenericCatalog
    {
        public string NombreLargo { get; set; }
        public string Categoria { get; set; }
        public virtual IEnumerable<EquipmentBranch> Valores { get; set; }

    }
}
