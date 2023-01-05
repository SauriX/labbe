using DocumentFormat.OpenXml.Office2010.ExcelAc;
using System;
using System.Collections.Generic;

namespace Service.Catalog.Dtos.Catalog
{
    public class BudgetFormDto
    {
        public int Id { get; set; }
        public string Clave { get; set; }
        public string NombreServicio { get; set; }
        public bool Activo { get; set; }
        public decimal CostoFijo { get; set; }
        public string Sucursal { get; set; }
        public List<string> Ciudad { get; set; }
        public Guid SucursalId { get; set; }
        public Guid UsuarioId { get; set; }
        public DateTime FechaAlta { get; set; }
    }
}
