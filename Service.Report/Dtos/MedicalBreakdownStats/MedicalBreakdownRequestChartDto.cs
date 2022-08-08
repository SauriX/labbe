using System;

namespace Service.Report.Dtos.MedicalBreakdownStats
{
    public class MedicalBreakdownRequestChartDto
    {
        public Guid Id { get; set; }
        public int NoSolicitudes { get; set; }
        public string ClaveMedico { get; set; }
    }
}
