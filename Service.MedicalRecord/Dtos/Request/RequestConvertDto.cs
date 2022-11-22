using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Service.MedicalRecord.Dtos.Request
{
    public class RequestConvertDto
    {
        public Guid ExpedienteId { get; set; }
        public Guid SucursalId { get; set; }
        public RequestGeneralDto General { get; set; }
        public List<RequestStudyDto> Estudios { get; set; }
        public List<RequestPackDto> Paquetes { get; set; }
        [JsonIgnore]
        public string Usuario { get; set; }
        [JsonIgnore]
        public Guid UsuarioId { get; set; }
    }
}
