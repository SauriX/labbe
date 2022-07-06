using System;
using System.Collections.Generic;

namespace Service.MedicalRecord.Dtos.Request
{
    public class RequestDto
    {
        public Guid? Id { get; set; }
        public Guid ExpedienteId { get; set; }
        public Guid SucursalId { get; set; }
        public string Clave { get; set; }
        public string ClavePatologica { get; set; }
        public Guid UsuarioId { get; set; }
        public RequestGeneralDto General { get; set; }
        public List<RequestStudyDto> Estudios { get; set; }
    }
}
