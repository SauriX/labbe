using System;

namespace Service.Report.Dtos.Request
{
    public class RequestStatsDto
    {
        public Guid Id { get; set; }
        public string Expediente { get; set; }
        public string Paciente { get; set; }
        public int NoSolicitudes { get; set; }
    }
}
