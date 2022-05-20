using Service.Catalog.Domain.Catalog;

namespace Service.Catalog.Dtos.PriceList
{
    public class PriceListStudyDto
    {
        public int Id { get; set; }
        public string Clave { get; set; }
        public string Nombre { get; set; }
        //public int AreaId { get; set; }
        public string Area { get; set; }
        public string Departamento { get; set; }
        public bool Activo { get; set; }
        public decimal Precio { get; set; }

    }
}
