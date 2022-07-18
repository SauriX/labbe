using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Service.Catalog.Dtos.Common
{
    public class LocationDto
    {
        public string CodigoPostal { get; set; }
        public string Estado { get; set; }
        public string Ciudad { get; set; }
        public IEnumerable<CatalogDto> Colonias { get; set; }
    }
}
