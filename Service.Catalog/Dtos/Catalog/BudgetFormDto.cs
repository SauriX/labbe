using DocumentFormat.OpenXml.Office2010.ExcelAc;
using Service.Catalog.Dtos.Branch;
using System;
using System.Collections.Generic;

namespace Service.Catalog.Dtos.Catalog
{
    public class BudgetFormDto
    {
        public int Id { get; set; }
        public string Clave { get; set; }
        public string NombreServicio { get; set; }
        public string Ciudad { get; set; }
        public bool Activo { get; set; }
        public decimal CostoFijo { get; set; }
        public Guid UsuarioId { get; set; }
        public DateTime Fecha { get; set; }
        public IEnumerable<BudgetBranchListDto> Sucursales { get; set; }
    }
}
