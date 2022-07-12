using System;

namespace Service.Report.Dtos.PatientStats
{
    public class PatientStatsFiltroDto
    {
        public Guid Id { get; set; } 
        public string Nombre { get; set; }
        public string Solicitado { get; set; }
        public decimal Monto { get; set; }
    }
}
