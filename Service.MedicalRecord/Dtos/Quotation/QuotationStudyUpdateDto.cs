using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Service.MedicalRecord.Dtos.Quotation
{
    public class QuotationStudyUpdateDto
    {
        public Guid CotizacionId { get; set; }
        public List<QuotationStudyDto> Estudios { get; set; }
        public List<QuotationPackDto> Paquetes { get; set; }
        public QuotationTotalDto Total { get; set; }
        [JsonIgnore]
        public string Usuario { get; set; }
        [JsonIgnore]
        public Guid UsuarioId { get; set; }
    }
}
