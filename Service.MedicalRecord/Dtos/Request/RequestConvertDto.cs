using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Service.MedicalRecord.Dtos.Request
{
    public class RequestConvertDto
    {
        public Guid? SolicitudId { get; set; }
        public Guid ExpedienteId { get; set; }
        public Guid SucursalId { get; set; }
        public string Clave { get; set; }
        public string ClavePatologica { get; set; }
        public string Registro { get; set; }
        public bool Parcialidad { get; set; }
        public bool EsNuevo { get; set; }
        public RequestGeneralDto General { get; set; }
        public List<RequestStudyDto> Estudios { get; set; }
        public List<RequestPackDto> Paquetes { get; set; }
        [JsonIgnore]
        public Guid UsuarioId { get; set; }
    }
}
