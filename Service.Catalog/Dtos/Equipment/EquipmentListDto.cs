using Service.Catalog.Domain.Equipment;
using System;
using System.Collections.Generic;

namespace Service.Catalog.Dtos.Equipment
{
    public class EquipmentListDto
    {
        public int Id { get; set; }
        public string Clave { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public string Categoria { get; set; }
        public bool Activo { get; set; }
        public virtual IEnumerable<EquipmentBranch> Valores { get; set; }
    }
}
