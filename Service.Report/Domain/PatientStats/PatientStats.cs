using System;

namespace Service.Report.Domain.PatientStats
{
    public class PatientStats
    {
        public Guid Id { get; set; }
        public string NombrePaciente { get; set; }
        public string Sucursal { get; set; }
        public DateTime Fecha { get; set; }
        public int Solicitudes { get; set; }
        public decimal Total { get; set; }
        public Guid SucursalId { get; set; }
        public Guid ExpendienteId { get; set; }
        public virtual MedicalRecord.MedicalRecord Expendiente { get; set; }
    }
}
