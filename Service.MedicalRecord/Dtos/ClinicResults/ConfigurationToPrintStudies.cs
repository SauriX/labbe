using System;
using System.Collections.Generic;

namespace Service.MedicalRecord.Dtos.ClinicResults
{
    public class ConfigurationToPrintStudies
    {
        public Guid SolicitudId { get; set; }
        public List<int> Estudios { get; set; }
        public bool ImprimirLogos { get; set; }
    }
}
