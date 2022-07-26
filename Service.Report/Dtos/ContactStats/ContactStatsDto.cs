using System;

namespace Service.Report.Dtos.ContactStats
{
    public class ContactStatsDto
    {
        public Guid Id { get; set; }
        public string Expediente { get; set; }
        public string Paciente { get; set; }
        public string Medico { get; set; }
        public string Clave { get; set; }
        public string Estatus { get; set; }
        public string Celular { get; set; }
        public string Correo { get; set; }
    }
}
