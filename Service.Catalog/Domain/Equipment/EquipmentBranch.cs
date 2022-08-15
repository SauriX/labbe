using Service.Catalog.Domain.Catalog;
using System;

namespace Service.Catalog.Domain.Equipment
{
    public class EquipmentBranch
    {
        public Guid EquipmentBranchId { get; set; }
        public Guid EquipmentId { get; set; }
        public virtual Equipos Equipment { get; set; }
        public Guid BranchId { get; set; }
        public virtual Branch.Branch Sucursal { get; set; }
        public int Num_Serie { get; set; }
        public Guid? UsuarioCreoId { get; set; }
        public DateTime? FechaCreo { get; set; }
        public Guid? UsuarioModId { get; set; }
        public DateTime? FechaMod { get; set; }
    }
}
