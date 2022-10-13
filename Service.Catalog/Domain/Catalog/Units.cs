using System;

namespace Service.Catalog.Domain.Catalog
{
    public class Units : GenericCatalog
    {
        public Units()
        {
        }

        public Units(int id, string clave)
        {
            Id = id;
            Clave = clave;
            Nombre = clave;
            Activo = true;
            FechaCreo = DateTime.Now;
        }
    }
}
