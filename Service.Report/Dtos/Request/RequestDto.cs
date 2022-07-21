using System;

namespace Service.Report.Dtos.Request
{
    public class RequestDto
    {
        public Guid Id { get; set; }
        public string ExpedienteNombre { get; set; }
        public string PacienteNombre { get; set; }
        public int Visitas { get; set; }
    }
}
