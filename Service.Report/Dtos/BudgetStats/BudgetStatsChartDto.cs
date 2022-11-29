using System;

namespace Service.Report.Dtos.BudgetStats
{
    public class BudgetStatsChartDto
    {
        public Guid Id { get; set; }
        public string Sucursal { get; set; }
        public decimal Total { get; set; }
    }
}
