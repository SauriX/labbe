using DocumentFormat.OpenXml.Office2010.ExcelAc;
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
    }

    public class ServicesCostTimeDto
    {
        public decimal TotalMensual { get; set; }
        public decimal TotalSemanal { get; set; }
        public decimal TotalDiario { get; set; }
    }

    public class ServicesDto
    {
        public List<ServicesCostDto> CostoServicios { get; set; }
        public ServicesCostTimeDto CostoTemporal { get; set; }
    }
}
