﻿using System.Collections.Generic;

namespace Service.MedicalRecord.Dtos.ClinicResults
{
    public class ClinicResultPathologicalPdfDto
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
        public string Departamento { get; set; }
        public bool isHistopathologic { get; set; } // validate by areaId
        public List<string> ImagenesHistopatologicas { get; set; }
        public string MuestraRecibida { get; set; }
        public string DescripcionMacroscopica { get; set; }
        public string DescripcionMicroscopica { get; set; }
        public string Diagnostico { get; set; }
        public string NombreFirma { get; set; }
    }
}
