using System;
using System.Collections.Generic;

namespace Service.MedicalRecord.Dtos.RportStudy
{
    public class ReportRequestListDto
    {
        public string ExpedienteId { get; set; }
        public string SolicitudId { get; set; }
        public string Solicitud { get; set; }
        public string Paciente { get; set; }
        public string Edad { get; set; }
        public string Sexo { get; set; }
        public string Sucursal { get; set; }
        public string Medico { get; set; }
        public string Tipo { get; set; }
        public string Compañia { get; set; }
        public string Entrega { get; set; }
        public List<ReportStudyListDto> Estudios { get; set; }
        public string Estatus { get; set; }
        public bool isPatologia { get; set; }
        public List<DateTime> Fechas { get; set; }
    }
}
