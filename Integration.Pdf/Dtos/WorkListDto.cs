using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Integration.Pdf.Dtos
{
    public class WorkListDto
    {
        public string HojaTrabajo { get; set; }
        public string Sucursal { get; set; }
        public string Fecha { get; set; }
        public string HoraInicio { get; set; }
        public string HoraFin { get; set; }
        public List<WorkListRequestDto> Solicitudes { get; set; }
    }

    public class WorkListRequestDto
    {
        public string Solicitud { get; set; }
        public string HoraSolicitud { get; set; }
        public string Paciente { get; set; }
        public List<WorkListStudyDto> Estudios { get; set; }
    }

    public class WorkListStudyDto
    {
        public string Estudio { get; set; }
        public List<string> ListasTrabajo { get; set; }
        public string Estatus { get; set; }
        public string HoraEstatus { get; set; }
    }
}