using System;
using System.Reflection.Metadata.Ecma335;

namespace Service.Report.Dtos.Indicators
{
    public class IndicatorsStatsDto
    {
        public Guid Id { get; set; }
        public string Sucursal { get; set; }
        public decimal Pacientes { get; set; }
        public decimal Ingresos { get; set; }
        public decimal CostoReactivo { get; set; }
        public decimal CostoTomaCalculado { get; set; }
        public DateTime FechaInicial { get; set; }
        public DateTime FechaFinal { get; set; }
        public decimal UtilidadOperacion => (CostoReactivo + CostoTomaCalculado + CostoFijo) - Ingresos;
        public decimal CostoFijo { get; set; }
    }
}
