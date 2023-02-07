using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Service.MedicalRecord.Dtos.Request
{
    public class RequestSendDto
    {
        public Guid SolicitudId { get; set; }
        public Guid ExpedienteId { get; set; }
        public List<string> Correos { get; set; } = null;
        public List<string> Telefonos { get; set; } = null;
        [JsonIgnore]
        public Guid UsuarioId { get; set; }
    }
}
