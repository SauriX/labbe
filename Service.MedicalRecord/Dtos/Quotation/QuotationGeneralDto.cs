using System;
using System.Text.Json.Serialization;

namespace Service.MedicalRecord.Dtos.Quotation
{
    public class QuotationGeneralDto
    {
        public Guid CotizacionId { get; set; }
        public byte Procedencia { get; set; }
        public Guid? CompañiaId { get; set; }
        public Guid? MedicoId { get; set; }
        public string Observaciones { get; set; }
        public string Correo { get; set; }
        public string Whatsapp { get; set; }
        public bool Activo { get; set; }
        [JsonIgnore]
        public Guid UsuarioId { get; set; }
    }
}
