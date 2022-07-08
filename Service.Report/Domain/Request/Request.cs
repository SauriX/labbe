using System;

namespace Service.Report.Domain.Request
{
    public class Request
    {
        public Guid Id { get; set; }
        public string ExpedienteNombre { get; set; }
        public string Sucursal { get; set; }
        public DateTime FechaInicial { get; set; }
        public DateTime FechaFinal { get; set; }
        public decimal Precio { get; set; }
        public string Ciudad { get; set; }
        public Guid SucursalId { get; set; }
        public Guid ExpedienteId { get; set; }
        public virtual MedicalRecord.MedicalRecord Expediente { get; set; }
    }
}