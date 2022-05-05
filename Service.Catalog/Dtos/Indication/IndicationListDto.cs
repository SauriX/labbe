using Service.Catalog.Dtos.Catalog;
using System.Collections.Generic;

namespace Service.Catalog.Dtos.Indication
{
    public class IndicationListDto
    {
        public int Id { get; set; }
        public string Clave { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public bool Activo { get; set; }
    }
}
