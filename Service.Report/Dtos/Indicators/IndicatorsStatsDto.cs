using System;
using System.Reflection.Metadata.Ecma335;

namespace Service.Report.Dtos.Indicators
{
    public class IndicatorsStatsDto
    {
        public Guid Id { get; set; }
        public string Sucursal { get; set; }
        public string Ciudad { get; set; }
        public Guid SucursalId { get; set; }
        public int Pacientes { get; set; }
        public decimal Ingresos { get; set; }
        public decimal CostoReactivo { get; set; }
        public decimal CostoTomaCalculado { get; set; }
        public DateTime FechaInicial { get; set; }
        public DateTime FechaFinal { get; set; }
        public DateTime FechaAlta { get; set; }
        public decimal UtilidadOperacion => Ingresos - (CostoReactivo + CostoTomaCalculado + CostoFijo);
        public decimal CostoFijo { get; set; }
        public int Expedientes { get; set; }
    }

    public class IndicatorsListDto
    {
        public Guid Id { get; set; }
        public decimal CostoReactivo { get; set; }
        public Guid SucursalId { get; set; }
        public DateTime Fecha { get; set; }
    }
}
