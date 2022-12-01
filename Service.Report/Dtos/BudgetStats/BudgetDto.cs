using Service.Report.Dtos.TypeRequest;
using System.Collections.Generic;

namespace Service.Report.Dtos.BudgetStats
{
    public class BudgetDto
    {
        public List<BudgetStatsDto> BudgetStats { get; set; }
        public InvoiceDto BudgetTotal { get; set; }
    }
}
