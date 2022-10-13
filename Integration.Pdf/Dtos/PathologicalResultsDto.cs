using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Integration.Pdf.Dtos
{
    public class PathologicalResultsDto
    {
        public List<Information> Information { get; set; }
        public bool ImprimrLogos { get; set; }
    }

    public class Information
    {
        public string Medico { get; set; }
        public string FechaEntrega { get; set; }
        public string Paciente { get; set; }
        public int Edad { get; set; }
        public string Estudio { get; set; }
        public string Clave { get; set; }
        public int Departamento { get; set; }
        public string MuestraRecibida { get; set; }
        public bool isHistopathologic { get; set; }
        public List<string> ImagenesHistopatologicas { get; set; }
        public string DescripcionMacroscopica { get; set; }
        public string DescripcionMicroscopica { get; set; }
        public string Diagnostico { get; set; }
        public string NombreFirma { get; set; }
    }
}