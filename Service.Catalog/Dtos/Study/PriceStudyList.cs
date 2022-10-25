using Service.Catalog.Dtos.Pack;
using System.Collections.Generic;

namespace Service.Catalog.Dtos.Study
{
    public class PriceStudyList
    {
        public int Id { get; set; }
        public int EstudioId { get; set; }
        public string Clave { get; set; }
        public string Nombre { get; set; }
        //public int AreaId { get; set; }
        public string Area { get; set; }
        public string Departamento { get; set; }
        public bool Activo { get; set; }
        public decimal Precio { get; set; }
        public decimal Descuento { get; set; }
        public decimal DescuenNum { get; set; }
        public decimal PrecioFinal { get; set; }
        public IEnumerable<PackStudyDto> Pack { get; set; }
    }
}
