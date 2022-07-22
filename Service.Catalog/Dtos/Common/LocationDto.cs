using System.Collections.Generic;

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
