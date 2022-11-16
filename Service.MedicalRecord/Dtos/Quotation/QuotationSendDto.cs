using System;
using System.Text.Json.Serialization;

namespace Service.MedicalRecord.Dtos.Quotation
{
    public class QuotationSendDto
    {
        public Guid CotizacionId { get; set; }
        public string Correo { get; set; } = null;
        public string Telefono { get; set; } = null;
        [JsonIgnore]
        public Guid UsuarioId { get; set; }
    }
}
