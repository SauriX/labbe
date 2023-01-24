using System;
using System.Collections.Generic;

namespace Service.MedicalRecord.Dtos.WorkList
{
    public class WorkListDto
    {
        public string HojaTrabajo { get; set; }
        public string Sucursal { get; set; }
        public string Fecha { get; set; }
        public List<string> Fechas { get; set; }
        public string HoraInicio { get; set; }
        public string HoraFin { get; set; }
        public bool MostrarResultado { get; set; }
        public List<WorkListRequestDto> Solicitudes { get; set; } = new List<WorkListRequestDto>();
    }

    public class WorkListRequestDto
    {
        public Guid Id { get; set; }
        public string Solicitud { get; set; }
        public string HoraSolicitud { get; set; }
        public string Paciente { get; set; }
        public List<WorkListStudyDto> Estudios { get; set; } = new List<WorkListStudyDto>();
    }

    public class WorkListStudyDto
    {
        public int SolicitudEstudioId { get; set; }
        public int EstudioId { get; set; }
        public string Estudio { get; set; }
        public string Estatus { get; set; }
        public string HoraEstatus { get; set; }
        public List<string> ListasTrabajo { get; set; } = new List<string>();
        public List<WorkListStudyResultsDto> ResultadoListasTrabajo { get; set; } = new List<WorkListStudyResultsDto>();
    }

    public class WorkListStudyResultsDto
    {
        public string Clave { get; set; }
        public string Resultado { get; set; }
        public string Unidades { get; set; }
    }
}