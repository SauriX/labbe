using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Service.Report.Dtos.CompanyStats
{
    public class CompanyDto
    {
        public List<CompanyStatsDto> CompanyStats { get; set; }
        public InvoiceDto CompanyTotal { get; set; }
    }
}
