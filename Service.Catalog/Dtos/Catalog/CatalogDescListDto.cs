using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Service.Catalog.Dtos.Catalog
{
    public class CatalogDescListDto
    {
        public int Id { get; set; }
        public string Clave { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set;}
        public bool Activo { get; set; }
    }
}
