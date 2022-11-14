using Service.MedicalRecord.Dtos.PriceQuote;
using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Service.MedicalRecord.Dtos.PriceQuote
{
    public class PriceQuoteStudyUpdateDto
    {
        public Guid CotizacionId { get; set; }
        public List<PriceQuoteStudyDto> Estudios { get; set; }
        public List<PriceQuotePackDto> Paquetes { get; set; }
        public PriceQuoteTotalDto Total { get; set; }
        [JsonIgnore]
        public string Usuario { get; set; }
        [JsonIgnore]
        public Guid UsuarioId { get; set; }
    }
}
