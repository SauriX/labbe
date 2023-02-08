using System;
using System.Text.Json.Serialization;

namespace Service.MedicalRecord.Dtos.Request
{
    public class RequestGeneralDto
    {
        public Guid SolicitudId { get; set; }
        public Guid ExpedienteId { get; set; }
        public byte Procedencia { get; set; }
        public string Afiliacion { get; set; }
        public Guid? CompañiaId { get; set; }
        public Guid? MedicoId { get; set; }
        public byte Urgencia { get; set; }
        public string Observaciones { get; set; }
        public string Correo { get; set; }
        public string Whatsapp { get; set; }
        public bool EnvioMedico { get; set; }
        public bool Activo { get; set; }
        [JsonIgnore]
        public Guid UsuarioId { get; set; }
    }
}
