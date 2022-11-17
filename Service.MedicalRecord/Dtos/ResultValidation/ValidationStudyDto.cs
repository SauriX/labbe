using System;

namespace Service.MedicalRecord.Dtos.ResultValidation
{
    public class ValidationStudyDto
    {
        public int Id { get; set; }
        public string Study { get; set; }
        public string Area { get; set; }
        public string Status { get; set; }
        public int Estatus { get; set; }
        public DateTime Registro { get; set; }
        public DateTime Entrega { get; set; }
        public Guid SolicitudId { get; set; }
    }
}
