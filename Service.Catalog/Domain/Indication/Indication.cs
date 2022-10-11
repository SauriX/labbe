using Service.Catalog.Domain.Catalog;
using System.Collections.Generic;

namespace Service.Catalog.Domain.Indication
{
    public class Indication : GenericCatalogDescription
    {
        public Indication()
        {
        }

        public Indication(int id, string clave, string nombre)
        {
            Id = id;
            Clave = clave;
            Nombre = nombre;
            Activo = true;
        }

        public virtual ICollection<IndicationStudy> Estudios { get; set; }
    }
}
