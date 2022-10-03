using System;
using System.Collections.Generic;

namespace Service.MedicalRecord.Dtos.Request
{
    public class RequestInfoDto
    {
        public Guid SolicitudId { get; set; }
        public Guid ExpedienteId { get; set; }
        public string Clave { get; set; }
        public string ClavePatologica { get; set; }
        public string Afiliacion { get; set; }
        public string Paciente { get; set; }
        public string Compañia { get; set; }
        public string Procedencia { get; set; }
        public string Factura { get; set; }
        public decimal Importe { get; set; }
        public decimal Descuento { get; set; }
        public decimal Total { get; set; }
        public decimal Saldo { get; set; }
        public IEnumerable<RequestStudyInfoDto> Estudios { get; set; }
    }

    public class RequestStudyInfoDto
    {
        public int Id { get; set; }
        public int estudioId { get; set; }
        public string Clave { get; set; }
        public string Nombre { get; set; }
        public byte EstatusId { get; set; }
        public string Estatus { get; set; }
        public string Color { get; set; }
        public int Departamento { get; set; }
        public int AreaId { get; set; }
        public string FechaTomaMuestra { get; set; }
        public string FechaValidacion { get; set; }
        public string FechaSolicitado{ get; set; }
        public string FechaCaptura { get; set; }
        public string FechaLiberado { get; set; }
        public string FechaEnviado { get; set; }
    }
}
