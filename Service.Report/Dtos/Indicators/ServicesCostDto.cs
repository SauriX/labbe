using DocumentFormat.OpenXml.Office2010.ExcelAc;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;

namespace Service.Report.Dtos.Indicators
{
    public class ServicesCostDto
    {
        public int Id { get; set; }
        public string Clave { get; set; }
        public string Nombre { get; set; }
        public string Sucursal { get; set; }
        public bool Activo { get; set; }
        public DateTime? FechaAlta { get; set; }
        public decimal CostoFijo { get; set; }
        public decimal CostosFijos { get; set; }
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
        public List<ServiceUpdateDto> Servicios { get; set; }
        public ReportModalFilterDto Filtros { get; set; }
    }

    public class CityBranchServiceDto
    {
        public Guid CostoId { get; set; }
        public Guid SucursalId { get; set; }
        public string Ciudad { get; set; }
    }

    public class BudgetFormDto
    {
        public int Id { get; set; }
        public string Clave { get; set; }
        public string NombreServicio { get; set; }
        public bool Activo { get; set; }
        public decimal CostoFijo { get; set; }
        public Guid UsuarioId { get; set; }
        public DateTime Fecha { get; set; }
        public List<BudgetBranchListDto> Sucursales { get; set; }
        public BudgetBranchListDto Sucursal { get; set; }
    }

    public class BudgetBranchListDto
    {
        public Guid SucursalId { get; set; }
        public int CostoFijoId { get; set; }
        public string Ciudad { get; set; }
    }

    public class InvoiceServicesDto
    {
        public List<ServiceUpdateDto> Servicios { get; set; }
        public decimal TotalMensual { get; set; }
        public decimal TotalSemanal => TotalMensual / 6;
        public decimal TotalDiario => TotalMensual / 24;
    }
}
