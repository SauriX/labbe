using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Service.Report.Dtos.StudyStats
{
    public class StudyStatsDto
    {
        public string Solicitud { get; set; }
        public string Paciente { get; set; }
        public List<StudiesDto> Estudio { get; set; }
        public int Edad { get; set; }
        public string Sexo { get; set; }
        public string Medico { get; set; }
        public DateTime FechaEntrega { get; set; }
        public string Estatus { get; set; }
        public DateTime Fecha { get; set; }
        public bool Parcialidad { get; set; }
    }
}
