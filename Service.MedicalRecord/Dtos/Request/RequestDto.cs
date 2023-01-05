using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Service.MedicalRecord.Dtos.Request
{
    public class RequestDto
    {
        public RequestDto()
        {
        }

        public RequestDto(Guid expedienteId, Guid sucursalId, string clave, string cp, Guid usuarioId)
        {
            ExpedienteId = expedienteId;
            SucursalId = sucursalId;
            Clave = clave;
            ClavePatologica = cp;
            UsuarioId = usuarioId;
        }

        public string NombreMedico { get; set; }
        public string NombreCompania { get; set; }
        public string ClaveMedico { get; set; }
        public string Observaciones { get; set; }
        public Guid? SolicitudId { get; set; }
        public Guid ExpedienteId { get; set; }
        public Guid SucursalId { get; set; }
        public string Clave { get; set; }
        public string ClavePatologica { get; set; }
        public string Registro { get; set; }
        public bool Parcialidad { get; set; }
        public bool EsNuevo { get; set; }
        public string FolioWeeClinic { get; set; }
        public bool EsWeeClinic => !string.IsNullOrEmpty(FolioWeeClinic);
        public bool TokenValidado { get; set; }
        public List<string> Servicios { get; set; }
        [JsonIgnore]
        public string Usuario { get; set; }
        [JsonIgnore]
        public Guid UsuarioId { get; set; }
        public bool SaldoPendiente { get; set; }
    }
}
