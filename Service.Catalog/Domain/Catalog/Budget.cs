using System;
using System.Collections.Generic;

namespace Service.Catalog.Domain.Catalog
{
    public class Budget : GenericCatalog
    {
        public Budget()
        {
        }

        public Budget(int id, string clave, string nombre)
        {
            Id = id;
            Clave = clave;
            Nombre = nombre;
            FechaCreo = DateTime.Now;
        }

        public decimal CostoFijo { get; set; } 
        public virtual ICollection<BudgetBranch> Sucursales { get; set; }
    }
}
