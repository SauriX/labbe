using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Service.MedicalRecord.Dtos.PriceQuote
{
    public class PriceQuoteDto
    {
        public string Id { get; set; }
        public string Clave { get; set; }
        public Guid SucursalId { get; set; }
        public string Expediente { get; set; }
        public string ExpedienteId { get; set; }
        public string NombrePaciente { get; set; }
        public int Edad { get; set; }
        public int Cargo { get; set; }
        public int Tipo { get; set; }
        public DateTime FechaNacimiento { get; set; }
        public string Genero { get; set; }
        [JsonIgnore]
        public string Usuario { get; set; }
        [JsonIgnore]
        public Guid UsuarioId { get; set; }
        public IEnumerable<PriceQuoteStudyInfoDto> Estudios { get; set; }
    }
}
