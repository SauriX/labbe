using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Service.Catalog.Domain.Catalog
{
    public class Area : GenericCatalog
    {
        public int DepartamentoId { get; set; }
        public virtual Department Departamento { get; set; }
    }
}
