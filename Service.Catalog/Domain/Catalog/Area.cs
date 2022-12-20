using System;

namespace Service.Catalog.Domain.Catalog
{
    public class Area : GenericCatalog
    {
        public Area()
        {
        }

        public Area(int id, int departamentoId, string clave, string nombre, int orden)
        {
            Id = id;
            DepartamentoId = departamentoId;
            Clave = clave;
            Nombre = nombre;
            Orden = orden;
            Activo = true;
            FechaCreo = DateTime.Now;
        }

        public int DepartamentoId { get; set; }
        public virtual Department Departamento { get; set; }
        public int Orden { get; set; }
    }
}
