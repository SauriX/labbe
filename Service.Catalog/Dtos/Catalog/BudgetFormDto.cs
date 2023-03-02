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
        public bool Activo { get; set; }
        public decimal CostoFijo { get; set; }
        public Guid UsuarioId { get; set; }
        public DateTime Fecha { get; set; }
        public IEnumerable<BudgetBranchListDto> Sucursales { get; set; }
    }

    public class BudgetBranchFormDto
    {
        public Guid Id { get; set; }
        public int ServicioId { get; set; }
        public Guid SucursalId { get; set; }
        public string Ciudad { get; set; }
        public decimal CostoFijo { get; set; }
        public string NombreServicio { get; set; }
        public DateTime Fecha { get; set; }
        public Guid UsuarioId { get; set; }
    }

    public class ServiceUpdateDto
    {
        public Guid Id { get; set; }
        public Guid Identificador { get; set; }
        public int CostoFijoId { get; set; }
        public List<CityBranchServiceDto> Sucursales { get; set; }
        public decimal CostoFijo { get; set; }
        public decimal TotalSucursales { get; set; }
        public string Nombre { get; set; }
        public DateTime FechaAlta { get; set; }
        public List<Guid> CostosId { get; set; }
    }

    public class UpdateServiceDto
    {
        public IEnumerable<ServiceUpdateDto> Servicios { get; set; }
        public BudgetFilterDto Filtros { get; set; }
    }

    public class CityBranchServiceDto
    {
        public Guid CostoId { get; set; }
        public Guid SucursalId { get; set; }
        public string Ciudad { get; set; }
    }
}
