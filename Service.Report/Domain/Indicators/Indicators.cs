using Service.Report.Domain.Catalogs;
using System;

namespace Service.Report.Domain.Indicators
{
    public class Indicators
    {
        public Guid Id { get; set; }
        public int Pacientes { get; set; }
        public decimal Ingresos { get; set; }
        public decimal CostoReactivo { get; set; }
        public decimal CostoTomaCalculado { get; set; }
        public DateTime FechaInicial { get; set; }
        public DateTime FechaFinal { get; set; }
        public decimal UtilidadOperacion { get; set; }
        public Guid SucursalId { get; set; }
        public virtual Branch Sucursal { get; set; }
        public decimal CostoFijo { get; set; }
    }
}
