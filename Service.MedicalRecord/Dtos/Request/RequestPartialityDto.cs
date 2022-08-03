using System;
using System.Text.Json.Serialization;

namespace Service.MedicalRecord.Dtos.Request
{
    public class RequestPartialityDto
    {
        public Guid SolicitudId { get; set; }
        public Guid ExpedienteId { get; set; }
        public bool Aplicar { get; set; }
        [JsonIgnore]
        public Guid UsuarioId { get; set; }
    }
}
