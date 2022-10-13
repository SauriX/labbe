using Service.Catalog.Domain.Catalog;
using System;
using System.Collections.Generic;

namespace Service.Catalog.Domain.Indication
{
    public class Indication : GenericCatalogDescription
    {
        public Indication()
        {
        }

        public Indication(int id, string clave, string descripcion)
        {
            Id = id;
            Clave = clave;
            Nombre = clave;
            Descripcion = descripcion;
            Activo = true;
            FechaCreo = DateTime.Now;
        }

        public virtual ICollection<IndicationStudy> Estudios { get; set; }
    }
}
