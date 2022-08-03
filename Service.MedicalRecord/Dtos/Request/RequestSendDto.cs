using Microsoft.AspNetCore.Mvc;
using System;
using System.Text.Json.Serialization;

namespace Service.MedicalRecord.Dtos.Request
{
    public class RequestSendDto
    {
        public Guid SolicitudId { get; set; }
        public Guid ExpedienteId { get; set; }
        public string Correo { get; set; } = null;
        public string Telefono { get; set; } = null;
        [JsonIgnore]
        public Guid UsuarioId { get; set; }
    }
}
