using System;
using System.Collections.Generic;

namespace Service.Catalog.Domain.Catalog
{
    public class Budget : GenericCatalog
    {
        public Budget()
        {
        }

        public Budget(int id, Guid sucursalId, string clave, string nombre)
        {
            Id = id;
            SucursalId = sucursalId;
            Clave = clave;
            Nombre = nombre;
            FechaCreo = DateTime.Now;
        }

        public decimal CostoFijo { get; set; }
        public List<string> Ciudad { get; set; }
        public Guid SucursalId { get; set; }
        public virtual Branch.Branch Sucursal { get; set; }
    }
}
