using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Service.Report.Dtos.CompanyStats
{
    public class CompanyStatsChartDto
    {
        public Guid Id { get; set; }
        public int NoSolicitudes { get; set; }
        public string Compañia { get; set; }
    }
}
