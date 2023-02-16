using Service.MedicalRecord.Dtos.Catalogs;
using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Service.MedicalRecord.Dtos.Request
{
    public class RequestStudyUpdateDto
    {
        public RequestStudyUpdateDto()
        {
        }

        public RequestStudyUpdateDto(Guid expendienteId, Guid solicitudId, Guid usuarioId)
        {
            ExpedienteId = expendienteId;
            SolicitudId = solicitudId;
            UsuarioId = usuarioId;
            Estudios = new List<RequestStudyDto>();
            Paquetes = new List<RequestPackDto>();
            Total = new RequestTotalDto();
        }

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