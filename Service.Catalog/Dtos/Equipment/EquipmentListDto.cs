using System;

namespace Service.Catalog.Dtos.Equipment
{
    public class EquipmentListDto
    {
        public int Id { get; set; }
        public string Clave { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public bool Activo { get; set; }
    }
}
