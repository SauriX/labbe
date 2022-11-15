using System.Collections.Generic;
using System;
using System.Text.Json.Serialization;

namespace Service.MedicalRecord.Dtos.Quotation
{
    public class QuotationDto
    {
        public QuotationDto()
        {
        }

        public QuotationDto(Guid expedienteId, Guid sucursalId, string clave, Guid usuarioId)
        {
            ExpedienteId = expedienteId;
            SucursalId = sucursalId;
            Clave = clave;
            UsuarioId = usuarioId;
        }

        public string NombreMedico { get; set; }
        public string NombreCompania { get; set; }
        public string ClaveMedico { get; set; }
        public string Observaciones { get; set; }
        public Guid CotizacionId { get; set; }
        public Guid? ExpedienteId { get; set; }
        public Guid SucursalId { get; set; }
        public string Clave { get; set; }
        public string Registro { get; set; }
        [JsonIgnore]
        public string Usuario { get; set; }
        [JsonIgnore]
        public Guid UsuarioId { get; set; }
    }
}
