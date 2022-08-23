using System;

namespace Service.Catalog.Dtos.Equipmentmantain
{
    public class MantainListDto
    {
        public Guid Id { get; set; }
        public string Clave { get; set; }
        public DateTime Fecha { get; set; }
        public bool activo { get; set; }
    }
}
