using System;

namespace Service.Catalog.Domain.Catalog
{
    public class BudgetBranch
    {
        public Guid SucursalId { get; set; }
        public string Ciudad { get; set; }
        public virtual Branch.Branch Sucursal { get; set; }
        public int CostoFijoId { get; set; }
        public virtual Budget CostoFijo { get; set; }
        public bool Activo { get; set; }
        public DateTime FechaCreo { get; set; }
        public Guid UsuarioCreoId { get; set; }
        public DateTime? FechaModifico { get; set; }
        public Guid? UsuarioModificoId { get; set; }
    }
}
