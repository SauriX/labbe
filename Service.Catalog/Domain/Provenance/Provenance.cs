using Service.Catalog.Domain.Catalog;
using System;

namespace Service.Catalog.Domain.Provenance
{
    public class Provenance : GenericCatalog
    {
        public Provenance()
        {
        }

        public Provenance(int id, string clave, string nombre)
        {
            Id = id;
            Clave = clave;
            Nombre = nombre;
            Activo = true;
            FechaCreo = DateTime.Now;
        }
    }
}
