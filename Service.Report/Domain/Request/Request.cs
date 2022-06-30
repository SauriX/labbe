using System;

namespace Service.Report.Domain.Request
{
    public class Request
    {
        public Guid Id { get; set; }
        public string Clave { get; set; }
        public string Nombre { get; set; }
        public Guid ExpedienteId { get; set; }
        public virtual MedicalRecord.MedicalRecord Expediente { get; set; }
    }
}
