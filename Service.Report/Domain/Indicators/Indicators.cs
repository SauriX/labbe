using Service.Report.Domain.Catalogs;
using System;

namespace Service.Report.Domain.Indicators
{
    public class Indicators
    {
        public Guid Id { get; set; }
        public decimal CostoReactivo { get; set; }
        public DateTime Fecha { get; set; }
        public Guid SucursalId { get; set; }
    }
}
