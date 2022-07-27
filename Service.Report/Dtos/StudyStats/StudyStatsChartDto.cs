using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Service.Report.Dtos.StudyStats
{
    public class StudyStatsChartDto
    {
        public Guid Id { get; set; }
        public string Estatus { get; set; }
        public int Cantidad{ get; set; }
        public string Color { get; set; }
    }
}
