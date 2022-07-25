using System;

namespace Service.Report.Dtos.PatientStats
{
    public class PatientStatsDto
    {
        public Guid Id { get; set; }
        public string Paciente { get; set; }
        public int NoSolicitudes { get; set; }
        public decimal Total { get; set; }
    }
}