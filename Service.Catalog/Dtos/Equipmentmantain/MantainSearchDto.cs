using System;

namespace Service.Catalog.Dtos.Equipmentmantain
{
    public class MantainSearchDto
    {
        public DateTime[] Fecha { get; set; }
        public string Clave { get; set; }
        public Guid IdEquipo { get; set; }
    }
}
