using System.Collections.Generic;

namespace Service.MedicalRecord.Dtos.Reports.BudgetStats
{
    public class BudgetDto
    {
        public List<BudgetStatsDto> BudgetStats { get; set; }
        public InvoiceDto BudgetTotal { get; set; }
    }
}
