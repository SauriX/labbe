using System;

namespace Service.Report.Domain.PatientStats
{
    public class PatientStats
    {
        public Guid Id { get; set; }
        public string Nombre { get; set; }
        public string Sucursal { get; set; }
        public DateTime FechaInicial { get; set; }
        public DateTime FechaFinal { get; set; }
        public string Solicitado { get; set; }
        public decimal Monto { get; set; }
        public Guid SucursalId { get; set; }
        public Guid ExpendienteId { get; set; }
        public virtual MedicalRecord.MedicalRecord Expendiente { get; set; }
    }
}
