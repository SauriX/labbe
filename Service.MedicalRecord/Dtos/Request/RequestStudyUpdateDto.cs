using Service.MedicalRecord.Dtos.Catalogs;
using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Service.MedicalRecord.Dtos.Request
{
    public class RequestStudyUpdateDto
    {
        public Guid SolicitudId { get; set; }
        public Guid ExpedienteId { get; set; }
        public List<RequestStudyDto> Estudios { get; set; }
        public List<RequestPackDto> Paquetes { get; set; }
        public RequestTotalDto Total { get; set; }
        [JsonIgnore]
        public string Usuario { get; set; }
        [JsonIgnore]
        public Guid UsuarioId { get; set; }
    }
}