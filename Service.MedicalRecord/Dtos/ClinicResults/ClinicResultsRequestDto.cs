using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Service.MedicalRecord.Dtos.ClinicResults
{
    public class ClinicResultsRequestDto
    {
        public Guid Id { get; set; }
        public string Clave { get; set; }
        public string Medico { get; set; }
        public string Paciente { get; set; }
        public string Compañia { get; set; }
        public int Edad { get; set; }
        public string Sexo { get; set; }
        public string Expediente { get; set; }
        public string FechaEntrega { get; set; }
        public string FechaAdmision { get; set; }
        public string User { get; set; }
        public string Metodo { get; set; }
    }
}
