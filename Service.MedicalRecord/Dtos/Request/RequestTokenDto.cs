using Newtonsoft.Json;
using System;

namespace Service.MedicalRecord.Dtos.Request
{
    public class RequestTokenDto
    {
        public Guid SolicitudId { get; set; }
        public Guid ExpedienteId { get; set; }
        public string Token { get; set; }
        public bool Reenviar { get; set; }
        [JsonIgnore]
        public Guid UsuarioId { get; set; }
    }
}
