using System;

namespace Service.Report.Dtos.Indicators
{
    public class SamplesCostsDto
    {
        public Guid Id { get; set; }
        public decimal CostoToma { get; set; }
        public Guid SucursalId { get; set; }
        public string Sucursal { get; set; }
        public DateTime FechaAlta { get; set; }
    }
}
