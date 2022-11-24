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

        public Guid CotizacionId { get; set; }
        public string NombreMedico { get; set; }
        public string NombreCompania { get; set; }
        public string ClaveMedico { get; set; }
        public string Observaciones { get; set; }
        public Guid? ExpedienteId { get; set; }
        public Guid SucursalId { get; set; }
        public string Clave { get; set; }
        public string Registro { get; set; }
        public byte EstatusId { get; set; }
        [JsonIgnore]
        public string Usuario { get; set; }
        [JsonIgnore]
        public Guid UsuarioId { get; set; }
        public bool Activo { get; set; }
    }
}
