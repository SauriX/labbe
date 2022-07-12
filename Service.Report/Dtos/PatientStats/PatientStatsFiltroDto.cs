using System;

namespace Service.Report.Dtos.PatientStats
{
    public class PatientStatsFiltroDto
    {
        public Guid Id { get; set; } 
        public string NombrePaciente { get; set; }
        public int Solicitudes { get; set; }
        public decimal Total { get; set; }
    }
}
