using System;

namespace Service.Catalog.Domain.Catalog
{
    public class Department : GenericCatalog
    {
        public Department()
        {
        }

        public Department(int id, string clave, string nombre)
        {
            Id = id;
            Clave = clave;
            Nombre = nombre;
            Activo = true;
            FechaCreo = DateTime.Now;
        }
    }
}
