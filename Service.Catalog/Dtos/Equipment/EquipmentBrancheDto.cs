using System;

namespace Service.Catalog.Dtos.Equipment
{
    public class EquipmentBrancheDto
    {
        public Guid BranchId { get; set; }
        public int EquipmentId { get; set; }
        public string Num_serie { get; set; }
        public string SucursalText { get; set; }
    }
}
