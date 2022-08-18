using Service.Report.Dtos.StudyStats;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Service.Report.Dtos
{
    public class BondedRequestDto
    {
        public Guid Id { get; set; }
        public string Solicitud { get; set; }
        public string Paciente { get; set; }
        public List<StudiesDto> Estudio { get; set; }
        public int Edad { get; set; }
        public string Sexo { get; set; }
        public string Medico { get; set; }
        public string FechaEntrega { get; set; }
        public string Estatus { get; set; }
    }
}
