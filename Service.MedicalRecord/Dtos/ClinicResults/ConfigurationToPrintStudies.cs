using System;
using System.Collections.Generic;

namespace Service.MedicalRecord.Dtos.ClinicResults
{
    public class ConfigurationToPrintStudies
    {
        public Guid SolicitudId { get; set; }
        public List<ConfigurationTypeResults> Estudios { get; set; }
        public bool ImprimirLogos { get; set; }
        public bool ImprimirCriticos { get; set; }
        public bool ImprimirPrevios { get; set; }
    }

    public class ConfigurationTypeResults
    {
        public int Id { get; set; }
        public string Tipo { get; set; }
    }
}
