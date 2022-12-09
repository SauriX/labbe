using System;

namespace Service.MedicalRecord.Dtos.Reports.BudgetStats
{
    public class BudgetStatsChartDto
    {
        public Guid Id { get; set; }
        public string Fecha { get; set; }
        public string Sucursal { get; set; }
        public decimal Total { get; set; }
    }
}
