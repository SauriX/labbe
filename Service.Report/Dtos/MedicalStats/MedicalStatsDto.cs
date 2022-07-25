using System;

namespace Service.Report.Dtos.MedicalStats
{
    public class MedicalStatsDto
    {
        public Guid Id { get; set; }
        public string ClaveMedico { get; set; }
        public string Medico { get; set; }
        public int NoSolicitudes { get; set; }
        public int NoPacientes { get; set; }
        public decimal Total { get; set; }
    }
}
