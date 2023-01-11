using System;

namespace Service.Catalog.Dtos.Catalog
{
    public class BudgetListDto
    {
        public int Id { get; set; }
        public string Clave { get; set; }
        public string Nombre { get; set; }
        public string Sucursal { get; set; }
        public bool Activo { get; set; }
        public DateTime? FechaAlta { get; set; }
        public decimal CostoFijo { get; set; }
    }

    public class BudgetBranchListDto
    {
        public Guid SucursalId { get; set; }
        public int CostoFijoId { get; set; }
        public string Ciudad { get; set; }
    }
}
