using System.Collections.Generic;
using System;
using Service.MedicalRecord.Dtos.Reports.StudyStats;

namespace Service.MedicalRecord.Dtos.Reports
{
    public class ReportInfoDto
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
        public string Fecha { get; set; }
        public bool Parcialidad { get; set; }
        public byte Urgencia { get; set; }
    }
}
