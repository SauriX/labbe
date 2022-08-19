using Service.Report.Dtos.StudyStats;
using System;
using System.Collections.Generic;

namespace Service.Report.Dtos
{
    public class MaquilaRequestDto
    {
        public Guid Id { get; set; }
        public string Solicitud { get; set; }
        public string Paciente { get; set; }
        public int Edad { get; set; }
        public string Sexo { get; set; }
        public string Medico { get; set; }
        public string FechaEntrega { get; set; }
        public string Maquila { get; set; }
        public string Sucursal { get; set; }
        public string Estatus { get; set; }
        public string NombreEstudio { get; set; }
        public string ClaveEstudio { get; set; }
    }
}
