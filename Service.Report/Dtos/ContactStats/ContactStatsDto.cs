using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Service.Report.Dtos.ContactStats
{
    public class ContactStatsDto
    {
        public Guid Id { get; set; }
        public string Expediente { get; set; }
        public string NombrePaciente { get; set; }
        public string NombreMedico { get; set; }
        public string Celular { get; set; }
        public string Correo { get; set; }
    }
}
