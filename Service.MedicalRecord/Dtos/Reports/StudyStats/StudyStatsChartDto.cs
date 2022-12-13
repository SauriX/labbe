using System;

namespace Service.MedicalRecord.Dtos.Reports.StudyStats
{
    public class StudyStatsChartDto
    {
        public Guid Id { get; set; }
        public string Estatus { get; set; }
        public int Cantidad { get; set; }
        public string Color { get; set; }
    }
}
