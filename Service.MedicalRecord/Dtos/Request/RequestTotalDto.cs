using System;
using System.Text.Json.Serialization;

namespace Service.MedicalRecord.Dtos.Request
{
    public class RequestTotalDto
    {
        public Guid SolicitudId { get; set; }
        public Guid ExpedienteId { get; set; }
        public decimal TotalEstudios { get; set; }
        public decimal Descuento { get; set; }
        public decimal Cargo { get; set; }
        public decimal Copago { get; set; }
        public decimal Total { get; set; }
        public decimal Saldo { get; set; }
        [JsonIgnore]
        public Guid UsuarioId { get; set; }
    }
}
