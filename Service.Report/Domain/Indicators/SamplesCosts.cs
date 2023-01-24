using System;

namespace Service.Report.Domain.Indicators
{
    public class SamplesCosts
    {
        public Guid Id { get; set; }
        public decimal CostoToma { get; set; }
        public Guid SucursalId { get; set; }
        public string Sucursal { get; set; }
        public string Ciudad { get; set; }
        public DateTime FechaAlta { get; set; }
        public DateTime? FechaModificacion { get; set; }
    }
}
