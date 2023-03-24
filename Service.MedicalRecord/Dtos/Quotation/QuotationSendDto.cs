using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Service.MedicalRecord.Dtos.Quotation
{
    public class QuotationSendDto
    {
        public Guid CotizacionId { get; set; }
        public List<string> Correos { get; set; } = null;
        public List<string> Telefonos { get; set; } = null;
        [JsonIgnore]
        public Guid UsuarioId { get; set; }
    }
}
