using System;

namespace Service.Report.Dtos.MedicalRecord
{
    public class GeneralReportDto
    {
        public Guid Id { get; set; }
        public Guid ExpedienteId { get; set; }
        public string Sucursal { get; set; }
        public string Paciente { get; set; }
        public string Medico { get; set; }
        public string Compañia { get; set; }

    }
}
