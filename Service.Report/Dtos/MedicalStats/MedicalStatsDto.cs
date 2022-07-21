using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Service.Report.Dtos.MedicalStats
{
    public class MedicalStatsDto
    {
        public Guid Id { get; set; }
        public string ClaveMedico { get; set; }
        public string NombreMedico { get; set; }
        public int Solicitudes { get; set; }
        public int Pacientes { get; set; }
        public decimal Total { get; set; }
    }
}
